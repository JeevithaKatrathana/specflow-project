using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.UIA3;
using FlaUI.UIA2;
using FlaUI.Core.Input;
using NUnit.Framework;
using System.Diagnostics;
using TechTalk.SpecFlow;
using FlaUI.Core.Definitions;
using System.Windows.Automation;
using FlaUI.Core.WindowsAPI;
using TechTalk.SpecFlow.CommonModels;
using TechTalk.SpecFlow.Assist;
using System.Text.Json.Nodes;
using Newtonsoft.Json;
using System.Windows.Interop;
using Gherkin.CucumberMessages.Types;
using LivingDoc.Dtos;
using Microsoft.VisualBasic.ApplicationServices;
using SpecFlowProject1.TestDataSupportFolder;
using System.Reflection;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Engine;
using System;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;



//using System.Net.Security;





namespace SpecFlowProject1.StepDefinitions
{
    [Binding]
    public sealed class CalculatorStepDefinitions
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        ConditionFactory cf = new ConditionFactory(new UIA3PropertyLibrary());
        //  Window? mainWindow = null; without using context injection
        int num1, num2, num3, num4, calculatorResult, expectedresult;
        string testdataname = string.Empty;
        TestContext _testContext;

        public CalculatorStepDefinitions(TestContext testContext)
        {
            _testContext = testContext;
        }
        public int iNum
        {
            set { num1 = value; }//reserved keyword
            get { return num1; }
        }

        [Given(@"I  Launch Calculator")]
        public void GivenILaunchCalculator()
        {
            #region Failed attempts to launch calculator
            /* //"C:\\Program Files\\WindowsApps\\Microsoft.WindowsCalculator_11.2405.2.0_x64__8wekyb3d8bbwe\\calculatorApp.exe"
             //  var processStartInfo = new ProcessStartInfo(@"calc.exe"); //parameter to attachOrLaunch application method
             // var application = Application.AttachOrLaunch(processStartInfo);//launched application but failed to get mainwindow
             // var application = Application.Launch("calc.exe"); //launched application but failed to get mainwindow*/
            #endregion
            _testContext.applicationPath = "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App";
            _testContext.application = Application.LaunchStoreApp(_testContext.applicationPath);
            // mainWindow = application.GetMainWindow(new UIA3Automation()); //without using context injection
            _testContext.mainWindow = _testContext.application.GetMainWindow(new UIA3Automation());

            #region Attach calculator if its open else launch calculator

           /*  //  _testContext.applicationPath = "calc.exe";
             var process = System.Diagnostics.Process.GetProcessesByName("CalculatorApp").FirstOrDefault();
              if (process == null)
              {
                 // _testContext.application = Application.Attach(process);
                
                _testContext.applicationPath = "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App";
                _testContext.application = Application.LaunchStoreApp(_testContext.applicationPath);
                _testContext.mainWindow = _testContext.application.GetMainWindow(new UIA3Automation());
                Console.WriteLine("Launch Calculator app");
                _testContext.mainWindow!.FindFirstDescendant(cf.ByAutomationId("clearButton")).AsButton().Click();
            }
           // _testContext.application = Application.Attach(process); //works 
          // _testContext.mainWindow = _testContext.application.GetMainWindow(new UIA3Automation()); //slows down here //if commented returns null
            Console.WriteLine("Attach Calculator app");*/
            #endregion
        }

        [Given("the first number is (.*)")]
        public void GivenTheFirstNumberIs(int number)
        {

            iNum = number;

        }

        [Given("the second number is (.*)")]
        public void GivenTheSecondNumberIs(int number)
        {
          
            num2 = number;
        }

        [When("the two numbers are added")]
        public void WhenTheTwoNumbersAreAdded()
        {
            #region attempt to find all objects
            // var allelements =  mainWindow!.FindAll(cf.ByAutomationId("num" + num1 + "Button"));
            /* var alldescendents = mainWindow!.FindAllDescendants();
            foreach(var item in alldescendents)
             {
                 item.DrawHighlight();
                 Thread.Sleep(500);
             }*/
            #endregion 
            InputValuesToCalculator(iNum);
            // mainWindow!.FindFirstDescendant(cf.ByAutomationId("plusButton")).AsButton().Click(); //without dependency injection
            _testContext.mainWindow.FindFirstDescendant(cf.ByAutomationId("plusButton")).AsButton().Click();
            InputValuesToCalculator(num2);
            _testContext.mainWindow.FindFirstDescendant(cf.ByAutomationId("equalButton")).AsButton().Click();
          
        }
        public void InputValuesToCalculator(int number)
        {
            var charArray = number.ToString().ToCharArray();
            foreach (var i in charArray)
            {
                _testContext.mainWindow.FindFirstDescendant(cf.ByAutomationId("num" + i + "Button")).AsButton().Click();
            }
        }

