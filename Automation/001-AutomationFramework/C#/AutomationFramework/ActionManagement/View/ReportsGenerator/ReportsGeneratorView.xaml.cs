using ActionManagement.Base;
using System.Windows.Controls;

namespace ActionManagement.View.ReportsGenerator
{
    /// <summary>
    /// Interaction logic for ReportsGenerator.xaml
    /// </summary>
    public partial class ReportsGeneratorView : UserControl
    {
        public ReportsGeneratorView()
        {
            InitializeComponent();
            this.DataContext = ViewModelContainer.Instance.ReportsGeneratorVM;
        }
    }
}
