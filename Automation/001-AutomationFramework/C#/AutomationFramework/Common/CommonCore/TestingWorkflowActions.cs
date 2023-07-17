using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using CommonCore.Configuration;
using CommonCore.Containers;
using CommonCore.Contracts;
using CommonCore.Tests;
using NUnit.Framework;
using RazorEngine.Compilation.ImpromptuInterface;
using ReportGenerator;
using System.Globalization;
using System.Reflection;
using TechTalk.SpecFlow;
using Utilities;
using static System.Net.Mime.MediaTypeNames;

namespace CommonCore
{
    public abstract class TestingWorkflowActions : ITestingWorkflowActions
    {
        private TestConfiguration _configuration;
        public IEnumerable<IEngineManager> Engines { get; private set; }
        public TestConfiguration TestConfiguration { get { return _configuration; } }
        public TestData TestManager { get; private set; }

        public ReportingManager reportingManager;
        public static ExtentReports _extent;
        public ExtentTest _test;
        public String TC_Name;

        protected abstract IEnumerable<Assembly> GetTestingComponentAssemblies();
        protected abstract IEnumerable<IEngineManager> GetTestingEngines();
        protected abstract TestConfiguration GetTestingConfiguration();

        #region Methods
        #region Hooks
        #region Test run setup
        public void TestExecutionInitialization()
        {
            ContainerManager.EnumerateTestAssemblies(GetTestingComponentAssemblies());
            Engines = GetTestingEngines();
            _configuration = GetTestingConfiguration();
            string currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            TestInMemoryParameters.Instance.TestResultsDirectory = CreateTestResultsDirectory(currentDirectory + _configuration.TestResultPath);

            //create test output folder
            ////var path = Assembly.GetCallingAssembly().CodeBase;
            ////var actualPath = path.Substring(0, path.LastIndexOf("net7.0"));
            ////var projectPath = new Uri(actualPath).LocalPath;
            ////Directory.CreateDirectory(projectPath.ToString() + "Reports");
            ////var reportPath = projectPath + "\\Index.html";
            reportingManager = new ReportingManager(TestInMemoryParameters.Instance.TestResultsDirectory + "\\Index.html");

           //_testReport = new ExtentReport(TestInMemoryParameters.Instance.TestResultsDirectory + "\\Index.html");


            //_testData = new TestData();
            //_driverFactory = new DriverFactory();
            //Directory.CreateDirectory(Path.Combine("..", "..", "TestResults"));
        }

        public void TestExecutionFinalization()
        {
            reportingManager.CreateReport();
            TestManager.Close();
        }
        #endregion

        #region Feature setup
        public void TestFeatureInitialization(FeatureContext featureContext)
        {
            foreach (var engine in Engines) { engine.StartUp(); }
            ContainerManager.BuildContainer();
            reportingManager.CreateFeature(featureContext.FeatureInfo.Title);
        }

        public void TestFeatureFinalization()
        {
            foreach (var engine in Engines) { engine.ShutDown(); }
            ContainerManager.DisposeContainer();

            reportingManager.CreateReport();
        }
        #endregion

        #region Test scenario setup
        public void TestScenarioInitialization(TestContext testContext, FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            List<string> tags = GetTags(featureContext, scenarioContext);
            SetUpTestData(testContext);
            // Start logging
            reportingManager.CreateTestCase(testContext.Test.FullName, featureContext.FeatureInfo.Description, tags);
        }

        public void TestScenarioFinalization(TestContext testContext, ScenarioContext scenarioContext, StepInfo? stepInfo)
        {
            reportingManager.SetTestcaseOutcome(testContext, scenarioContext, stepInfo);
        }

        public void TestScenarioBlockInitialization(TestContext testContext, ScenarioContext scenarioContext)
        {
        }

        public void TestScenarioBlockFinalization(TestContext testContext, ScenarioContext scenarioContext)
        {
        }
        #endregion

        #region Test step setup
        public void TestStepInitialization(TestContext testContext, ScenarioContext scenarioContext)
        {
            reportingManager.CreateTestStep(scenarioContext.StepContext.StepInfo.Text, "bla bla");
        }

        public void TestStepFinalization(TestContext testContext, ScenarioContext scenarioContext)
        {
            // Take a screenshot?

            //var path = Path.Combine(_configuration.TestResultPath, Summary.BuildTestPath());
            //path = ResultPathValidation(path);
            //var stepName = _summary.GetCurrentStepId();

            //foreach (var engine in Engines)
            //{
            //    var name = $"{stepName} - {engine.GetEngineName()}";
            //    engine.CollectData(path, name);
            //}
        }
        #endregion

