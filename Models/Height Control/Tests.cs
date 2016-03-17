﻿// The MIT License (MIT)
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

namespace SafetySharp.CaseStudies.HeightControl
{
	using System;
	using Analysis;
	using Modeling;
	using NUnit.Framework;

	[TestFixture]
	public class Tests
	{
		public static void Main()
		{
			new Tests().Test();
		}

		[Test]
		public void CollisionDcca()
		{
			var specification = new Specification();
			var analysis = new SafetyAnalysis();

			var result = analysis.ComputeMinimalCriticalSets(specification, specification.Collision);
			result.SaveCounterExamples("counter examples/elbtunnel/");

			Console.WriteLine(result);
		}

		[Test]
		public void Test()
		{
			var model = new Specification();
			var faults = model.Faults;

			for (var i = 0; i < faults.Length; ++i)
//				 faults[i].Activation = i < 5 ? Activation.Nondeterministic : Activation.Suppressed;
//				 faults[i].Activation = Activation.Suppressed;
				faults[i].Activation = Activation.Nondeterministic;

			var checker = new SSharpChecker();
			checker.CheckInvariant(model, true);

//			var ltsMin = new LtsMin();
//			ltsMin.CheckInvariant(model, true);
		}
	}
}