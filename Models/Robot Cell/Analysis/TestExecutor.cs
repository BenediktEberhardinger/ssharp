using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafetySharp.CaseStudies.RobotCell.Analysis
{
    using System.Collections;
    using System.Reflection;
    using JetBrains.Annotations;
    using Modeling;
    using Modeling.Controllers;
    using Modeling.Controllers.Reconfiguration;
    using NUnit.Framework;
    using SafetySharp.Analysis;
    using SafetySharp.Modeling;

    class TestExecutor
    {

        [Test, TestCaseSource(nameof(StandardConfiguration))]
        public void SimulateTestInput(Model model, List<List<Fault>> testCases)
        {
            model.Faults.SuppressActivations();
            var simulator = new Simulator(model);
            foreach (var testSet in  testCases)
            {
                foreach (var fault in testSet)
                {
                    fault.ForceActivation();
                }
                simulator.SimulateStep();
            }
        }

        private static IEnumerable StandardConfiguration()
        {
            return SampleModels.CreateDefaultConfigurations<FastController>()
            .Select(model => new TestCaseData(model, ImportTestCases(model)).SetName(model.Name)).ToList();
        }

        private static List<List<Fault>> ImportTestCases(Model model)
        {
            string[][] dummyList = 
            {
                new string[] { "R268.ConfigurationUpdateFailed" }
            };
            var availableFaults = CollectFaults(model);
            var result = new List<List<Fault>>();
            foreach (var testcase in dummyList)
            {
                result.Add(availableFaults.Where(fault => testcase.Any(name => fault.Item1.Name.Equals(name))).Select(tuple => tuple.Item1).ToList());
            }
            
            return result;
        }

        private static Tuple<Fault, IComponent>[] CollectFaults(Model model)
        {
            var faultInfo = new HashSet<Tuple<Fault, IComponent>>();
            model.VisitPostOrder(component =>
            {
                var faultFields =
                    from faultField in component.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic)
                    where typeof(Fault).IsAssignableFrom(faultField.FieldType) && (Fault)faultField.GetValue(component) != null
                    select Tuple.Create((Fault)faultField.GetValue(component), component);

                foreach (var info in faultFields)
                    faultInfo.Add(info);
            });
            return faultInfo.ToArray();
        }
    }
}