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
	using ExecutableModel;

	/// <summary>
	///   Represents an action that is executed when a new, reachable state is found during model traversal.
	/// </summary>
	internal unsafe interface IStateAction<TExecutableModel> where TExecutableModel : ExecutableModel<TExecutableModel>
	{
		/// <summary>
		///   Processes the new <paramref name="state" /> discovered by the <paramref name="worker " /> within the traversal
		///   <paramref name="context" />.
		/// </summary>
		/// <param name="context">The context of the model traversal.</param>
		/// <param name="worker">The worker that found the transition.</param>
		/// <param name="state">The new state that should be handled.</param>
		/// <param name="stateIndex">The unique index of the new state.</param>
		/// <param name="isInitial">Indicates whether the state is an initial state.</param>
		void ProcessState(TraversalContext<TExecutableModel> context, Worker<TExecutableModel> worker, byte* state, int stateIndex, bool isInitial);
	}
}