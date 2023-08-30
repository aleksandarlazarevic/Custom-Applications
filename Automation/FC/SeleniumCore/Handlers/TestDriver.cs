using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumCore.Enums;
using SeleniumCore.WebDriver;
using SeleniumCore.Contracts.Drivers;

namespace SeleniumCore.Handlers
{
    public class TestDriver
    {
        #region Fields and Properties
        private int _stepCount = 0;
        private bool _stepContinuation = false;
        private bool _isHighImportanceFailed = false;
        private IList<IStepInfo> _testSteps = new List<IStepInfo>();
        public static TestDriver Instance { get; private set; }
        public TestContext TestContext { get; set; }
        public string TestIdentifier { get; set; }
        public string TestDescription { get; set; }
        public TestStatus Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan Duration { get; set; }
        public Exception TestException { get; set; }
        public Exception StepException { get; set; }
        #endregion

        private TestDriver(TestContext testContext)
        {
            TestContext = testContext;

            TestIdentifier = testContext.TestName;
            TestDescription = testContext.FullyQualifiedTestClassName;
            Status = TestStatus.Ready;
        }

        #region Methods
        public TestStatus RunStep(Action action)
        {
            TestStatus testStatus = TestStatus.Failed;

            try
            {
                action();
                testStatus = TestStatus.Passed;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                FinalizeStep();
            }

            return testStatus;
        }

        public TestStatus RunStep<T>(Action<T> action, T parameter)
        {
            TestStatus testStatus = TestStatus.Failed;

            try
            {
                action(parameter);
                testStatus = TestStatus.Passed;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                FinalizeStep();
            }

            return testStatus;
        }

        public TestStatus RunStep(Action action, IStepInfo stepInfo)
        {
            try
            {
                if (!ShouldTheStepBeExecuted(stepInfo))
                {
                    action();
                    stepInfo.Status = TestStatus.Passed;
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

        public TestStatus RunStep<T>(Action<T> action, T parameter, IStepInfo stepInfo)
        {
            try
            {
                if (!ShouldTheStepBeExecuted(stepInfo))
                {
                    action(parameter);
                    stepInfo.Status = TestStatus.Passed;
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

        public TestStatus RunStep<T1, T2>(Action<T1, T2> action, T1 parameter1, T2 parameter2, IStepInfo stepInfo)
        {
            try
            {
                if (!ShouldTheStepBeExecuted(stepInfo))
                {
                    action(parameter1, parameter2);
                    stepInfo.Status = TestStatus.Passed;
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
                Instance = new TestDriver(testContext);
            }
        }

        public void Close()
        {
            TestStatus testStatusIn;
            TestStatus testStatusOut;

            this.HasTheStepFailed(_testSteps, out testStatusIn);
            this.SomethingOutsideTestStepHasFailed(out testStatusOut);

            if (testStatusIn == TestStatus.Passed && testStatusOut == TestStatus.Passed)
            {
                Status = TestStatus.Passed;
            }
            else
            {
                Status = TestStatus.Failed;
            }

            Instance = null;
            TestContext = null;

            if (Status != TestStatus.Passed)
            {
                throw StepException ?? TestException ?? new Exception(String.Format("Test case failed {0}", TestIdentifier));
            }
        }

        private bool ShouldTheStepBeExecuted(IStepInfo step)
        {
            bool skip = false;

            return skip;
        }

        private void HandleException(Exception ex, IStepInfo stepInfo)
        {
            stepInfo.Status = TestStatus.Failed;
            stepInfo.StepException = ex;
            StepException = ex;
            _stepContinuation = false;

            UIDriver.TakeScreenshot("Exception");
        }

        private void FinalizeStep()
        {

        }

        private void FinalizeStep(IStepInfo stepInfo)
        {

        }

        private bool HasTheStepFailed(IList<IStepInfo> stepCollection, out TestStatus testStatus)
        {
            if (stepCollection.Where(x => x.Status == TestStatus.Failed).Count() > 0)
            {
                testStatus = TestStatus.Failed;
                return true;
            }

            testStatus = TestStatus.Passed;
            return false;
        }

        private bool SomethingOutsideTestStepHasFailed(out TestStatus testStatus)
        {
            if (TestException != null)
            {
                testStatus = TestStatus.Failed;
                return true;
            }

            testStatus = TestStatus.Passed;
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