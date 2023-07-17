using ActionManagement.Commands;
using ActionManagement.HelperActions;
using System;
using System.Windows;

namespace ActionManagement.ViewModel
{
    public class ReportsGeneratorViewModel : BaseViewModel
    {
        private string _selectedCsvReportPath;
        public DateTime _reportEndDate;

        public RelayCommand BrowseCsvReportCommand { get; set; }

        public string SelectedCsvReportPath
        {
            get { return this._selectedCsvReportPath; }
            set { this.SetProperty(ref this._selectedCsvReportPath, value); }
        }

        public DateTime ReportEndDate
        {
            get { return this._reportEndDate; }
            set { this.SetProperty(ref this._reportEndDate, value); }
        }

        public ReportsGeneratorViewModel()
        {
            this.BrowseCsvReportCommand = new RelayCommand(this.BrowseCsvReportFile);
            this.ReportEndDate = DateTime.Now.Date;
        }

        public void BrowseCsvReportFile()
        {
            this.SelectedCsvReportPath = FileSystemActions.OpenBrowseFileDialog();
            if (this._selectedCsvReportPath.Equals(string.Empty))
            {
                MessageBox.Show("Please select a CSV file");
                return;
            }

            ReportsCreation.CreateReport(SelectedCsvReportPath, this.ReportEndDate.AddDays(-1));
        }
    }
}