        [Then("the result should be (.*)")]
        public void ThenTheResultShouldBe(int result)
        {
            _testContext.scenarioResult = "Failed";
            //TODO: implement assert (verification) logic
            //  var results = _testContext.mainWindow!.FindFirstDescendant(cf.ByAutomationId("CalculatorResults"));
            #region attempts to find child elements of resultfield
            /* var pane = results.FindFirstChild(); //returning null

             var alldescendents = results.FindAllDescendants();
             foreach (var item in alldescendents)
             {
                 item.DrawHighlight();
                 Console.WriteLine(item.Name);
                 Thread.Sleep(500);
             } // didn't return anything 

            //  var pane = mainWindow!.FindFirstChild(cf.ByAutomationId("TextContainer")); //returning null

            // Assert.IsNotNull(pane); //throws exception */

            /* var resultValue = results.Name;
             string[] a = resultValue.Split("Display is");
              var value1 = int.Parse(a[1]); //^1 */
            #endregion 

            calculatorResult = getCalculatorResult();
            Assert.That(result, Is.EqualTo(calculatorResult));
            _testContext.scenarioResult = "Passed";
            Thread.Sleep(300);
           
        }
        public int getCalculatorResult()
        {
            var results = _testContext.mainWindow!.FindFirstDescendant(cf.ByAutomationId("CalculatorResults"));
            var resultValue = results.Name;
            string[] a = resultValue.Split("Display is");

            string newText = a[1];
            if (a[1].Contains(','))
            {
                string[] parts = a[1].Split(',', StringSplitOptions.RemoveEmptyEntries);
                newText = System.String.Join("", parts);

            }

            calculatorResult = int.Parse(newText); //^1
            return calculatorResult;
        }
        [Then(@"Close Calculator")]
        public void ThenCloseCalculator()
        {

            // _testContext.mainWindow!.FindFirstDescendant(cf.ByAutomationId("Close")).AsButton().Click();
             _testContext.application.Close();
        }
        [When(@"the two numbers are subtracted")]
        public void WhenTheTwoNumbersAreSubtracted()
        {
            InputValuesToCalculator(num1);
            _testContext.mainWindow!.FindFirstDescendant(cf.ByAutomationId("minusButton")).AsButton().Click();
            InputValuesToCalculator(num2);
            _testContext.mainWindow!.FindFirstDescendant(cf.ByAutomationId("equalButton")).AsButton().Click();

        }

        #region Notepad example - attach existing else launch new app
        //-----------------------------------------Note pad Scenario-----------------------------------------------------------------------------------
        //note pad program
        [Given(@"I Launch Notepad")]
        public void GivenILaunchNotepad()
        {

            _testContext.applicationPath = "Notepad.exe";
            LaunchOrAttach_Application(_testContext.applicationPath, "notepad");
           
        }

        //launch using attachOrLaunch method
        public void LaunchOrAttach_Application(string applicationPath, string processname)
        {

            var process = System.Diagnostics.Process.GetProcessesByName(processname).FirstOrDefault();
            if (process != null)
            {
                var application = Application.Attach(process);
                _testContext.mainWindow = application.GetMainWindow(new UIA3Automation());
            }
            else
            {

               //  _testContext.application = Application.LaunchStoreApp(applicationPath);
                var processStartInfo = new ProcessStartInfo(applicationPath);
               //_testContext.application = Application.AttachOrLaunch(processStartInfo);
                _testContext.application = Application.Launch(processStartInfo);
                // mainWindow = application.GetMainWindow(new UIA3Automation()); //without using dependency injection
                _testContext.mainWindow = _testContext.application.GetMainWindow(new UIA3Automation());
              
            }

        }

        [Given(@"I type system time")]
        public void GivenITypeSystemTime()
        {
            _testContext.mainWindow.FindFirstDescendant(cf.ByControlType(FlaUI.Core.Definitions.ControlType.Document)).Focus();
            var value = _testContext.mainWindow.FindFirstDescendant(cf.ByControlType(FlaUI.Core.Definitions.ControlType.Document)).AsTextBox().Text.ToString();
            if (!string.IsNullOrEmpty(value))
                Keyboard.Type(" ");

            Keyboard.Type(System.DateTime.Now.ToString());

            _testContext.scenarioResult = _testContext.mainWindow.FindFirstDescendant(cf.ByControlType(FlaUI.Core.Definitions.ControlType.Document)).AsTextBox().Text.ToString();
        }

        #endregion 
        //--------------------------------------------------Scenario outline example ----------------------------------------------


        [Given(@"the third number is (.*)")]
        public void GivenTheThirdNumberIs(int number)
        {
            num3 = number;
        }

