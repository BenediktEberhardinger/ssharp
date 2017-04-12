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

namespace ISSE.SafetyChecking.FaultMinimalKripkeStructure
{
	using ExecutableModel;
	using AnalysisModel;
	using AnalysisModelTraverser;

	/// <summary>
	///   Checks for invariant violations during model traversal.
	/// </summary>
	internal sealed class InvariantViolationByIndexAction<TExecutableModel> : ITransitionAction<TExecutableModel> where TExecutableModel : ExecutableModel<TExecutableModel>
	{
		private readonly int _formulaIndex;

		/// <summary>
		///   Initializes a new instance.
		/// </summary>
		/// <param name="formulaIndex">The zero-based index of the analyzed formula.</param>
		public InvariantViolationByIndexAction(int formulaIndex)
		{
			_formulaIndex = formulaIndex;
		}

		/// <summary>
		///   Processes the new <paramref name="transition" /> discovered by the <paramref name="worker " /> within the traversal
		///   <paramref name="context" />.
		/// </summary>
		/// <param name="context">The context of the model traversal.</param>
		/// <param name="worker">The worker that found the transition.</param>
		/// <param name="transition">The new transition that should be processed.</param>
		/// <param name="isInitialTransition">
		///   Indicates whether the transition is an initial transition not starting in any valid source state.
		/// </param>
		public unsafe void ProcessTransition(TraversalContext<TExecutableModel> context, Worker<TExecutableModel> worker, Transition* transition, bool isInitialTransition)
		{
			if (transition->Formulas[_formulaIndex])
				return;

			context.FormulaIsValid = false;
			context.LoadBalancer.Terminate();
			worker.CreateCounterExample(endsWithException: false, addAdditionalState: false);
		}
	}
}