using ActionManagement.Model;
using ActionManagement.Model.Operational_Summary.Incident_KPIs;
using ActionManagement.Model.Operational_Summary.Live_Service_Key_Incidents;
using ActionManagement.ViewModel;
using CsvHelper;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using UglyToad.PdfPig.Graphics.Operations.PathPainting;

namespace ActionManagement.HelperActions
{
    public class ReportsCreation : BaseViewModel
    {
        private static string _selectedCsvReport { get; set; }
        //public static DateTime startDate = new DateTime(2023, 6, 19);
        public static string reportsLocation = @"C:\Reports\";
        public static string ticketUrlPrefix = "https://regusit.atlassian.net/browse/";
        public static List<CsvReportFormat> allTicketData = new List<CsvReportFormat>();
        public static List<CsvReportFormat> ticketsFromLastWeek = new List<CsvReportFormat>();
        private static DateTime _startDate = new DateTime();
        private static DateTime _endDate = new DateTime();

        public static void CreateReport(string selectedCsvReportPath, DateTime endDate)
        {
            _startDate = endDate.AddDays(-6);
            _endDate = endDate;
            MessageBox.Show("Calculating the reports for 1 week before the specified end date: ", endDate.ToString());
            _selectedCsvReport = Path.GetFileName(selectedCsvReportPath);

            allTicketData = ExtractTicketDataFromCsv(selectedCsvReportPath);
            ticketsFromLastWeek = GetTicketsFromLastWeek(allTicketData);

            GetIncidentSeverityAnalysis();
            GetIncidentRootCauseAnalysisOfNewMSTs​(ticketsFromLastWeek);
            GetIncidentRootCauseAnalysisOfClosedMSTs​(allTicketData);

            GetKeyIncidentsOpen​(allTicketData);
            GetKeyIncidentsClosed​(allTicketData);
        }

        private static List<CsvReportFormat> GetTicketsFromLastWeek(List<CsvReportFormat> tickets)
        {
            List<CsvReportFormat> ticketsFromLastWeek = new List<CsvReportFormat>();

            foreach (CsvReportFormat ticket in tickets)
            {
                DateTime createdDate = DateTime.Parse(ticket.Created);
                if (createdDate >= _startDate && createdDate <= _endDate)
                {
                    ticketsFromLastWeek.Add(ticket);
                }
            }

            return ticketsFromLastWeek;
        }

        private static void GetKeyIncidentsClosed(List<CsvReportFormat> tickets)
        {
            List<KeyIncidentsClosed​> table = new List<KeyIncidentsClosed​>();

            foreach (var ticket in tickets)
            {
                if (ticket.Origin.Equals(string.Empty))
                {
                    ticket.Origin = "No Root Cause set-" + ticket.IssueKey;
                    //throw new Exception($"{ticket.IssueKey} contains no cause Set!");
                    continue;
                }
            }

            List<string> rootCauses = tickets.Where(p => p.Status.Equals("Resolved")).Select(p => p.Origin).Distinct().ToList();
            foreach (string cause in rootCauses)
            {
                PopulateKeyIncidentsClosed​​RowData(tickets, cause, ref table);
            }

            CreateCSV<KeyIncidentsClosed​>(table, $"{reportsLocation}KeyIncidentsClosed​.csv");
        }

