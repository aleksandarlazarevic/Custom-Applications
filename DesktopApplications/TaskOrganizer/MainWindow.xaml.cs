using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TaskOrganizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
   {
        private string  localPhysicalFile = AppDomain.CurrentDomain.BaseDirectory + "tasks.txt";
        public ObservableCollection<string> taskList = new ObservableCollection<string>();
        public ObservableCollection<string> TaskList
        {
            get { return this.taskList; }
            set { this.SetProperty(ref this.taskList, value); }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            TaskList = new ObservableCollection<string> { "Feed the chickens", "Buy milk", "Learn Mandarin", "Shave the bunny" };
            ReadFromPhysicalFile(TaskList);
        }

        private void ReadFromPhysicalFile(ObservableCollection<string> taskList)
        {
            using (StreamReader fileRead = new StreamReader(localPhysicalFile))
            {
                string lineRead = fileRead.ReadLine();
                while (lineRead != null)
                {
                    taskList.Add(lineRead);
                    lineRead = fileRead.ReadLine();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected virtual void SetProperty<T>(
                       ref T member,
                       T val,
                       [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(member, val)) return;
            member = val;
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string textToWrite = textBox.Text;
            TaskList.Add(textToWrite);
            using (StreamWriter file =
                new StreamWriter(localPhysicalFile, true))
            {
                file.WriteLine(textToWrite);
            }
        }
    }
}
