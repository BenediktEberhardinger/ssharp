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

namespace ISSE.SafetyChecking.AnalysisModel
{
	using ExecutableModel;

	/// <summary>
	///   Describes the result of a model checking based analysis.
	/// </summary>
	public sealed class AnalysisResult<TExecutableModel> where TExecutableModel : ExecutableModel<TExecutableModel>
	{
		/// <summary>
		///   Gets the counter example that has been generated by the model checker, if any.
		/// </summary>
		public CounterExample<TExecutableModel> CounterExample { get; internal set; }

		/// <summary>
		///   Gets a value indicating whether the analyzed formula holds.
		/// </summary>
		public bool FormulaHolds { get; internal set; }

		/// <summary>
		///   Gets the number of states checked by the model checker.
		/// </summary>
		public int StateCount { get; internal set; }

		/// <summary>
		///   Gets the number of activation-minimal transitions checked by the model checker.
		/// </summary>
		public long TransitionCount { get; internal set; }

		/// <summary>
		///   Gets the number of computed transitions checked by the model checker.
		/// </summary>
		internal long ComputedTransitionCount { get; set; }

		/// <summary>
		///   Gets the number of levels checked by the model checker.
		/// </summary>
		public int LevelCount { get; internal set; }
	}
}