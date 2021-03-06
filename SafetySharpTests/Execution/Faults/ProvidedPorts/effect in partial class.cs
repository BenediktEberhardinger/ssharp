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

namespace Tests.Execution.Faults.ProvidedPorts
{
	using ISSE.SafetyChecking.Modeling;
	using SafetySharp.Modeling;
	using Shouldly;
	using Utilities;

	internal class PartialClass : TestModel
	{
		protected sealed override void Check()
		{
			Create(new D());
			var d = (D)RootComponents[0];

			d.F1.Activation = Activation.Forced;
			d.F2.Activation = Activation.Forced;
			d.M().ShouldBe(1100);
			d.N().ShouldBe(2222);

			d.F1.Activation = Activation.Suppressed;
			d.F2.Activation = Activation.Forced;
			d.M().ShouldBe(1100);
			d.N().ShouldBe(2202);

			d.F1.Activation = Activation.Forced;
			d.F2.Activation = Activation.Suppressed;
			d.M().ShouldBe(100);
			d.N().ShouldBe(222);

			d.F1.Activation = Activation.Suppressed;
			d.F2.Activation = Activation.Suppressed;
			d.M().ShouldBe(100);
			d.N().ShouldBe(202);
		}

		private abstract class C : Component
		{
			public readonly TransientFault F1 = new TransientFault();

			public abstract int M();
			public virtual int N() => 2;

			[FaultEffect(Fault = nameof(F1))]
			public abstract class F1Effect : C
			{
				public override int N() => 20 + base.N();
			}
		}

		private partial class D : C
		{
			public readonly TransientFault F2 = new TransientFault();

			public override int M() => 100;
			public override int N() => 200 + base.N();
		}

		private partial class D
		{
			[FaultEffect(Fault = nameof(F2))]
			public class F2Effect : D
			{
				public override int M() => base.M() + 1000;
				public override int N() => base.N() + 2000;
			}
		}
	}
}