        private static void PopulateKeyIncidentsClosedRowData(List<CsvReportFormat> tickets, string cause, ref List<KeyIncidentsClosed> table)
        {
            if (tickets.Count > 0)
            {
                List<CsvReportFormat> ticketsWithGivenCause = tickets.Where(p => p.Origin.Equals(cause)).ToList();

                // created tickets
                List<CsvReportFormat> createdTickets = ticketsWithGivenCause;

                // closed tickets
                List<CsvReportFormat> closedTickets = ticketsWithGivenCause.Where(p => p.Status.Equals("Resolved")).ToList();

                // average resolution time
                int sumOfResolutionTimes = 0;
                decimal averageResolutionTime = 0;

                foreach (var ticket in closedTickets)
                {
                    DateTime currentDate = DateTime.Parse(ticket.Created);
                    DateTime updatedDate = DateTime.Parse(ticket.Updated);
                    int resolutionTime = (int)(updatedDate - currentDate).Days;
                    if (resolutionTime == 0)
                    {
                        resolutionTime = (int)(updatedDate.Day - currentDate.Day);
                    }
                    sumOfResolutionTimes += resolutionTime;
                }

                if (closedTickets.Count != 0)
                {
                    averageResolutionTime = (decimal)sumOfResolutionTimes / (decimal)closedTickets.Count;
                }

                var numberOfMSTTickets = ticketsWithGivenCause.Count.ToString();
                var raised = createdTickets.Count;
                var resolved = closedTickets.Count;

                table.Add(new KeyIncidentsClosed​
                {
                    Area = cause,
                    Impact = "TBD",
                    Status = "Resolved",
                    IncidentCount​ = numberOfMSTTickets,
                    Raised = raised.ToString(),
                    Resolved​ = resolved.ToString(),
                    Duration = averageResolutionTime.ToString(),
                    FixSystem​ = "CSU",
                    ActionTaken​ = "TBD"
                });
            }
            else
            {
                table.Add(new KeyIncidentsClosed​
                {
                    Area = cause,
                    Impact = "",
                    Status = "Resolved",
                    IncidentCount​ = "0",
                    Raised = "0",
                    Resolved​ = "0",
                    Duration = "N/A",
                    FixSystem​ = "CSU",
                    ActionTaken​ = ""
                });
            }
        }

        private static void GetKeyIncidentsOpen(List<CsvReportFormat> tickets)
        {
            List<KeyIncidentsOpen​> table = new List<KeyIncidentsOpen​>();

            foreach (var ticket in tickets)
            {
                if (ticket.Origin.Equals(string.Empty))
                {
                    ticket.Origin = "No Root Cause set-" + ticket.IssueKey;
                    //throw new Exception($"{ticket.IssueKey} contains no cause Set!");
                    continue;
                }
            }

            List<string> rootCauses = tickets.Where(p => !p.Status.Equals("Resolved")).Select(p => p.Origin).Distinct().ToList();
            foreach (string cause in rootCauses)
            {
                PopulateKeyIncidentsOpen​RowData(tickets, cause, ref table);
            }

            CreateCSV<KeyIncidentsOpen​>(table, $"{reportsLocation}KeyIncidentsOpen​.csv");
        }

        private static void PopulateKeyIncidentsOpenRowData(List<CsvReportFormat> tickets, string cause, ref List<KeyIncidentsOpen> table)
        {
            if (tickets.Count > 0)
            {
                List<CsvReportFormat> ticketsWithGivenCause = tickets.Where(p => p.Origin.Equals(cause)).ToList();

                // created tickets
                List<CsvReportFormat> createdTickets = ticketsWithGivenCause;

                // closed tickets
                List<CsvReportFormat> closedTickets = ticketsWithGivenCause.Where(p => p.Status.Equals("Resolved")).ToList();

                // average resolution time
                int sumOfResolutionTimes = 0;
                decimal averageResolutionTime = 0;

                foreach (var ticket in closedTickets)
                {
                    DateTime currentDate = DateTime.Parse(ticket.Created);
                    DateTime updatedDate = DateTime.Parse(ticket.Updated);
                    int resolutionTime = (int)(updatedDate - currentDate).Days;
                    if (resolutionTime == 0)
                    {
                        resolutionTime = (int)(updatedDate.Day - currentDate.Day);
                    }

                    sumOfResolutionTimes += resolutionTime;
                }

                if (closedTickets.Count != 0)
                {
                    averageResolutionTime = (decimal)sumOfResolutionTimes / (decimal)closedTickets.Count;
                }

                var numberOfMSTTickets = ticketsWithGivenCause.Count.ToString();
                var raised = createdTickets.Count;
                var resolved = closedTickets.Count;

                table.Add(new KeyIncidentsOpen​
                {
                    Area = cause,
                    Impact = "TBD",
                    Status = "Planned",
                    IncidentCount​ = numberOfMSTTickets,
                    Raised = raised.ToString(),
                    Resolved​ = resolved.ToString(),
                    Duration = averageResolutionTime.ToString(),
                    FixSystem​ = "CSU",
                    ActionTaken​ = "TBD"
                });
            }
            else
            {
                table.Add(new KeyIncidentsOpen​
                {
                    Area = cause,
                    Impact = "",
                    Status = "Planned",
                    IncidentCount​ = "0",
                    Raised = "0",
                    Resolved​ = "0",
                    Duration = "N/A",
                    FixSystem​ = "CSU",
                    ActionTaken​ = ""
                });
            }
        }

