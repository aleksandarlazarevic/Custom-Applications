using NUnit.Framework;
using Utilities;
using static NUnit.Framework.TestContext;

namespace CommonCore.Tests
{
    public class TestData
    {
        #region Fields and Properties
        private int _stepCount = 0;
        private bool _stepContinuation = false;
        private bool _isHighImportanceFailed = false;
        private IList<IStepInfo> _testSteps = new List<IStepInfo>();
        public static TestData Instance { get; private set; }
        public TestContext TestContext { get; set; }
        public TestAdapter TestIdentifier { get; set; }
        public string TestDescription { get; set; }
        public TestState Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan Duration { get; set; }
        public Exception TestException { get; set; }
        public Exception StepException { get; set; }
        #endregion

        private TestData(TestContext testContext)
        {
            TestContext = testContext;
            TestIdentifier = testContext.Test;
            Status = TestState.Unknown;
        }

        #region Methods
        public TestState RunStep(Action action)
        {
            TestState testStatus = TestState.Failed;

            try
            {
                action();
                testStatus = TestState.Passed;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                FinalizeStep();
            }

            return testStatus;
        }

        public TestState RunStep<T>(Action<T> action, T parameter)
        {
            TestState testStatus = TestState.Failed;

            try
            {
                action(parameter);
                testStatus = TestState.Passed;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                FinalizeStep();
            }

            return testStatus;
        }

        public TestState RunStep(Action action, IStepInfo stepInfo)
        {
            try
            {
                if (!ShouldTheStepBeExecuted(stepInfo))
                {
                    action();
                    stepInfo.Status = TestState.Passed;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, stepInfo);
            }
            finally
            {
                FinalizeStep(stepInfo);
            }

            return stepInfo.Status;
        }

        public TestState RunStep<T>(Action<T> action, T parameter, IStepInfo stepInfo)
        {
            try
            {
                if (!ShouldTheStepBeExecuted(stepInfo))
                {
                    action(parameter);
                    stepInfo.Status = TestState.Passed;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, stepInfo);
            }
            finally
            {
                FinalizeStep(stepInfo);
            }

            return stepInfo.Status;
        }

        public TestState RunStep<T1, T2>(Action<T1, T2> action, T1 parameter1, T2 parameter2, IStepInfo stepInfo)
        {
            try
            {
                if (!ShouldTheStepBeExecuted(stepInfo))
                {
                    action(parameter1, parameter2);
                    stepInfo.Status = TestState.Passed;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, stepInfo);
            }
            finally
            {
                FinalizeStep(stepInfo);
            }

            return stepInfo.Status;
        }

        public static void Initialize(TestContext testContext)
        {
            if (Instance == null)
            {
                Instance = new TestData(testContext);
            }
        }

        public void Close()
        {
            TestState testStatusIn;
            TestState testStatusOut;

            HasTheStepFailed(_testSteps, out testStatusIn);
            SomethingOutsideTestStepHasFailed(out testStatusOut);

            if (testStatusIn == TestState.Passed && testStatusOut == TestState.Passed)
            {
                Status = TestState.Passed;
            }
            else
            {
                Status = TestState.Failed;
            }

            Instance = null;
            TestContext = null;

            if (Status != TestState.Passed)
            {
                throw StepException ?? TestException ?? new Exception($"Test case {TestIdentifier} failed");
            }
        }

        private bool ShouldTheStepBeExecuted(IStepInfo step)
        {
            bool skip = false;

            return skip;
        }

        private void HandleException(Exception ex, IStepInfo stepInfo)
        {
            stepInfo.Status = TestState.Failed;
            stepInfo.StepException = ex;
            StepException = ex;
            _stepContinuation = false;

            //WebDriverFactory.TakeScreenshot("Exception");
        }

        private void FinalizeStep()
        {

        }

        private void FinalizeStep(IStepInfo stepInfo)
        {

        }

        private bool HasTheStepFailed(IList<IStepInfo> stepCollection, out TestState testStatus)
        {
            if (stepCollection.Where(x => x.Status == TestState.Failed).Count() > 0)
            {
                testStatus = TestState.Failed;
                return true;
            }

            testStatus = TestState.Passed;
            return false;
        }

        private bool SomethingOutsideTestStepHasFailed(out TestState testStatus)
        {
            if (TestException != null)
            {
                testStatus = TestState.Failed;
                return true;
            }

            testStatus = TestState.Passed;
            return false;
        }

        private bool ShouldContinueOnFailure(IStepInfo stepInfo)
        {
            if (!stepInfo.SkipStepOnFailure)
            {
                return _stepContinuation = true;
            }
            else if (stepInfo.SkipStepOnFailure && _stepContinuation)
            {
                return true;
            }

            return false;
        }
        #endregion
    }
}