using ActionManagement.Base;
using ActionManagement.Commands;
using ActionManagement.HelperActions;
using System;
using System.Threading;
using System.Windows.Forms;

namespace ActionManagement.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private BaseViewModel currentViewModel;

        public RelayCommandParameterized<string> NavigationCommand { get; set; }

        public BaseViewModel CurrentViewModel
        {
            get { return this.currentViewModel; }
            set { this.SetProperty(ref this.currentViewModel, value); }
        }

        public MainWindowViewModel()
        {
            this.NavigationCommand = new RelayCommandParameterized<string>(this.OnNavigate);
            this.CurrentViewModel = ViewModelContainer.Instance.TestRunnerVM;
        }

        private void OnNavigate(string destination)
        {
            switch (destination)
            {
                case "testRunner":
                    this.CurrentViewModel = new TestRunnerViewModel();
                    break;
                case "troubleshooting":
                    this.CurrentViewModel = ViewModelContainer.Instance.TroubleshootingVM;
                    break;
                case "reportsGenerator":
                    this.CurrentViewModel = ViewModelContainer.Instance.ReportsGeneratorVM;
                    break;
            }
        }
    }
}
