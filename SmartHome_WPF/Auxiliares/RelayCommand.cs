using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SmartHome_WPF.Auxiliares
{
    class RelayCommand : ICommand
    { 
        private Action<object> execute;
        private Func<bool> canExecute;
    
        public event EventHandler CanExecuteChanged;

        
        public RelayCommand(Action<object> execute)
        {
            this.execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }
}
