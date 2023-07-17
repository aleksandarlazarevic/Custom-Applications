using System;
using System.Windows.Input;

namespace ActionManagement.Commands
{
    public class RelayCommand : ICommand
    {
        private Action targetExecuteMethod;

        private Func<bool> targetCanExecuteMethod;

        public RelayCommand(Action executeMethod)
        {
            this.targetExecuteMethod = executeMethod;
        }

        public RelayCommand(Action executeMethod, Func<bool> canExecuteMethod)
        {
            this.targetExecuteMethod = executeMethod;
            this.targetCanExecuteMethod = canExecuteMethod;
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            this.CanExecuteChanged(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            if (this.targetCanExecuteMethod != null)
            {
                return this.targetCanExecuteMethod();
            }

            if (this.targetExecuteMethod != null)
            {
                return true;
            }

            return false;
        }

        public void Execute(object parameter)
        {
            if (this.targetExecuteMethod != null)
            {
                this.targetExecuteMethod();
            }
        }
    }
}
