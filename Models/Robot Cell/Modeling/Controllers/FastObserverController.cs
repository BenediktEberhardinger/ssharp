﻿// The MIT License (MIT)
// 
// Copyright (c) 2014-2017, Institute for Software & Systems Engineering
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

namespace SafetySharp.CaseStudies.RobotCell.Modeling.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using SafetySharp.Modeling;

	/// <summary>
	///   An <see cref="ObserverController" /> implementation that is much faster than
	///   the MiniZinc implementation.
	/// </summary>
	internal class FastObserverController : ObserverController
	{
		[Hidden(HideElements = true)]
		private readonly RobotAgent[] _availableRobots;

		[Hidden(HideElements = true)]
		private int[,] _costMatrix;

		[Hidden(HideElements = true)]
		private int[,] _pathMatrix;

		public static MonitoringHeuristic MonitoringHeuristic { get; set; }

		public FastObserverController(IEnumerable<Agent> agents, List<Task> tasks)
			: base(agents, tasks)
		{
			_availableRobots = Agents.OfType<RobotAgent>().ToArray();
		}

		protected override void Reconfigure()
		{
			var isReconfPossible = IsReconfPossible(_availableRobots, Tasks);

			// find optimal path that satisfies the required capabilities
			CalculateShortestPaths();
			var path = FindPath(Tasks[0]);
			var roles = path == null ? null : Convert(Tasks[0], path).ToArray();

#if ENABLE_F5
			var length = roles?.Sum(role => Math.Max(role.Item2.Length, 1));
			if (roles == null || length > 2 * Tasks[0].Capabilities.Length)
#else
			if (roles == null)
#endif
			{
				if (isReconfPossible)
					throw new Exception("Reconfiguration failed even though there is a solution.");
				ReconfigurationState = ReconfStates.Failed;
			}
			else
			{
				ApplyConfiguration(roles);
				if (!isReconfPossible)
					throw new Exception("Reconfiguration successful even though there is no valid configuration.");
				ReconfigurationState = ReconfStates.Succedded;
			}

			MonitoringHeuristic?.ReportReconfiguration(Agents);
		}

		/// <summary>
		///   Calculates the connection matrix for the available robots.
		/// </summary>
		/// <returns>A tuple containing the successor matrix and the path length matrix. -1 indicates no successor / infinite costs.</returns>
		private void CalculateShortestPaths()
		{
			_pathMatrix = new int[_availableRobots.Length, _availableRobots.Length];
			_costMatrix = new int[_availableRobots.Length, _availableRobots.Length];

			for (var i = 0; i < _availableRobots.Length; ++i)
			{
				for (var j = 0; j < _availableRobots.Length; ++j)
				{
					// neighbours
					if (_availableRobots[i].Outputs.Any(cart => cart.Outputs.Contains(_availableRobots[j])))
					{
						_pathMatrix[i, j] = j;
						_costMatrix[i, j] = 1;
					}
					else // default for non-neighbours
					{
						_pathMatrix[i, j] = -1; // signifies no path
						_costMatrix[i, j] = -1; // signifies infinity
					}
				}

				// reflexive case
				_pathMatrix[i, i] = i;
				_costMatrix[i, i] = 0;
			}

			// Floyd-Warshall algorithm
			for (var link = 0; link < _availableRobots.Length; ++link)
			{
				for (var start = 0; start < _availableRobots.Length; ++start)
				{
					for (var end = 0; end < _availableRobots.Length; ++end)
					{
						if (_costMatrix[start, link] > -1 && _costMatrix[link, end] > -1 // paths start->link and link->end exist
							&& (_costMatrix[start, end] == -1 || _costMatrix[start, end] > _costMatrix[start, link] + _costMatrix[link, end]))
						{
							_costMatrix[start, end] = _costMatrix[start, link] + _costMatrix[link, end];
							_pathMatrix[start, end] = _pathMatrix[start, link];
						}
					}
				}
			}
		}

		/// <summary>
		///   Finds a sequence of connected robots that are able to fulfill the
		///   <param name="task" />'s capabilities.
		/// </summary>
		/// <returns>
		///   An array of robot identifiers, one for each capability.
		/// </returns>
		private int[] FindPath(Task task)
		{
			var path = new int[task.Capabilities.Length];

			for (var first = 0; first < _availableRobots.Length; ++first)
			{
				if (CanSatisfyNext(task, 0, first))
				{
					path[0] = first;
					if (FindPath(task, path, 1))
						return path;
				}
			}

			return null;
		}

		/// <summary>
		///   Recursively checks if there is a valid path with the given prefix for the task.
		///   If so, returns true and <param name="path" /> contains the path. Otherwise, returns false.
		/// </summary>
		private bool FindPath(Task task, int[] path, int prefixLength)
		{
			// termination case: the path is already complete
			if (prefixLength == task.Capabilities.Length)
				return true;

			var last = path[prefixLength - 1];

			// special handling: see if the last robot can't do the next capability as well
			if (CanSatisfyNext(task, prefixLength, last))
			{
				path[prefixLength] = last;
				if (FindPath(task, path, prefixLength + 1))
					return true;
			}
			else // otherwise check connected robots
			{
				for (int next = 0; next < _availableRobots.Length; ++next) // go through all stations
				{
					// if connected to last robot and can fulfill next capability
					if (_pathMatrix[last, next] != -1 && CanSatisfyNext(task, prefixLength, next) && next != last)
					{
						path[prefixLength] = next; // try a path over next
						if (FindPath(task, path, prefixLength + 1)) // if there is such a path, return true
							return true;
					}
				}
			}

			return false; // there is no valid path with the given prefix
		}

		/// <summary>
		///   Checks if the given robot can satisfy all the demanded capabilities.
		/// </summary>
		/// <param name="task">The task for which a path is searched.</param>
		/// <param name="capability">The zero-based index of the task's capability that should be applied next.</param>
		/// <param name="robot">The robot which should be next on the path.</param>
		/// <returns>True if choosing station as next path entry would not exceed its capabilities.</returns>
		private bool CanSatisfyNext(Task task, int capability, int robot)
		{
			return _availableRobots[robot].AvailableCapabilities.Any(c => c.IsEquivalentTo(task.Capabilities[capability]));
		}

		private IEnumerable<Tuple<Agent, Capability[]>> Convert(Task task, int[] path)
		{
			var previous = -1;
			var usedCarts = new List<Agent>();

			for (var i = 0; i < path.Length;)
			{
				var current = path[i];

				if (previous != -1)
				{
#if ENABLE_F1 // error F1: transitive routes interpreted as direct ones
					yield return Transport(previous, current, usedCarts);
#else
					// Find a cart that connects both robots, the path matrix contains the next robot we have to go to
					foreach (var nextRobot in GetShortestPath(previous, current))
					{
						yield return Transport(previous, nextRobot, usedCarts);

						if (nextRobot != current)
#if ENABLE_F7 // new error F7: intermediate transport-only roles assigned to path[i] == current; meant for nextRobot
							yield return Tuple.Create(Agents[path[i]], new Capability[0]);
#else
							yield return Tuple.Create(Agents[nextRobot], new Capability[0]);
#endif

							previous = nextRobot;
					}
#endif
					}

				// Collect the capabilities that this robot should apply
				var capabilities = 
					path
					.Skip(i)
					.TakeWhile(robot => robot == current)
					.Select((_, index) => task.Capabilities[i + index])
					.ToArray();

				yield return Tuple.Create(Agents[path[i]], capabilities);
				previous = current;

				i += capabilities.Length;
			}
		}

		private IEnumerable<int> GetShortestPath(int from, int to)
		{
			for (var current = _pathMatrix[from, to]; current != to; current = _pathMatrix[current, to])
				yield return current;
			yield return to;
		}

		private Tuple<Agent, Capability[]> Transport(int from, int to, List<Agent> usedCarts)
		{
			// prefer not to use a cart that has already been used
			var candidates = _availableRobots[from].Outputs.Where(c => _availableRobots[to].Inputs.Contains(c)).ToArray();
			var unusedCart = candidates.Except(usedCarts).FirstOrDefault();
			if (unusedCart != null)
			{
				usedCarts.Add(unusedCart);
				return Tuple.Create(unusedCart, new Capability[0]);
			}

			// otherwise, reuse some cart
			return Tuple.Create(candidates.First(), new Capability[0]);
		}
	}
}