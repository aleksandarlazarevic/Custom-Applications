using ActionManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionManagement.Base
{
    public class ViewModelContainer
    {
        private static ViewModelContainer viewModelContainer;

        private ViewModelContainer()
        {
            this.InitializeVMs();
        }

        public static ViewModelContainer Instance
        {
            get
            {
                return viewModelContainer;
            }
        }

        public TestRunnerViewModel TestRunnerVM
        {
            get; set;
        }

        public TroubleshootingViewModel TroubleshootingVM
        {
            get; set;
        }

        public ReportsGeneratorViewModel ReportsGeneratorVM
        {
            get; set;
        }

        public static void Initialize()
        {
            if (viewModelContainer == null)
            {
                viewModelContainer = new ViewModelContainer();
            }
        }

        private void InitializeVMs()
        {
            this.TestRunnerVM = new TestRunnerViewModel();
            this.TroubleshootingVM = new TroubleshootingViewModel();
            this.ReportsGeneratorVM = new ReportsGeneratorViewModel();
        }
    }
}
