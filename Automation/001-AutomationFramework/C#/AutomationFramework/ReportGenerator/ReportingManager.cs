using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TechTalk.SpecFlow;

namespace ReportGenerator
{
    public class ReportingManager
    {
        private static ExtentHtmlReporter _htmlReporter;
        private static AventStack.ExtentReports.ExtentReports _extentReport;
        private static ExtentTest _extentStep;
        private static ExtentTest _extentTest;
        private static ExtentTest _extentFeature;

        public ReportingManager(string reportFolderPath)
        {
            _htmlReporter = new ExtentHtmlReporter(reportFolderPath);
            //_htmlReporter.LoadConfig(reportFolderPath + "reportConfig.xml");

            _extentReport = new AventStack.ExtentReports.ExtentReports();
            _extentReport.AttachReporter(_htmlReporter);
        }

        public void CreateReport()
        {
            _extentReport.Flush();
        }

        public void CreateFeature(string name)
        {
            _extentFeature = _extentReport.CreateTest(new GherkinKeyword("Feature"), $"Feature: {name}");
        }

        public void RemoveFeature(ExtentTest extentFeature)
        {
            _extentReport.RemoveTest(extentFeature);
        }

        public void CreateTestCase(string name, string description, IEnumerable<string> tags)
        {
            _extentTest = _extentReport.CreateTest(name, description);

            foreach (var tag in tags)
            {
                _extentTest.AssignCategory(tag);
            }
        }

        public void CreateTestStep(string name, string description)
        {
            _extentStep = _extentTest.CreateNode(new GherkinKeyword("Given"), name, description);
        }

        public void TakeAScreenshot(string screenshotPath)
        {
            _extentStep.AddScreenCaptureFromPath(screenshotPath);
        }

        public MediaEntityModelProvider AddScreenshotToLogs(string screenshotPath)
        {
            return MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build();
        }

        public void SetTestcaseOutcome(TestContext testContext, ScenarioContext sc, StepInfo? stepInfo)
        {
            var stepType = stepInfo.StepDefinitionType.ToString();
            PropertyInfo pInfo = typeof(ScenarioContext).GetProperty("ScenarioExecutionStatus", BindingFlags.Instance | BindingFlags.Public);
            MethodInfo getter = pInfo.GetGetMethod(nonPublic: true);
            object TestResult = getter.Invoke(sc, null);
            if (sc.TestError == null)
            {
                if (stepType == "Given")
                    _extentTest.CreateNode<Given>(stepInfo.Text);
                else if (stepType == "When")
                    _extentTest.CreateNode<When>(stepInfo.Text);
                else if (stepType == "Then")
                    _extentTest.CreateNode<Then>(stepInfo.Text);
                else if (stepType == "And")
                    _extentTest.CreateNode<And>(stepInfo.Text);
            }
            if (sc.TestError != null)
            {
                if (stepType == "Given")
                    _extentTest.CreateNode<Given>(stepInfo.Text).Fail(sc.TestError.Message);
                if (stepType == "When")
                    _extentTest.CreateNode<When>(stepInfo.Text).Fail(sc.TestError.Message);
                if (stepType == "Then")
                    _extentTest.CreateNode<Then>(stepInfo.Text).Fail(sc.TestError.Message);
                if (stepType == "And")
                    _extentTest.CreateNode<And>(stepInfo.Text).Fail(sc.TestError.Message);
            }





            bool passed = testContext.Result.Outcome.Status == TestStatus.Passed;
            var exec_status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(testContext.Result.StackTrace) ? "" 
                : string.Format("{0}", testContext.Result.StackTrace);
            Status logstatus = Status.Pass;
            string screenShotPath;
            string fileName = "Screenshot_" + DateTime.Now.ToString("h_mm_ss") + testContext.Test.Name + ".png";

            switch (exec_status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    //var mediaEntity = CaptureScreenShot(WebDriverFactory, testContext.Test.Name);
                    _extentTest.Log(Status.Fail, "Fail");
                    //_extentTest.Log(Status.Fail, "Traditional Snapshot below: " + _extentTest.AddScreenCaptureFromPath("Screenshots\\" + fileName));
                    break;
                case TestStatus.Passed:
                    logstatus = Status.Pass;
                    /* Capturing Screenshots using built-in methods in ExtentReports 4 */
                    //mediaEntity = CaptureScreenShot(driver.Value, fileName);
                    _extentTest.Log(Status.Pass, "Pass");
                    /* Usage of MediaEntityBuilder for capturing screenshots */
                    //_extentTest.Pass("ExtentReport 4 Capture: Test Passed", mediaEntity);
                    /* Usage of traditional approach for capturing screenshots */
                    //_extentTest.Log(Status.Pass, "Traditional Snapshot below: " + _extentTest.AddScreenCaptureFromPath("Screenshots\\" + fileName));
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    break;
                default:
                    break;
            }
        }

        public MediaEntityModelProvider CaptureScreenShot(IWebDriver driver, String screenShotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            var screenshot = ts.GetScreenshot().AsBase64EncodedString;

            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenShotName).Build();
        }
    }
}
