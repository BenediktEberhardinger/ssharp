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

namespace SafetySharp.CaseStudies.HemodialysisMachine.Modeling.ExtracorporealBloodCircuit
{
	using ISSE.SafetyChecking.Modeling;
	using SafetySharp.Modeling;

	public class VenousTubingValve : Component
	{
		// HACK: To be able to react in time we delay the BloodFlow
		public readonly BloodFlowInToOut MainFlow = new BloodFlowInToOut();

		public ValveState ValveState = ValveState.Open;

		public BufferedBlood DelayedBlood = new BufferedBlood();

		[Provided]
		public Blood SetMainFlow(Blood fromPredecessor)
		{
			if (ValveState == ValveState.Open)
			{
				Blood toSuccessor = DelayedBlood;
				DelayedBlood=fromPredecessor;
				return toSuccessor;
			}
			else
			{
				Blood toSuccessor = DelayedBlood;
				toSuccessor.HasHeparin = true;
				toSuccessor.Water = 0;
				toSuccessor.SmallWasteProducts = 0;
				toSuccessor.BigWasteProducts = 0;
				toSuccessor.ChemicalCompositionOk = true;
				toSuccessor.GasFree = true;
				toSuccessor.Pressure = QualitativePressure.NoPressure;
				toSuccessor.Temperature = QualitativeTemperature.BodyHeat;
				return toSuccessor;
			}
		}

		[Provided]
		public Suction SetMainFlowSuction(Suction fromSuccessor)
		{
			return fromSuccessor;
		}

		public virtual void CloseValve()
		{
			ValveState = ValveState.Closed;
		}

		public VenousTubingValve()
		{
			MainFlow.UpdateBackward=SetMainFlowSuction;
			MainFlow.UpdateForward=SetMainFlow;
		}

		public readonly Fault ValveDoesNotClose = new PermanentFault();

		[FaultEffect(Fault = nameof(ValveDoesNotClose))]
		public class ValveDoesNotCloseEffect : VenousTubingValve
		{
			public override void CloseValve()
			{
			}
		}
	}
}