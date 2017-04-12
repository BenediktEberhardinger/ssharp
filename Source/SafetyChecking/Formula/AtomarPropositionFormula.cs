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

namespace ISSE.SafetyChecking.Formula
{
	using System;

	/// <summary>
	///   Represents a state formula, i.e., a Boolean expression that is evaluated in a single system state.
	/// </summary>
	public class AtomarPropositionFormula : Formula
	{
		/// <summary>
		///   Initializes a new instance of the <see cref="AtomarPropositionFormula" /> class.
		/// </summary>
		/// <param name="label">
		///   The name that should be used for the state label of the formula. If <c>null</c>, a unique name is generated.
		/// </param>
		internal AtomarPropositionFormula(string label = null)
		{
			Label = label ?? "StateFormula" + Guid.NewGuid().ToString().Replace("-", String.Empty);
		}
		
		/// <summary>
		///   Gets the state label that a model checker can use to determine whether the state formula holds.
		/// </summary>
		public string Label { get; }

		/// <summary>
		///   Executes the <paramref name="visitor" /> for this formula.
		/// </summary>
		/// <param name="visitor">The visitor that should be executed.</param>
		internal override void Visit(FormulaVisitor visitor)
		{
			visitor.VisitAtomarPropositionFormula(this);
		}
	}
}