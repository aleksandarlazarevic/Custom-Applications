using ActionManagement.ViewModel;
using System.Collections.Generic;

namespace ActionManagement.Base
{
    public class AppModel
    {
        private static AppModel singleInstance;

        public static AppModel Instance
        {
            get
            {
                if (singleInstance == null)
                {
                    singleInstance = new AppModel();
                }

                return singleInstance;
            }
        }

        public List<string> TestsList { get; set; }

        public string SelectedTest { get; set; }

        public void InitSystem()
        {
            this.UpdateVMsFromModel();
        }

        public void UpdateVMsFromModel()
        {
            BaseViewModel.TestsList = this.TestsList;
            BaseViewModel.Instance.DisplayTests();
        }
    }
}
