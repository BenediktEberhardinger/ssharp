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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.MarkovDecisionProcess.Optimized
{
	using ISSE.SafetyChecking.MarkovDecisionProcess.Optimized;
	using System.Diagnostics;
	using ISSE.SafetyChecking.Formula;
	using JetBrains.Annotations;
	using Utilities;
	using Xunit;
	using Xunit.Abstractions;
	using Shouldly;
	using Examples;
	public class BuiltinNmdpModelCheckerTests
	{
		/// <summary>
		///   Gets the output that writes to the test output stream.
		/// </summary>
		public TestTraceOutput Output { get; }

		[UsedImplicitly]
		public static IEnumerable<object[]> DiscoverTests()
		{
			foreach (var example in AllExamples.Examples)
			{
				yield return new object[] { example };// only one parameter
			}
		}

		public BuiltinNmdpModelCheckerTests(ITestOutputHelper output)
		{
			Output = new TestTraceOutput(output);
		}

		[Theory, MemberData(nameof(DiscoverTests))]
		public void MinimalProbabilityToReachIn50Steps_Label1(NestedMarkovDecisionProcessExample example)
		{
			var nmdp = example.Nmdp;
			var steps = 50;

			var finallyLabel1 = new BoundedUnaryFormula(NestedMarkovDecisionProcessExample.Label1Formula, UnaryOperator.Finally, steps);

			using (var checker = new BuiltinNmdpModelChecker(nmdp, Output.TextWriterAdapter()))
			{
				var result = checker.CalculateMinimalProbability(finallyLabel1);
				result.Is(example.MinimalProbabilityFinallyLabel1, 0.0001).ShouldBe(true);
			}
		}

		[Theory, MemberData(nameof(DiscoverTests))]
		public void MaximalProbabilityToReachIn50Steps_Label1(NestedMarkovDecisionProcessExample example)
		{
			var nmdp = example.Nmdp;
			var steps = 50;

			var finallyLabel1 = new BoundedUnaryFormula(NestedMarkovDecisionProcessExample.Label1Formula, UnaryOperator.Finally, steps);

			using (var checker = new BuiltinNmdpModelChecker(nmdp, Output.TextWriterAdapter()))
			{
				var result = checker.CalculateMaximalProbability(finallyLabel1);
				result.Is(example.MaximalProbabilityFinallyLabel1, 0.0001).ShouldBe(true);
			}
		}
	}
}
