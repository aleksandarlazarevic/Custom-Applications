using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ActionManagement.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private static readonly object Padlock = new object();

        private static BaseViewModel instance = null;

        public static BaseViewModel Instance
        {
            get
            {
                lock (Padlock)
                {
                    if (instance == null)
                    {
                        instance = new BaseViewModel();
                    }

                    return instance;
                }
            }
        }

        public static List<string> TestsList { get; internal set; }

        public BaseViewModel()
        {
            this.PropertyChanged += new PropertyChangedEventHandler(this.OnPropertyChanged);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        internal void DisplayTests()
        {

        }

        public virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("SelectedConfiguration"))
            {

            }
        }

        public virtual void SetProperty<T>(
                       ref T member,
                       T val,
                       [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(member, val))
            {
                return;
            }

            member = val;
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
