using ActionManagement.Base;
using System.Windows.Controls;

namespace ActionManagement.View.Troubleshooting
{
    /// <summary>
    /// Interaction logic for TroubleshootingView.xaml
    /// </summary>
    public partial class TroubleshootingView : UserControl
    {
        public TroubleshootingView()
        {
            InitializeComponent();
            this.DataContext = ViewModelContainer.Instance.TroubleshootingVM;

        }
    }
}
