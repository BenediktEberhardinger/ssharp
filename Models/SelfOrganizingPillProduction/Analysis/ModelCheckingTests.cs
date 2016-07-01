﻿using NUnit.Framework;
using System.Linq;
using SafetySharp.Analysis;
using SafetySharp.Modeling;
using SafetySharp.CaseStudies.SelfOrganizingPillProduction.Modeling;
using SafetySharp.Analysis.Heuristics;
using static SafetySharp.Analysis.Operators;

namespace SafetySharp.CaseStudies.SelfOrganizingPillProduction.Analysis
{
    public class ModelCheckingTests
    {
        public enum HeuristicsUsage { None, Subsumption, Redundancy, Both }

        [Test]
        public void ForcedActivationIncompleteDcca(
            [Values(FaultActivationBehaviour.ForceOnly, FaultActivationBehaviour.Nondeterministic)]
            FaultActivationBehaviour activation
        )
        {
            // create stations
            var producer = new ContainerLoader();
            var commonDispenser = new ParticulateDispenser();
            var dispenserTop = new ParticulateDispenser();
            var dispenserBottom = new ParticulateDispenser();
            var consumerTop = new PalletisationStation();
            var consumerBottom = new PalletisationStation();
            var stations = new Station[] { producer, commonDispenser, dispenserTop, dispenserBottom, consumerTop, consumerBottom };

            // set very limited ingredient amounts
            commonDispenser.SetStoredAmount(IngredientType.BlueParticulate, 40);
            dispenserTop.SetStoredAmount(IngredientType.RedParticulate, 100);
            dispenserBottom.SetStoredAmount(IngredientType.RedParticulate, 100);

            // create connections
            producer.Outputs.Add(commonDispenser);
            commonDispenser.Inputs.Add(producer);

            commonDispenser.Outputs.Add(dispenserTop);
            dispenserTop.Inputs.Add(commonDispenser);
            commonDispenser.Outputs.Add(dispenserBottom);
            dispenserBottom.Inputs.Add(commonDispenser);

            dispenserTop.Outputs.Add(consumerTop);
            consumerTop.Inputs.Add(dispenserTop);

            dispenserBottom.Outputs.Add(consumerBottom);
            consumerBottom.Inputs.Add(dispenserBottom);

            var model = new Model(
                stations,
                new FastObserverController(stations)
            );

            var recipe = new Recipe(
                new[] { new Ingredient(IngredientType.BlueParticulate, 30), new Ingredient(IngredientType.RedParticulate, 10) },
                1u
            );
            model.ScheduleProduction(recipe);

            Dcca(model, activation);
        }

        [Test, Combinatorial]
        public void DccaTest(
            [Values("complete_network.model", "bidirectional_circle.model", "duplicate_dispenser.model", "simple_circle.model", "trivial_setup.model", "simple_setup.model", "simple_setup3.model", "simple_setup2.model", "medium_setup.model", "complex_setup.model")]
            string modelFile,
            [Values(HeuristicsUsage.None, HeuristicsUsage.Subsumption, HeuristicsUsage.Redundancy, HeuristicsUsage.Both)]
            HeuristicsUsage heuristicsUsage,
            [Values(FaultActivationBehaviour.ForceOnly, FaultActivationBehaviour.ForceThenFallback, FaultActivationBehaviour.Nondeterministic)]
            FaultActivationBehaviour faultActivation
        )
        {
            var model = new ModelSetupParser().Parse($"Analysis/{modelFile}");
            IFaultSetHeuristic[] heuristics;
            switch (heuristicsUsage)
            {
                case HeuristicsUsage.None:
                    heuristics = new IFaultSetHeuristic[0];
                    break;
                case HeuristicsUsage.Subsumption:
                    heuristics = new[] { new SubsumptionHeuristic(model) };
                    break;
                case HeuristicsUsage.Redundancy:
                    heuristics = new[] { RedundancyHeuristic(model) };
                    break;
                default:
                case HeuristicsUsage.Both:
                    heuristics = new[] { new SubsumptionHeuristic(model), RedundancyHeuristic(model) };
                    break;
            }
            Dcca(model, faultActivation, heuristics);
        }

        private void Dcca(Model model, FaultActivationBehaviour activation, params IFaultSetHeuristic[] heuristics)
        {
            var modelChecker = new SafetyAnalysis();

            modelChecker.Configuration.StateCapacity = 1 << 16;
            modelChecker.Configuration.CpuCount = 4;
            modelChecker.Configuration.GenerateCounterExample = false;
            modelChecker.Heuristics.AddRange(heuristics);
            modelChecker.FaultActivationBehaviour = activation;

            var result = modelChecker.ComputeMinimalCriticalSets(model, model.ObserverController.Unsatisfiable);
            System.Console.WriteLine(result);
            Assert.AreEqual(0, result.Exceptions.Count);
        }

        private IFaultSetHeuristic RedundancyHeuristic(Model model)
        {
            return new MinimalRedundancyHeuristic(
                model,
                model.Stations.OfType<ContainerLoader>().Select(c => c.NoContainersLeft),
                model.Stations.OfType<ParticulateDispenser>().Select(d => d.BlueTankDepleted),
                model.Stations.OfType<ParticulateDispenser>().Select(d => d.RedTankDepleted),
                model.Stations.OfType<ParticulateDispenser>().Select(d => d.YellowTankDepleted),
                model.Stations.OfType<PalletisationStation>().Select(p => p.PalletisationDefect)
            );
        }

        [Test]
        public void EnumerateAllStates()
        {
            var model = new ModelSetupParser().Parse("Analysis/medium_setup.model");
            model.Faults.SuppressActivations();

            var checker = new SSharpChecker { Configuration = { StateCapacity = 1 << 18 } };
            var result = checker.CheckInvariant(model, true);

            System.Console.WriteLine(result.StateVectorLayout);
        }

        [Test]
        public void ProductionCompletesIfNoFaultsOccur()
        {
            var model = Model.NoRedundancyCircularModel();
            var recipe = new Recipe(new[]
            {
                new Ingredient(IngredientType.BlueParticulate, 1),
                new Ingredient(IngredientType.RedParticulate, 3),
                new Ingredient(IngredientType.BlueParticulate, 1)
            }, 6);
            model.ScheduleProduction(recipe);
            model.Faults.SuppressActivations();

            var result = ModelChecker.Check(model, F(recipe.ProcessingComplete));
            Assert.That(result.FormulaHolds, "Recipe production never finishes");
        }
    }
}