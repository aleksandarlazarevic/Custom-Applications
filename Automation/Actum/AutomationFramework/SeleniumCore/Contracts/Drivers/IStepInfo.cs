using SeleniumCore.Enums;

namespace SeleniumCore.Contracts.Drivers
{
    public interface IStepInfo
    {
        string Description { get; set; }
        bool SkipStep { get; set; }
        Importance Level { get; set; }
        bool SkipStepOnFailure { get; set; }
        bool FailsIteration { get; set; }
        TestStatus Status { get; set; }
        DateTime StartTime { get; set; }
        DateTime EndTime { get; set; }
        TimeSpan Duration { get; set; }
        Exception StepException { get; set; }
        bool IsMandatory { get; set; }
    }
}