        [When(@"the third number subtracted")]
        public void WhenTheThirdNumberIsSubtracted()
        {
            _testContext.mainWindow!.FindFirstDescendant(cf.ByAutomationId("minusButton")).AsButton().Click();
            InputValuesToCalculator(num3);
            _testContext.mainWindow!.FindFirstDescendant(cf.ByAutomationId("equalButton")).AsButton().Click();
        }

        [Given(@"the fourth number is (.*)")]
        public void GivenTheFourthNumberIs(int number)
        {
            num4 = number;
        }

        [When(@"the fourth number multiplied")]
        public void WhenTheFourthNumberIsMultiplied()
        {
            _testContext.mainWindow!.FindFirstDescendant(cf.ByAutomationId("multiplyButton")).AsButton().Click();
            InputValuesToCalculator(num4);
            _testContext.mainWindow!.FindFirstDescendant(cf.ByAutomationId("equalButton")).AsButton().Click();
        }

        // ----------------------------------------------------- using CreateSet ---------------------------------------------------------------------

        [Then(@"I can verify Mixed calculations for the following rows")]
        public void ThenICanVerifyMixedCalculationsForTheFollowingRows(Table table)
        {
            VerifyMixedCalculations(table);
        }

        public void VerifyMixedCalculations(Table table)
        {
            var tablecontent = table.CreateSet<TestdataTable>();
            bool testResult = true;
            foreach (var item in tablecontent)
            {
                testResult = ReceiveTestDataAndPerformMixedCalculations(item) && testResult;
                try
                {
                    Assert.IsTrue(testResult, "Testcase scenario failed for : " + testdataname);

                }
                catch (Exception e)
                {
                    break;

                    // continue; // executes next rows, but fails becuase testResult is false after 1 failure
                }

            }
        }
        public bool ReceiveTestDataAndPerformMixedCalculations(TestdataTable tablerow)
        {
            bool isAsExpected = false;
            var item = tablerow;
            GivenTheFirstNumberIs(item.num1);
            GivenTheSecondNumberIs(item.num2);
            GivenTheThirdNumberIs(item.num3);
            GivenTheFourthNumberIs(item.num4);
            testdataname = item.testdataName;
            PerformMixedArithmeticOperations();
            calculatorResult = getCalculatorResult();

            // Assert.IsTrue(, testdataname);
            try
            {
                Assert.AreEqual(item.result, (calculatorResult), "Actual result not as expected for : " + testdataname);
            }
            catch (Exception)
            {
                Console.WriteLine("exception occured");
                return isAsExpected;
            }

            isAsExpected = true;
            Thread.Sleep(500);
            return isAsExpected;
        }

        public record TestdataTable //using record type . can be public record TestdataTable as well
        {
            public int num1 { set; get; }
            public int num2 { set; get; }
            public int num3 { set; get; }
            public int num4 { set; get; }
            public int result { set; get; }
            public string testdataName { set; get; }

        }

        public void PerformMixedArithmeticOperations()
        {
            WhenTheTwoNumbersAreAdded();
            WhenTheThirdNumberIsSubtracted();
            WhenTheFourthNumberIsMultiplied();
            //  expectedresult = ((num1 + num2) - num3) * num4;
        }

        //-------------------------------------------------using create instance --------------------------------------------

        [Then(@"I can verify Mixed calculations for one row")]
        public void ThenICanVerifyMixedCalculationsForOneRow(Table table)
        {
            #region otherworkarounds
            /*  TestdataTableClass tablecontent = table.CreateInstance<TestdataTableClass>(); // created only for single row
             tablecontent.testdataName //to get data from class */
            /*foreach (TableRow row in table.Rows)  //without using create instance
             {
                 /* foreach(string header in table.Header)
                  {
                      Console.WriteLine(row[header].ToString());
                  }
                 Console.WriteLine(row["num1"].ToString());
                 
             } */
            #endregion
            VerifyMixedCalculations(table);

        }

        //---------------------------------------using Json file as input tesdata----------------------------------------
        #region Parsing Json n reading data
        //class files location:  TestDataSupportFolder 
        //classes created : TestDataSupport, Data, Addition, Subtraction
        [Then(@"I can verify Mixed Calculations using json file")]
        public void ThenICanVerifyMixedCalculationsUsingJsonFile()
        {
            string filepath = @"C:\Users\jeevitha_j\source\repos\SpecFlowProject1\SpecFlowProject1\TestDataSupportFolder\testdatainjson.json";
            parseJson(filepath);
        }