        private static void GetIncidentRootCauseAnalysisOfClosedMSTs(List<CsvReportFormat> tickets)
        {
            List<IncidentRootCauseAnalysisOfClosedMSTs​> table = new List<IncidentRootCauseAnalysisOfClosedMSTs​>();

            foreach (var ticket in tickets)
            {
                if (ticket.Origin.Equals(string.Empty))
                {
                    ticket.Origin = "No Root Cause set-" + ticket.IssueKey;
                    //throw new Exception($"{ticket.IssueKey} contains no cause Set!");
                    continue;
                }
            }

            List<string> rootCauses = tickets.Where(p => p.Status.Equals("Resolved")).Select(p => p.Origin).Distinct().ToList();
            foreach (string cause in rootCauses)
            {
                PopulateRootCauseClosedRowData(tickets, cause, ref table);
            }

            CreateCSV<IncidentRootCauseAnalysisOfClosedMSTs​>(table, $"{reportsLocation}IncidentRootCauseAnalysisOfClosedMSTs​.csv");
        }

        private static void PopulateRootCauseClosedRowData(List<CsvReportFormat> tickets, string cause, ref List<IncidentRootCauseAnalysisOfClosedMSTs> table)
        {
            if (tickets.Count > 0)
            {
                List<CsvReportFormat> ticketsWithGivenCause = tickets.Where(p => p.Origin.Equals(cause)).ToList();

                var numberOfMSTTickets = ticketsWithGivenCause.Count.ToString();
                var p1Blocker = ticketsWithGivenCause.Where(p => p.Priority.Equals("Blocker")).ToList().Count;
                var p2Critical = ticketsWithGivenCause.Where(p => p.Priority.Equals("Critical")).ToList().Count;
                var p3Major = ticketsWithGivenCause.Where(p => p.Priority.Equals("Major")).ToList().Count;
                var p4Minor = ticketsWithGivenCause.Where(p => p.Priority.Equals("Minor")).ToList().Count;
                var impact = $"{numberOfMSTTickets} customers";
                var percentage = (((decimal)ticketsWithGivenCause.Count / (decimal)tickets.Count) * 100).ToString() + "%";

                table.Add(new IncidentRootCauseAnalysisOfClosedMSTs​
                {
                    RootCause​​ = cause,
                    NumberOfMSTTickets = numberOfMSTTickets,
                    P1Blocker = p1Blocker.ToString(),
                    P2Critical = p2Critical.ToString(),
                    P3Major = p3Major.ToString(),
                    P4Minor = p4Minor.ToString(),
                    Impact = impact,
                    Percentage = percentage
                });
            }
            else
            {
                table.Add(new IncidentRootCauseAnalysisOfClosedMSTs​
                {
                    RootCause​​ = cause,
                    NumberOfMSTTickets = "0",
                    P1Blocker = "0",
                    P2Critical = "0",
                    P3Major = "0",
                    P4Minor = "0",
                    Impact = "0",
                    Percentage = "N/A"
                });
            }
        }

