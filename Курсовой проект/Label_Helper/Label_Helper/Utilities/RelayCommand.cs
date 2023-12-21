using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Label_Helper.Utilities
{
    // Класс RelayCommand реализует интерфейс ICommand и предоставляет удобный способ создания команд в WPF.
    class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;  // Делегат для выполнения команды
        private readonly Func<object, bool> _canExecute;  // Делегат для проверки возможности выполнения команды

        // Событие, которое возникает, когда изменяется возможность выполнения команды
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }  // Подписываемся на событие пересмотра команд
            remove { CommandManager.RequerySuggested -= value; }  // Отписываемся от события пересмотра команд
        }

        // Конструктор класса RelayCommand
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        // Метод, определяющий, может ли команда выполняться в данный момент
        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);

        // Метод, выполняющий логику команды
        public void Execute(object parameter) => _execute(parameter);
    }
}
