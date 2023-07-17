namespace CommonCore.Tests
{
    public interface IStepInfo
    {
        string Description { get; set; }
        bool SkipStep { get; set; }
        Priority Level { get; set; }
        bool SkipStepOnFailure { get; set; }
        bool FailsIteration { get; set; }
        TestState Status { get; set; }
        DateTime StartTime { get; set; }
        DateTime EndTime { get; set; }
        TimeSpan Duration { get; set; }
        Exception StepException { get; set; }
        bool IsMandatory { get; set; }
    }
}