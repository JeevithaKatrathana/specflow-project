using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//context injection
namespace SpecFlowProject1
{
    public class TestContext
    {
        public string applicationPath { get; set; }
        public Window mainWindow { get; set; }
        public Application application { get; set; }
        public string scenarioResult { get; set; }

    }
   
}