        public void parseJson(string filepath)
        {
            string? filecontent = System.IO.File.ReadAllText(filepath);

            TestDataSupport? testdata = JsonConvert.DeserializeObject<TestDataSupport>(filecontent);
            Data? dt = testdata!.Data;
            AddGivenNumbers(dt!.Addition!);
            SubtractGivenNumbers(dt!.Subtraction!);
          
        }

        public void PerformGivenrithmeticOperation(Addition numbers,string btnName)
        {
            Addition numberstoAdd = numbers;
          /*  int count = numberstoAdd.GetType().GetProperties().Length; // get property count
             PropertyInfo[] all = numberstoAdd.GetType().GetProperties(); //get all properties
          
           for(int i = 0; i < count -2 ; i++)
            {
                EnternumbersinCalculatorAndPerformAction(Convert.ToInt32(all[i].GetValue(numberstoAdd)), btnName);
                Thread.Sleep(100);
            }

            foreach (PropertyInfo prop in all)
            {
                
                 //   Console.WriteLine(prop.GetValue(numberstoAdd, null).ToString());
                
            }*/
            EnternumbersinCalculatorAndPerformAction(numberstoAdd.num1, btnName);
            EnternumbersinCalculatorAndPerformAction(numberstoAdd.num2, btnName);
            EnternumbersinCalculatorAndPerformAction(numberstoAdd.num3, btnName);
            InputValuesToCalculator(numberstoAdd.num4);
            Console.WriteLine(" numbers to add : ");
            _testContext.mainWindow!.FindFirstDescendant(cf.ByAutomationId("equalButton")).AsButton().Click();
            calculatorResult = getCalculatorResult();

            Assert.AreEqual(numberstoAdd.result, (calculatorResult), "Actual result not as expected ");
        }
        public void  AddGivenNumbers(Addition numbers)
        {
            string btnName = "plusButton";
            PerformGivenrithmeticOperation(numbers, btnName);
        }
        public void SubtractGivenNumbers(Addition numbers)
        {
            string btnName = "minusButton";
            PerformGivenrithmeticOperation(numbers, btnName);
        }
        public void EnternumbersinCalculatorAndPerformAction(int num, string btnName)
        {
            InputValuesToCalculator(num);
            _testContext.mainWindow!.FindFirstDescendant(cf.ByAutomationId(btnName)).AsButton().Click();
        }
        #endregion

        //---------------------------------- Screen settings -------------------------------------------
        [Given(@"I Launch Settings screen")]
        public void GivenILaunchSettingsScreen()
        {
             _testContext.applicationPath =@"C:\Windows\ImmersiveControlPanel\SystemSettings";
            // C:\Windows\ImmersiveControlPanel
            // _testContext.application = Application.LaunchStoreApp(_testContext.applicationPath);

            // mainWindow = application.GetMainWindow(new UIA3Automation()); //without using context injection
            //  _testContext.mainWindow = _testContext.application.GetMainWindow(new UIA3Automation());
            //   _testContext.applicationPath = "SystemSettings.exe";
          //  _testContext.applicationPath = @"C:\Users\jeevitha_j\Downloads\BankingApplication\BankingApplication\bin\Debug\BankingApplication";
            LaunchOrAttach_Application(_testContext.applicationPath, "SystemSettings");
          //  LaunchOrAttach_Application(_testContext.applicationPath, "BankingApplication"); 
        }

        [When(@"I Change screen settings")]
        public void WhenIChangeScreenSettings()
        {
           
        }

        #region not used
        /* [Given(@"I input following numbers to Calculator")]
         public void GivenIInputFollowingNumbersToCalculator(Table table)
         {
             var tablecontent = table.CreateSet<TestdataTable>();
             foreach (var item in tablecontent)
             {
                 // Console.Write(item.testdataName.ToString() + " : ");
                 GivenTheFirstNumberIs(item.num1);
                 GivenTheSecondNumberIs(item.num2);
                 GivenTheThirdNumberIs(item.num3);
                 GivenTheFourthNumberIs(item.num4);
                 testdataname = item.testdataName;
                 PerformMixedArithmeticOperations();
                 Console.Write(item.num1 + " + " + item.num1 + " - " + item.num3 + " * " + num4 + " = " + item.result.ToString());

                 Console.WriteLine();
                 Thread.Sleep(500);
             }
         }*/
        /*  [Then(@"the result must be ""([^""]*)""")]
          public void ThenTheResultMustBe(string result)
          {

              string actualresult = "FAILED";
              calculatorResult = getCalculatorResult();
              if (expectedresult == calculatorResult )
              {
                  actualresult = "PASSED";
              }

            //  Assert.That(result.ToUpper(), Is.EqualTo(actualresult));
               Assert.AreEqual(result.ToUpper(), (actualresult), testdataname); //print msgs in assertion


              Thread.Sleep(300);
          }*/
        #endregion
    }
}
