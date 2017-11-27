// The MIT License (MIT)
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

namespace SafetySharp.CaseStudies.RobotCell.Modeling.Controllers.Reconfiguration
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using SafetySharp.Modeling;
    using Odp;
    using Odp.Reconfiguration;

    public class ConfigUpdateLogger : IController
    {
        private readonly IController _controller;
        private readonly Model _model;

        [NonDiscoverable, Hidden(HideElements = true)]
        private readonly StreamWriter _sw;

        [Hidden]
        private bool _namesWritten;

        [Hidden]
        private int counter = 0;

        public ConfigUpdateLogger(IController controller, Model model)
        {
            _controller = controller;
            _model = model;
            var filePath = "testFile" + model.Name +  ".txt";
            _sw = File.AppendText(filePath);
        }

        public BaseAgent[] Agents => _controller.Agents;

        public async Task<ConfigurationUpdate> CalculateConfigurations(object context, ITask task)
        {
            /*if (_namesWritten == false)
            {
                WriteFaultNames();
                _namesWritten = true;
            }*/
            
            var config = await _controller.CalculateConfigurations(context, task);

            var agentRoles = "";
            foreach (var agent in Agents)
            {
                agentRoles += "<" + agent.ID + ":";
                agentRoles = agent.AllocatedRoles == null || !agent.AllocatedRoles.Any() ? agentRoles + "( No Role )" :
                    agent.AllocatedRoles.Aggregate(agentRoles, (current, role) => current + "( " + role.ToString() + ")");
                agentRoles += ">, ";
            }
            _sw.Write(agentRoles);

            WriteFaultActivations(config);

            return config;
        }

        public event Action<ITask, ConfigurationUpdate> ConfigurationsCalculated
        {
            add { _controller.ConfigurationsCalculated += value; }
            remove { _controller.ConfigurationsCalculated -= value; }
        }

        private void WriteFaultActivations(ConfigurationUpdate config)
        {
          
            foreach (var t in _model.Faults)
            {   
                _sw.Write(t.IsActivated ? "1" : "0");
                _sw.Write(", ");
            }
            _sw.Write(config.ToString());
            
            _sw.WriteLine("");
            _sw.Flush();
        }

        private void WriteFaultNames()
        {
            foreach (var t in _model.Faults)
            {
                _sw.Write(t.Name);
                _sw.Write(", ");
                _sw.Flush();
            }
            _sw.WriteLine("");
            _sw.Flush();
        }
    }
}