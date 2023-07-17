namespace CommonCore.Tests
{
    public class TestStepInfo : IStepInfo
    {
        #region Fields and Properties
        public string Description { get; set; }
        public bool SkipStep { get; set; }
        public Priority Level { get; set; }
        public bool SkipStepOnFailure { get; set; }
        public bool FailsIteration { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan Duration { get; set; }
        public Exception StepException { get; set; }
        public bool IsMandatory { get; set; }
        TestState Status { get; set; }
        TestState IStepInfo.Status { get; set; }
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

        public TestStepInfo(string description, Priority level)
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

        public TestStepInfo(string description, bool skipStepOnFailure, Priority level)
        {
            StepInit();
            Description = description;
            SkipStepOnFailure = skipStepOnFailure;
            Level = level;
        }

        public TestStepInfo(string description, bool skipStepOnFailure, Priority level, bool isMandatory = false)
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

        public TestStepInfo(string description, bool skipStepOnFailure, bool skipStep, Priority level)
        {
            StepInit();
            Description = description;
            SkipStepOnFailure = skipStepOnFailure;
            SkipStep = skipStep;
            Level = level;
        }

        protected void StepInit()
        {
            Description = "Description not defined";
            SkipStep = false;
            SkipStepOnFailure = true;
            FailsIteration = true;
            Level = Priority.High;
            Status = TestState.Unknown;
        }
    }
}