using ActionManagement.Base;
using System.Windows.Controls;

namespace ActionManagement.View.TestRunner
{
    /// <summary>
    /// Interaction logic for TestRunnerView.xaml
    /// </summary>
    public partial class TestRunnerView : UserControl
    {
        public TestRunnerView()
        {
            InitializeComponent();
            this.DataContext = ViewModelContainer.Instance.TestRunnerVM;

        }
    }
}
