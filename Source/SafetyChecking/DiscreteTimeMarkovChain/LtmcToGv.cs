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


namespace ISSE.SafetyChecking.DiscreteTimeMarkovChain
{
	using System.Globalization;
	using System.IO;
	using ISSE.SafetyChecking.Modeling;

	internal static class LtmcToGv
	{
		
		private static void ExportDistribution(LabeledTransitionMarkovChain markovChain, TextWriter sb, string sourceStateName, LabeledTransitionMarkovChain.LabeledTransitionEnumerator distribution)
		{
			while (distribution.MoveNext())
			{
				sb.Write($"{sourceStateName} -> {distribution.CurrentTargetState} [label=\"{Probability.PrettyPrint(distribution.CurrentProbability)}");

				for (int i = 0; i < markovChain.StateFormulaLabels.Length; i++)
				{
					if (i==0)
						sb.Write("\\n");
					else
						sb.Write(",");
					if (distribution.CurrentFormulas[i])
						sb.Write("t");
					else
						sb.Write("f");
				}
				sb.WriteLine("\"];");
			}
		}

		public static void ExportToGv(this LabeledTransitionMarkovChain markovChain, TextWriter sb)
		{
			sb.WriteLine("digraph S {");
			//sb.WriteLine("size = \"8,5\"");
			sb.WriteLine("node [shape=box];");

			var initialStateName = "initialState";
			sb.WriteLine($" {initialStateName} [shape=point,width=0.0,height=0.0,label=\"\"];");
			var initialDistribution = markovChain.GetInitialDistributionEnumerator();
			ExportDistribution(markovChain, sb, initialStateName, initialDistribution);

			foreach (var state in markovChain.SourceStates)
			{

				sb.Write($" {state} [label=\"{state}");
				sb.WriteLine("\"];");

				var distribution = markovChain.GetTransitionEnumerator(state);
				ExportDistribution(markovChain, sb, state.ToString(), distribution);

			}
			sb.WriteLine("}");
		}
	}
}
