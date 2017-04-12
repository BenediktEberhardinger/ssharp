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

namespace ISSE.SafetyChecking.Simulator
{
	using System;
	using System.Collections.Generic;
	using System.Runtime.InteropServices;
	using System.Text;
	using ExecutableModel;
	using Utilities;
	using ExecutedModel;

	/// <summary>
	///   Simulates a S# model for debugging or testing purposes.
	/// </summary>
	public unsafe class Simulator<TExecutableModel> : DisposableObject where TExecutableModel : ExecutableModel<TExecutableModel>
	{
		private readonly CounterExample<TExecutableModel> _counterExample;
		public readonly TExecutableModel RuntimeModel;
		private readonly List<byte[]> _states = new List<byte[]>();
		private ChoiceResolver _choiceResolver;
		private int _stateIndex;

		/// <summary>
		///   Initializes a new instance.
		/// </summary>
		/// <param name="model">The model that should be simulated.</param>
		/// <param name="formulas">The formulas that can be evaluated on the model.</param>
		public Simulator(TExecutableModel runtimeModel)
		{
			Requires.NotNull(runtimeModel, nameof(runtimeModel));

			RuntimeModel = runtimeModel;
			Reset();
		}

		/// <summary>
		///   Initializes a new instance.
		/// </summary>
		/// <param name="counterExample">The counter example that should be simulated.</param>
		public Simulator(CounterExample<TExecutableModel> counterExample)
		{
			Requires.NotNull(counterExample, nameof(counterExample));

			_counterExample = counterExample;
			RuntimeModel = counterExample.RuntimeModel;

			Reset();
		}

		/// <summary>
		///   Gets a value indicating whether the simulator is replaying a counter example.
		/// </summary>
		public bool IsReplay => _counterExample != null;

		/// <summary>
		///   Gets a value indicating whether the simulation is completed.
		/// </summary>
		public bool IsCompleted => _counterExample != null && _stateIndex + 1 >= _counterExample.StepCount;

		/// <summary>
		///   Gets a value indicating whether the simulation can be fast-forwarded.
		/// </summary>
		public bool CanFastForward => _counterExample == null || !IsCompleted;

		/// <summary>
		///   Gets a value indicating whether the simulation can be rewound.
		/// </summary>
		public bool CanRewind => _stateIndex > 0;

		/// <summary>
		///   Runs a step of the simulation. Returns <c>false</c> to indicate that the simulation is completed.
		/// </summary>
		public bool SimulateStep()
		{
			if (IsCompleted)
				return false;

			Prune();

			var state = stackalloc byte[RuntimeModel.StateVectorSize];

			if (_counterExample == null)
			{
				RuntimeModel.ExecuteStep();

				RuntimeModel.Serialize(state);
				AddState(state);
			}
			else
			{
				_counterExample.DeserializeState(_stateIndex + 1);
				RuntimeModel.Serialize(state);

				EnsureStatesMatch(state, _counterExample.GetState(_stateIndex + 1));

				AddState(state);
				Replay();
			}

			return true;
		}

		/// <summary>
		///   Advances the simulator by the given number of <paramref name="steps" />, if possible.
		/// </summary>
		/// <param name="steps">The number of steps the simulation should be advanced.</param>
		public void FastForward(int steps)
		{
			// Reuse the already discovered states after a rewind
			var advanceCount = Math.Min(steps, _states.Count - _stateIndex - 1);
			_stateIndex += advanceCount;
			RestoreState(Math.Min(_stateIndex, _states.Count - 1));

			// Continue the simulation once we cannot reuse previously discovered states
			for (var i = 0; i < steps - advanceCount; ++i)
			{
				SimulateStep();

				if (IsCompleted)
					return;
			}
		}

		/// <summary>
		///   Rewinds the simulator by the given number of <paramref name="steps" />, if possible.
		/// </summary>
		/// <param name="steps">The number of steps that should be rewound.</param>
		public void Rewind(int steps)
		{
			_stateIndex = Math.Max(0, _stateIndex - steps);
			RestoreState(_stateIndex);
		}

		/// <summary>
		///   Prunes all states lying in the future after a rewind.
		/// </summary>
		public void Prune()
		{
			// Delete all previously discovered states that lie in the future if the simulation has
			// been rewinded and is resimulated from a certain point in the past
			for (var i = _states.Count - 1; i >= _stateIndex + 1; --i)
				_states.RemoveAt(i);
		}

		/// <summary>
		///   Resets the model to its initial state.
		/// </summary>
		public void Reset()
		{
			if (_choiceResolver == null)
			{
				_choiceResolver = new NondeterministicChoiceResolver();
				RuntimeModel.SetChoiceResolver(_choiceResolver);
			}

			var state = stackalloc byte[RuntimeModel.StateVectorSize];

			_states.Clear();
			_stateIndex = -1;

			if (_counterExample == null)
				RuntimeModel.Reset();
			else
				_counterExample.Replay(_choiceResolver, 0);

			RuntimeModel.Serialize(state);
			AddState(state);
		}

		/// <summary>
		///   Replays the next transition of the simulated counter example.
		/// </summary>
		private void Replay()
		{
			_counterExample.Replay(_choiceResolver, _stateIndex);

			var actualState = stackalloc byte[RuntimeModel.StateVectorSize];
			RuntimeModel.Serialize(actualState);

			if (IsCompleted && _counterExample.EndsWithException)
			{
				throw new InvalidOperationException(
					"The path the counter example was generated for ended with an exception that could not be reproduced by the replay.");
			}

			EnsureStatesMatch(actualState, _states[_stateIndex]);
		}

		/// <summary>
		///   Ensures that the two states match.
		/// </summary>
		private void EnsureStatesMatch(byte* actualState, byte[] expectedState)
		{
			fixed (byte* state = expectedState)
			{
				if (MemoryBuffer.AreEqual(actualState, state, RuntimeModel.StateVectorSize))
					return;

				var builder = new StringBuilder();
				for (var i = 0; i < RuntimeModel.StateVectorSize; ++i)
					builder.AppendLine($"@{i}: {actualState[i]} vs. {_states[_stateIndex][i]}");

				throw new InvalidOperationException($"Invalid replay of counter example: Unexpected state difference.\n\n{builder}");
			}
		}

		/// <summary>
		///   Adds the state to the simulator.
		/// </summary>
		private void AddState(byte* state)
		{
			var newState = new byte[RuntimeModel.StateVectorSize];
			Marshal.Copy(new IntPtr(state), newState, 0, RuntimeModel.StateVectorSize);

			_states.Add(newState);
			++_stateIndex;
		}

		/// <summary>
		///   Restores a previously discovered state.
		/// </summary>
		private void RestoreState(int stateNumber)
		{
			fixed (byte* state = _states[stateNumber])
				RuntimeModel.Deserialize(state);
		}

		/// <summary>
		///   Disposes the object, releasing all managed and unmanaged resources.
		/// </summary>
		/// <param name="disposing">If true, indicates that the object is disposed; otherwise, the object is finalized.</param>
		protected override void OnDisposing(bool disposing)
		{
			if (!disposing)
				return;

			_counterExample.SafeDispose();
			_choiceResolver.SafeDispose();
		}
	}
}