        private static void GetIncidentRootCauseAnalysisOfNewMSTs(List<CsvReportFormat> tickets)
        {
            List<IncidentRootCauseAnalysisOfNewMSTs​> table = new List<IncidentRootCauseAnalysisOfNewMSTs​>();

            foreach (var ticket in tickets)
            {
                if (ticket.Origin.Equals(string.Empty))
                {
                    ticket.Origin = "No Origin set-" + ticket.IssueKey;
                    //throw new Exception($"{ticket.IssueKey} contains no root cause Set!");
                    continue;
                }
            }

            List<string> rootCauses = tickets.Select(p => p.Origin).Distinct().ToList();
            foreach (string cause in rootCauses)
            {
                PopulateRootCauseRowData(tickets, cause, ref table);
            }

            CreateCSV<IncidentRootCauseAnalysisOfNewMSTs​>(table, $"{reportsLocation}IncidentRootCauseAnalysisOfNewMSTs​.csv");
        }

        private static void PopulateRootCauseRowData(List<CsvReportFormat> tickets, string cause, ref List<IncidentRootCauseAnalysisOfNewMSTs> table)
        {
            if (tickets.Count > 0)
            {
                List<CsvReportFormat> ticketsWithGivenCause = tickets.Where(p => p.Origin.Equals(cause)).ToList();

                var numberOfMSTTickets = ticketsWithGivenCause.Count.ToString();
                var p1Blocker = ticketsWithGivenCause.Where(p => p.Priority.Equals("Blocker")).ToList().Count;
                var p2Critical = ticketsWithGivenCause.Where(p => p.Priority.Equals("Critical")).ToList().Count;
                var p3Major = ticketsWithGivenCause.Where(p => p.Priority.Equals("Major")).ToList().Count;
                var p4Minor = ticketsWithGivenCause.Where(p => p.Priority.Equals("Minor")).ToList().Count;
                var impact = $"{numberOfMSTTickets} customers";
                var percentage = (((decimal)ticketsWithGivenCause.Count / (decimal)tickets.Count) * 100).ToString() + "%";

                table.Add(new IncidentRootCauseAnalysisOfNewMSTs
                {
                    Origin = cause,
                    NumberOfMSTTickets = numberOfMSTTickets,
                    P1Blocker = p1Blocker.ToString(),
                    P2Critical = p2Critical.ToString(),
                    P3Major = p3Major.ToString(),
                    P4Minor = p4Minor.ToString(),
                    Impact = impact,
                    Percentage = percentage
                });
            }
            else
            {
                table.Add(new IncidentRootCauseAnalysisOfNewMSTs
                {
                    Origin = cause,
                    NumberOfMSTTickets = "0",
                    P1Blocker = "0",
                    P2Critical = "0",
                    P3Major = "0",
                    P4Minor = "0",
                    Impact = "0",
                    Percentage = "N/A"
                });
            }
        }

        private static void GetIncidentSeverityAnalysis()
        {
            List<IncidentSeverityAnalysis> table = new List<IncidentSeverityAnalysis>();

            List<CsvReportFormat> blockers = allTicketData.Where(p => p.Priority.Equals("Blocker")).ToList();
            PopulateRowData(blockers, ref table);

            List<CsvReportFormat> criticals = allTicketData.Where(p => p.Priority.Equals("Critical")).ToList();
            PopulateRowData(criticals, ref table);

            List<CsvReportFormat> majors = allTicketData.Where(p => p.Priority.Equals("Major")).ToList();
            PopulateRowData(majors, ref table);

            List<CsvReportFormat> minors = allTicketData.Where(p => p.Priority.Equals("Minor")).ToList();
            PopulateRowData(minors, ref table);

            if ((blockers.Count == 0) && (criticals.Count == 0) && (majors.Count == 0) && (minors.Count == 0))
            {
                MessageBox.Show("Please double-check if all MST ticked fields have been populated properly, especially fields like 'Root Cause' and 'IT Priority'");
            }

            CreateCSV<IncidentSeverityAnalysis>(table, $"{reportsLocation}IncidentSeverityAnalysis.csv");
        }

