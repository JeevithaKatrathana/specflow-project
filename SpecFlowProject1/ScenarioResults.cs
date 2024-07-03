using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpecFlowProject1
{
    [Binding]
    public class ScenarioResults
    {
        TestContext _testContext;
        string ?caption;
        public ScenarioResults(TestContext testContext)
        {
            _testContext = testContext;
        }

        [Then(@"Display testRun status")]
        public void ThenDisplayTestRunStatus()
        {
           
            caption = "Testcase status";
            //  DisplayInMessagebox();
            Console.WriteLine(caption + " :  " + _testContext.scenarioResult);
        }
        [Then(@"Display File content")]
        public void ThenDisplayFileContent()
        {
            caption = "Notepad content";
            // DisplayInMessagebox();
            Console.WriteLine(caption + " :  " + _testContext.scenarioResult);
        }
        public void DisplayInMessagebox()
        {
            string message = _testContext.scenarioResult;
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            MessageBox.Show(message, caption, buttons);
        }
        [Then(@"Display testRun status for data(.*)")]
        public void ThenDisplayTestRunStatusForData(string testdataname)
        {
            caption = "Testcase status for " + testdataname;
            //  DisplayInMessagebox();
            Console.WriteLine(caption + " :  "+ _testContext.scenarioResult);
        }

    }
}
