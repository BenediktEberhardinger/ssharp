// The MIT License (MIT)
// 
// Copyright (c) 2014-2015, Institute for Software & Systems Engineering
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

namespace SafetySharp.Runtime
{
	using System.Linq;
	using System.Runtime.CompilerServices;
	using Analysis;
	using CompilerServices;
	using Modeling;
	using Serialization;
	using Utilities;

	/// <summary>
	///   Represents a runtime model that can be used for model checking or simulation.
	/// </summary>
	public sealed unsafe class RuntimeModel : DisposableObject
	{
		/// <summary>
		///   The <see cref="ChoiceResolver" /> used by the model.
		/// </summary>
		private readonly ChoiceResolver _choiceResolver;

		/// <summary>
		///   Deserializes a state of the model.
		/// </summary>
		private readonly SerializationDelegate _deserialize;

		/// <summary>
		///   Serializes a state of the model.
		/// </summary>
		private readonly SerializationDelegate _serialize;

		/// <summary>
		///   The <see cref="StateCache" /> used by the model.
		/// </summary>
		private readonly StateCache _stateCache;

		/// <summary>
		///   Initializes a new instance.
		/// </summary>
		/// <param name="rootComponents">The root components of the model.</param>
		/// <param name="objectTable">The table of objects referenced by the model.</param>
		/// <param name="stateFormulas">The state formulas of the model.</param>
		internal RuntimeModel(Component[] rootComponents, ObjectTable objectTable, StateFormula[] stateFormulas)
		{
			Requires.NotNull(rootComponents, nameof(rootComponents));
			Requires.NotNull(objectTable, nameof(objectTable));
			Requires.NotNull(stateFormulas, nameof(stateFormulas));

			// Create a local object table just for the objects referenced by the model; only these objects
			// have to be serialized and deserialized. The local object table does not contain, for instance,
			// the closure types of the state formulas
			var objects = rootComponents.SelectMany(obj => SerializationRegistry.Default.GetReferencedObjects(obj, SerializationMode.Optimized));
			var localObjectTable = new ObjectTable(objects);

			RootComponents = rootComponents;
			StateFormulas = stateFormulas;
			StateSlotCount = SerializationRegistry.Default.GetStateSlotCount(localObjectTable, SerializationMode.Optimized);

			_deserialize = SerializationRegistry.Default.CreateStateDeserializer(localObjectTable, SerializationMode.Optimized);
			_serialize = SerializationRegistry.Default.CreateStateSerializer(localObjectTable, SerializationMode.Optimized);

			_stateCache = new StateCache(StateSlotCount);
			_choiceResolver = new ChoiceResolver(objectTable);

			PortBinding.BindAll(objectTable);
		}

		/// <summary>
		///   Gets the number of slots in the state vector.
		/// </summary>
		internal int StateSlotCount { get; }

		/// <summary>
		///   Gets the root components of the model.
		/// </summary>
		public Component[] RootComponents { get; }

		/// <summary>
		///   Gets the state labels of the model.
		/// </summary>
		internal StateFormula[] StateFormulas { get; }

		/// <summary>
		///   Gets the number of transition groups.
		/// </summary>
		internal int TransitionGroupCount => 1;

		/// <summary>
		///   Deserializes the model's state from <paramref name="serializedState" />.
		/// </summary>
		/// <param name="serializedState">The state of the model that should be deserialized.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void Deserialize(int* serializedState)
		{
			_deserialize(serializedState);
		}

		/// <summary>
		///   Serializes the model's state to <paramref name="serializedState" />.
		/// </summary>
		/// <param name="serializedState">The memory region the model's state should be serialized into.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void Serialize(int* serializedState)
		{
			_serialize(serializedState);
		}

		/// <summary>
		///   Updates the state of the model by executing a single step.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ExecuteStep()
		{
			foreach (var component in RootComponents)
				component.Update();
		}

		/// <summary>
		///   Checks whether the state expression identified by the <paramref name="label" /> holds for the model's current state.
		/// </summary>
		/// <param name="serializedState">The state of the model that should be used to check the <paramref name="label" />.</param>
		/// <param name="label">The label that should be checked.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal bool CheckStateLabel(int* serializedState, int label)
		{
			Deserialize(serializedState);
			return StateFormulas[label].Expression();
		}

		/// <summary>
		///   Gets the serialized initial state of the model.
		/// </summary>
		internal int* GetInitialState()
		{
			var state = _stateCache.Allocate();
			Serialize(state);

			return state;
		}

		/// <summary>
		///   Computes the next states for <paramref name="sourceState" />.
		/// </summary>
		/// <param name="sourceState">The source state the next states should be computed for.</param>
		/// <param name="transitionGroup">The transition group the next states should be computed for.</param>
		internal StateCache ComputeNextStates(int* sourceState, int transitionGroup)
		{
			_stateCache.Clear();
			_choiceResolver.PrepareNextState();

			while (_choiceResolver.PrepareNextPath())
			{
				Deserialize(sourceState);
				ExecuteStep();

				var targetState = _stateCache.Allocate();
				Serialize(targetState);
			}

			return _stateCache;
		}

		/// <summary>
		///   Disposes the object, releasing all managed and unmanaged resources.
		/// </summary>
		/// <param name="disposing">If true, indicates that the object is disposed; otherwise, the object is finalized.</param>
		protected override void OnDisposing(bool disposing)
		{
			_stateCache.SafeDispose();
			_choiceResolver.SafeDispose();
		}
	}
}