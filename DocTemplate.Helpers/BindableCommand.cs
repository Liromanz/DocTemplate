using System;
using System.Windows.Input;

namespace DocTemplate.Helpers
{
    public class BindableCommand : ICommand
    {
        private Action<object> _execute;
        private Func<object, bool> _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Настройка привязки команды
        /// </summary>
        /// <param name="execute">Команда для выполнения</param>
        /// <param name="canExecute">Можно ли ее выполнять</param>
        public BindableCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Метод для обработки того, можно ли выполнять метод
        /// </summary>
        /// <param name="parameter">Параметр для проверки</param>
        /// <returns>Буленов ответ да-нет</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        /// <summary>
        /// Метод для выполнения команды
        /// </summary>
        /// <param name="parameter">Команда</param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
