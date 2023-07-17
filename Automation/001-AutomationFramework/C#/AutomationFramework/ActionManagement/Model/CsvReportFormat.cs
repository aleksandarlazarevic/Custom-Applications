namespace ActionManagement.Model
{
    public class CsvReportFormat
    {
        public string IssueType { get; set; }
        public string IssueKey { get; set; }
        public string IssueId { get; set; }
        public string Summary { get; set; }
        public string Assignee { get; set; }
        public string AssigneeId { get; set; }
        public string Reporter { get; set; }
        public string ReporterId { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public string Resolution { get; set; }
        public string RootCauseDescription { get; set; }
        public string Origin { get; set; }
        public string Created { get; set; }
        public string DueDate { get; set; }
        public string Updated { get; set; }
        public string Resolved { get; set; }
    }
}