        private static void PopulateRowData(List<CsvReportFormat> tickets, ref List<IncidentSeverityAnalysis> table)
        {
            if (tickets.Count > 0)
            {
                // open tickets
                List<CsvReportFormat> openTickets = tickets.Where(p => !p.Status.Equals("Resolved")).ToList();

                // created tickets
                List<CsvReportFormat> createdTickets = GetTicketsFromLastWeek(tickets);

                // closed tickets
                List<CsvReportFormat> closedTickets = tickets.Where(p => p.Status.Equals("Resolved") && (DateTime.Parse(p.Updated) <= _endDate)).ToList();

                // average resolution time
                int sumOfResolutionTimes = 0;
                decimal averageResolutionTime = 0;

                foreach (var ticket in closedTickets)
                {
                    DateTime currentDate = DateTime.Parse(ticket.Created);
                    DateTime updatedDate = DateTime.Parse(ticket.Updated);
                    int resolutionTime = (int)(updatedDate - currentDate).Days;

                    if (resolutionTime == 0)
                    {
                        resolutionTime = (int)(updatedDate.Day - currentDate.Day);
                    }

                    sumOfResolutionTimes += resolutionTime;
                }

                if (closedTickets.Count != 0)
                {
                    averageResolutionTime = (decimal)sumOfResolutionTimes / (decimal)closedTickets.Count;
                }

                string openTicketsSummary = openTickets.Count.ToString();
                string createdTicketsSummary = createdTickets.Count.ToString();
                string closedTicketsSummary = closedTickets.Count.ToString();
                foreach (CsvReportFormat ticket in openTickets)
                {
                    openTicketsSummary += $"-{ticketUrlPrefix}{ticket.IssueKey}";
                }

                foreach (CsvReportFormat ticket in createdTickets)
                {
                    createdTicketsSummary += $"-{ticketUrlPrefix}{ticket.IssueKey}";
                }

                foreach (CsvReportFormat ticket in closedTickets)
                {
                    closedTicketsSummary += $"- {ticketUrlPrefix}{ticket.IssueKey}";
                }

                table.Add(new IncidentSeverityAnalysis
                {
                    Open​ = openTicketsSummary,
                    Created = createdTicketsSummary,
                    Closed = createdTicketsSummary,
                    AverageResolutionTime = averageResolutionTime.ToString()
                });
            }
            else
            {
                table.Add(new IncidentSeverityAnalysis
                {
                    Open​ = "0",
                    Created = "0",
                    Closed = "0",
                    AverageResolutionTime = "N/A"
                });
            }
        }

        #region Write to CSV
        public static void CreateCSV<T>(List<T> list, string filePath)
        {
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                CreateHeader(list, sw);
                CreateRows(list, sw);
            }
        }

        private static void CreateRows<T>(List<T> list, StreamWriter sw)
        {
            foreach (var item in list)
            {
                PropertyInfo[] properties = typeof(T).GetProperties();
                for (int i = 0; i < properties.Length - 1; i++)
                {
                    var prop = properties[i];
                    sw.Write(prop.GetValue(item) + ",");
                }
                var lastProp = properties[properties.Length - 1];
                sw.Write(lastProp.GetValue(item) + sw.NewLine);
            }
        }

