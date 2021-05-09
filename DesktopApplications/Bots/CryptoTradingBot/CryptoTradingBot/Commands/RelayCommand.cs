using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Windows;
using System.Windows.Input;

namespace CryptoTradingBot.Commands
{
    public class RelayCommand : ICommand
    {
        private Action targetExecuteMethod;
        private Func<bool> targetCanExecuteMethod;
        public event EventHandler CanExecuteChanged;
        public RelayCommand(Action executeMethod)
        {
            this.targetExecuteMethod = executeMethod;
        }

        public RelayCommand(Action executeMethod, Func<bool> canExecuteMethod)
        {
            this.targetExecuteMethod = executeMethod;
            this.targetCanExecuteMethod = canExecuteMethod;
        }

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

    public class RelayCommand<T> : ICommand
    {
        private Action<T> targetExecuteMethod;
        private Func<T, bool> targetCanExecuteMethod;

        public RelayCommand(Action<T> executeMethod)
        {
            this.targetExecuteMethod = executeMethod;
        }

        public RelayCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
        {
            this.targetExecuteMethod = executeMethod;
            this.targetCanExecuteMethod = canExecuteMethod;
        }

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

        // Beware - should use weak references if command instance lifetime is longer than lifetime of UI objects that get hooked up to command
        // Prism commands solve this in their implementation
        public event EventHandler CanExecuteChanged = delegate { };

        void ICommand.Execute(object parameter)
        {
            if (this.targetExecuteMethod != null)
            {
                this.targetExecuteMethod((T)parameter);
            }
        }
    }
}
