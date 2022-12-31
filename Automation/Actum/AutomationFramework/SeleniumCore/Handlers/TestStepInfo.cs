using SeleniumCore.Contracts.Drivers;
using SeleniumCore.Enums;

namespace SeleniumCore.Handlers
{
    public class TestStepInfo : IStepInfo
    {
        #region Fields and Properties
        public string Description { get; set; }
        public bool SkipStep { get; set; }
        public Importance Level { get; set; }
        public bool SkipStepOnFailure { get; set; }
        public bool FailsIteration { get; set; }
        public TestStatus Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan Duration { get; set; }
        public Exception StepException { get; set; }
        public bool IsMandatory { get; set; }
        #endregion

        public TestStepInfo()
        {
            StepInit();
        }

        public TestStepInfo(string description)
        {
            StepInit();
            Description = description;
        }

        public TestStepInfo(string description, Importance level)
        {
            StepInit();
            Description = description;
            Level = level;
        }

        public TestStepInfo(string description, bool skipStepOnFailure)
        {
            StepInit();
            Description = description;
            SkipStepOnFailure = skipStepOnFailure;
        }

        public TestStepInfo(string description, bool skipStepOnFailure, Importance level)
        {
            StepInit();
            Description = description;
            SkipStepOnFailure = skipStepOnFailure;
            Level = level;
        }

        public TestStepInfo(string description, bool skipStepOnFailure, Importance level, bool isMandatory = false)
        {
            StepInit();
            Description = description;
            SkipStepOnFailure = skipStepOnFailure;
            Level = level;
            IsMandatory = isMandatory;
        }

        public TestStepInfo(string description, bool skipStepOnFailure, bool skipStep)
        {
            StepInit();
            Description = description;
            SkipStepOnFailure = skipStepOnFailure;
            SkipStep = skipStep;
        }

        public TestStepInfo(string description, bool skipStepOnFailure, bool skipStep, Importance level)
        {
            StepInit();
            Description = description;
            SkipStepOnFailure = skipStepOnFailure;
            SkipStep = skipStep;
            Level = level;
        }

        protected void StepInit()
        {
            Description = "Step Description not defined";
            SkipStep = false;
            SkipStepOnFailure = true;
            FailsIteration = true;
            Level = Importance.High;
            Status = TestStatus.Ready;
        }
    }
}