        public void SetUpTestData(TestContext _testContext)
        {
            TestData.Initialize(_testContext);
            TestManager = TestData.Instance;

            TestInMemoryParameters.Instance.TestIdentifier = TestManager.TestIdentifier;
            TestInMemoryParameters.Instance.TestIdentifierStartTime = DateTime.Now;
            TestInMemoryParameters.Instance.ElementTimeout = "60";
            TestInMemoryParameters.Instance.PageLoadTimeout = "60";

            // Log test data
        }

        private static List<string> GetTags(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            var tags = featureContext.FeatureInfo.Tags.ToList();
            tags.AddRange(scenarioContext.ScenarioInfo.Tags.ToList());
            return tags;
        }

        private string CreateTestResultsDirectory(string path)
        {
            string currentTime = $"{DateTime.UtcNow.ToString("yyy-MM-dd HH-mm-ss", CultureInfo.InvariantCulture)}";
            string? testResultsDirectory = Path.Combine(path, currentTime);

            if (Directory.Exists(testResultsDirectory))
            {
                Directory.Delete(testResultsDirectory, true);
            }

            Directory.CreateDirectory(testResultsDirectory);
            return testResultsDirectory;
        }

        //private void TakeScreenshot(IWebDriver driver)
        //{
        //    try
        //    {
        //        string fileNameBase = string.Format("error_{0}_{1}_{2}",
        //            FeatureContext.Current.FeatureInfo.Title,
        //            ScenarioContext.Current.ScenarioInfo.Title,
        //            DateTime.Now.ToString("yyyyMMdd_HHmmss"));

        //        var artifactDirectory = Path.Combine(Directory.GetCurrentDirectory(), "testresults");
        //        if (!Directory.Exists(artifactDirectory))
        //            Directory.CreateDirectory(artifactDirectory);

        //        string pageSource = driver.PageSource;
        //        string sourceFilePath = Path.Combine(artifactDirectory, fileNameBase + "_source.html");
        //        //File.WriteAllText(sourceFilePath, pageSource, Encoding.UTF8);
        //        Console.WriteLine("Page source: {0}", new Uri(sourceFilePath));

        //        ITakesScreenshot takesScreenshot = driver as ITakesScreenshot;

        //        if (takesScreenshot != null)
        //        {
        //            var screenshot = takesScreenshot.GetScreenshot();
        //            string screenshotFilePath = Path.Combine(artifactDirectory, fileNameBase + "_screenshot.png");
        //            //screenshot.SaveAsFile(screenshotFilePath, ImageFormat.Png);
        //            Console.WriteLine("Screenshot: {0}", new Uri(screenshotFilePath));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error while taking screenshot: {0}", ex);
        //    }
        //}
        //public static void TakeScreenshot(IWebDriver driver)
        //{
        //    Screenshot screenshot = driver.TakeScreenshot();
        //    string screenshotDirectory = (Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))));
        //    string screenshotFolder = Path.Combine(screenshotDirectory, "Screenshot");
        //    Directory.CreateDirectory(screenshotFolder);
        //    string screenshotName = $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now:yyyy_MM_dd_HH_mm_ss}.png";
        //    string sreenshotPath = Path.Combine(screenshotFolder, screenshotName);
        //    screenshot.SaveAsFile(sreenshotPath, ScreenshotImageFormat.Png);
        //}

        //public static void TakeFullPageScreenshot(this IWebDriver driver, string fileName)
        //{
        //    driver.SaveScreenshot(fileName, true);
        //}

        //public static void TakeStandardScreenshot(this IWebDriver driver, string fileName)
        //{
        //    driver.SaveScreenshot(fileName, false);
        //}

        //private static void SaveScreenshot(this IWebDriver driver, string fileName, bool fullPage)
        //{
        //    try
        //    {
        //        var safeFileName = GetSafeFileName(fileName);
        //        var pathToFile = GetPathToFile(GetSafeFileName(safeFileName));

        //        if (fullPage)
        //        {
        //            driver.SaveFullPageScreenshot(pathToFile);
        //        }
        //        else
        //        {
        //            driver.SaveStandardScreenshot(pathToFile);
        //        }

        //        AllureLifecycle.Instance.AddAttachment(pathToFile, safeFileName);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Taking a screenshot failed: {ex.Message}");
        //    }
        //}

        //private static void SaveFullPageScreenshot(this IWebDriver driver, string pathToFile)
        //{
        //    var bytesArr = GetBaseDriver(driver).TakeScreenshot(new VerticalCombineDecorator(new ScreenshotMaker()));
        //    bytesArr.ToMagickImage().Write(pathToFile);
        //}

        //private static void SaveStandardScreenshot(this IWebDriver driver, string pathToFile)
        //{
        //    var screenshot = ((ITakesScreenshot)GetBaseDriver(driver)).GetScreenshot();
        //    screenshot.SaveAsFile(pathToFile, ScreenshotImageFormat.Png);
        //}





        #endregion
        #endregion
    }
}