        private static void CreateHeader<T>(List<T> list, StreamWriter sw)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            for (int i = 0; i < properties.Length - 1; i++)
            {
                sw.Write(properties[i].Name + ",");
            }
            var lastProp = properties[properties.Length - 1].Name;
            sw.Write(lastProp + sw.NewLine);
        }
        #endregion

        #region Ticket data extraction
        public static List<CsvReportFormat> ExtractTicketDataFromCsv(string selectedCsvReportPath)
        {
            List<CsvReportFormat> ticketData = new List<CsvReportFormat>();
            MutableDataTable dt = DataTable.New.Read(selectedCsvReportPath);
            var rows = dt.Rows;
            foreach (var row in rows)
            {
                try
                {
                    var IssueType = GetSpecificFieldValue(row, "Issue Type");
                    var IssueKey = GetSpecificFieldValue(row, "Issue key");
                    var IssueId = GetSpecificFieldValue(row, "Issue id");
                    var Summary = GetSpecificFieldValue(row, "Summary");
                    var Assignee = GetSpecificFieldValue(row, "Assignee");
                    var AssigneeId = GetSpecificFieldValue(row, "Assignee Id");
                    var Reporter = GetSpecificFieldValue(row, "Reporter");
                    var ReporterId = GetSpecificFieldValue(row, "Reporter Id");
                    var Priority = GetSpecificFieldValue(row, "IT Priority");
                    var Status = GetSpecificFieldValue(row, "Status");
                    var Resolution = GetSpecificFieldValue(row, "Resolution");
                    var RootCauseDescription = GetSpecificFieldValue(row, "Root Cause Description");
                    var RootCause = GetSpecificFieldValue(row, "Custom field (Origin)");
                    var Created = GetSpecificFieldValue(row, "Created");
                    var DueDate = GetSpecificFieldValue(row, "Due date");
                    var Updated = GetSpecificFieldValue(row, "Updated");
                    var Resolved = GetSpecificFieldValue(row, "Resolved");


                    ticketData.Add(new CsvReportFormat
                    {
                        IssueType = GetSpecificFieldValue(row, "Issue Type"),
                        IssueKey = GetSpecificFieldValue(row, "Issue key"),
                        IssueId = GetSpecificFieldValue(row, "Issue id"),
                        Summary = GetSpecificFieldValue(row, "Summary"),
                        Assignee = GetSpecificFieldValue(row, "Assignee"),
                        AssigneeId = GetSpecificFieldValue(row, "Assignee Id"),
                        Reporter = GetSpecificFieldValue(row, "Reporter"),
                        ReporterId = GetSpecificFieldValue(row, "Reporter Id"),
                        Priority = GetSpecificFieldValue(row, "IT Priority"),
                        Status = GetSpecificFieldValue(row, "Status"),
                        Resolution = GetSpecificFieldValue(row, "Resolution"),
                        RootCauseDescription = GetSpecificFieldValue(row, "Root Cause Description"),
                        Origin = GetSpecificFieldValue(row, "Custom field (Origin)"),
                        Created = GetSpecificFieldValue(row, "Created"),
                        DueDate = GetSpecificFieldValue(row, "Due date"),
                        Updated = GetSpecificFieldValue(row, "Updated"),
                        Resolved = GetSpecificFieldValue(row, "Resolved")
                    });
                }
                catch (Exception exception)
                {
                    string ticketId = GetSpecificFieldValue(row, "Issue key");
                    throw new Exception($"Failed parsing ticket: {ticketId} - {exception.Message}");
                }
            }

            return ticketData;
        }

        private static string GetSpecificFieldValue(Row row, string field)
        {
            return row.DebugValues.Where(p => p.Contains(field)).Select(q => q.Split('=')[1]).First().ToString();
        }

        private static string GetSingleSpecificFieldValue(Row row, string field)
        {
            return row.DebugValues.Where(p => p.Equals(field)).Select(q => q.Split('=')[1]).First().ToString();
        }

        private static List<string> GetSpecificFieldValues(Row row, string field)
        {
            return row.DebugValues.Where(p => p.Contains(field)).Select(q => q.Split('=')[1]).ToList();
        }
        #endregion
    }
}