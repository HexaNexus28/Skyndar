using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPF.ViewModels
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _executeWithParam;
        private readonly Action _executeWithoutParam;
        private readonly Func<bool> _canExecute;

        // Constructeur pour les commandes SANS paramètre
        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _executeWithoutParam = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        // Constructeur pour les commandes AVEC paramètre
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _executeWithParam = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute != null)
                return _canExecute();
            if (_executeWithParam != null && parameter != null)
                return true;
            return true; // Par défaut, la commande est exécutable
        }

        public void Execute(object parameter)
        {
            if (_executeWithoutParam != null)
                _executeWithoutParam();
            else if (_executeWithParam != null)
                _executeWithParam(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
