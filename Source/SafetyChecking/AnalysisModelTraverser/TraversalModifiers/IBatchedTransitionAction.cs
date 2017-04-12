// The MIT License (MIT)
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

namespace ISSE.SafetyChecking.AnalysisModelTraverser
{
	using AnalysisModel;
	using ExecutableModel;


	/// <summary>
	///   Represents an action that is executed when a batch of new, valid transitions is found during model traversal.
	/// </summary>
	internal interface IBatchedTransitionAction<TExecutableModel> where TExecutableModel : ExecutableModel<TExecutableModel>
	{
		/// <summary>
		///   Processes the new <paramref name="transitions" /> discovered by the <paramref name="worker " /> within the traversal
		///   <paramref name="context" />. Only transitions with <see cref="CandidateTransition.IsValid" /> set to <c>true</c> are
		///   actually new.
		/// </summary>
		/// <param name="context">The context of the model traversal.</param>
		/// <param name="worker">The worker that found the transition.</param>
		/// <param name="sourceState">The index of the transition's source state.</param>
		/// <param name="transitions">The new transitions that should be processed.</param>
		/// <param name="transitionCount">The actual number of valid transitions.</param>
		/// <param name="areInitialTransitions">
		///   Indicates whether the transitions are an initial transitions not starting in any valid source state.
		/// </param>
		void ProcessTransitions(TraversalContext<TExecutableModel> context, Worker<TExecutableModel> worker, int sourceState,
								TransitionCollection transitions, int transitionCount, bool areInitialTransitions);
	}
}