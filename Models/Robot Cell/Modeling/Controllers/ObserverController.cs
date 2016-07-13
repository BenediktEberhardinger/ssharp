// The MIT License (MIT)
// 
// Copyright (c) 2014-2016, Institute for Software & Systems Engineering
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

	internal abstract class ObserverController : Component
	{
		private bool _hasReconfed = false;
		//[Hidden]
		private bool _reconfigurationRequested = true;

		protected ObserverController(IEnumerable<Agent> agents, List<Task> tasks)
		{
			Tasks = tasks;
			Agents = agents.ToArray();

			foreach (var agent in Agents)
			{
				agent.ObserverController = this;
				GenerateConstraints(agent);
			}
		}

		protected ObjectPool<Role> RolePool { get; } = new ObjectPool<Role>(Model.MaxRoleCount);
		protected List<Task> Tasks { get; }

		[Hidden(HideElements = true)]
		protected Agent[] Agents { get; }

		public ReconfStates ReconfigurationState { get; protected set; } = ReconfStates.NotSet;

		protected abstract void Reconfigure();

		public void ScheduleReconfiguration()
		{
			_reconfigurationRequested = true;
		}

		private void GenerateConstraints(Agent agent)
		{
			agent.Constraints = new List<Func<bool>>()
			{
				// I/O Consistency
				() => agent.AllocatedRoles.All(role => role.PreCondition.Port == null || agent.Inputs.Contains(role.PreCondition.Port)),
				() => agent.AllocatedRoles.All(role => role.PostCondition.Port == null || agent.Outputs.Contains(role.PostCondition.Port)),
				// Capability Consistency
				() =>
					agent.AllocatedRoles.All(
						role => role.CapabilitiesToApply.All(capability => agent.AvailableCapabilites.Contains(capability))),
                //   Pre-PostconditionConsistency
				() =>
					agent.AllocatedRoles.Any(role => role.PostCondition.Port == null || role.PreCondition.Port == null)
						? true
						: agent.AllocatedRoles.TrueForAll(
							role => PostMatching(role, agent) && PreMatching(role, agent))
			};
		}

		private bool PostMatching(Role role, Agent agent)
		{
			if (!role.PostCondition.Port.AllocatedRoles.Any(role1 => role1.PreCondition.Port.Equals(agent)))
			{
				;
			}
			else if (
				!role.PostCondition.Port.AllocatedRoles.Any(
					role1 =>
						role.PostCondition.State.Select(capability => capability.Identifier)
							.SequenceEqual(role1.PreCondition.State.Select(capability => capability.Identifier))))
			{
				;
			}
			else if (!role.PostCondition.Port.AllocatedRoles.Any(role1 => role.PostCondition.Task.Equals(role1.PreCondition.Task)))
			{
				;
			}

			return role.PostCondition.Port.AllocatedRoles.Any(role1 => role1.PreCondition.Port.Equals(agent)
																	   &&
																	   role.PostCondition.State.Select(capability => capability.Identifier)
																		   .SequenceEqual(role1.PreCondition.State.Select(capability => capability.Identifier))
																	   && role.PostCondition.Task.Equals(role1.PreCondition.Task));
		}

		private bool PreMatching(Role role, Agent agent)
		{
			return role.PreCondition.Port.AllocatedRoles.Any(role1 => role1.PostCondition.Port.Equals(agent)
																	  && role.PreCondition.State.SequenceEqual(role1.PostCondition.State)
																	  && role.PreCondition.Task.Equals(role1.PostCondition.Task));
		}

		public override void Update()
		{
			foreach (var agent in Agents)
			{
				if (_reconfigurationRequested)
					break;

				agent.Update();
			}

			if (!_reconfigurationRequested)
				return;

			// TODO: This speeds up analyses when checking for reconf failures with DCCA, but is otherwise
			// unacceptable for other kinds of analyses

			if (_hasReconfed)
				return;

			foreach (var agent in Agents)
				agent.CheckAllocatedCapabilities();

			RolePool.Reset();
			Reconfigure();
			_reconfigurationRequested = false;
			_hasReconfed = true;
		}
	}
}