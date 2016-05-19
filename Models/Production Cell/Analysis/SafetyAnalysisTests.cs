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

namespace SafetySharp.CaseStudies.ProductionCell.Analysis
{
	using System;
	using System.Linq;
	using Modeling;
	using Modeling.Controllers;
	using NUnit.Framework;
	using SafetySharp.Analysis;
	using SafetySharp.Modeling;

	public class SafetyAnalysisTests
	{
		[Test]
		public void DamagedWorkpieces()
		{
			var model = new Model();
			var safetyAnalysis = new SafetyAnalysis { Configuration = { CpuCount = 1, StateCapacity = 1 << 16 } };
			var result = safetyAnalysis.ComputeMinimalCriticalSets(model, model.Workpieces.Any(w => w.IsDamaged));

			Console.WriteLine(result);
		}

		[Test]
		public void ReconfigurationFailed()
		{
			var model = new Model();

			foreach (var robot in model.Robots)
				robot.ResourceTransportFault.SuppressActivation();

			var safetyAnalysis = new SafetyAnalysis { Configuration = { CpuCount = 1, StateCapacity = 1 << 16 } };
			var result = safetyAnalysis.ComputeMinimalCriticalSets(model, model.ObserverController.ReconfigurationFailed);

			Console.WriteLine(result);
		}

		[Test]
		public void InvariantViolation()
		{
			var model = new Model();

			

			var safetyAnalysis = new SafetyAnalysis { Configuration = { CpuCount = 1, StateCapacity = 1 << 16 } };
			var result = safetyAnalysis.ComputeMinimalCriticalSets(model, Hazard(model));

			Console.WriteLine(result);
		}

		private bool Hazard(Model model)
		{
			var agents = model.CartAgents.Cast<Agent>().Concat(model.RobotAgents).ToArray();

			if (model.ObserverController.ReconfigurationFailed)
				return false;

			foreach (var agent in agents)
			{
				foreach (var constraint in agent.Constraints)
				{
					if (!constraint())
						return true;
				}
			}

			return false;
		}
	}
}