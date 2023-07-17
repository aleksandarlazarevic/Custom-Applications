using System;
using System.Windows.Input;

namespace ActionManagement.Commands
{
    public class RelayCommandParameterized<T> : ICommand
    {
        private Action<T> targetExecuteMethod;
        private Func<T, bool> targetCanExecuteMethod;

        public RelayCommandParameterized(Action<T> executeMethod)
        {
            this.targetExecuteMethod = executeMethod;
        }

        public RelayCommandParameterized(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
        {
            this.targetExecuteMethod = executeMethod;
            this.targetCanExecuteMethod = canExecuteMethod;
        }

        public event EventHandler CanExecuteChanged = delegate { };

        public void RaiseCanExecuteChanged()
        {
            this.CanExecuteChanged(this, EventArgs.Empty);
        }

        bool ICommand.CanExecute(object parameter)
        {
            if (this.targetCanExecuteMethod != null)
            {
                T tparm = (T)parameter;
                return this.targetCanExecuteMethod(tparm);
            }

            if (this.targetExecuteMethod != null)
            {
                return true;
            }

            return false;
        }

        void ICommand.Execute(object parameter)
        {
            if (this.targetExecuteMethod != null)
            {
                this.targetExecuteMethod((T)parameter);
            }
        }
    }
}
