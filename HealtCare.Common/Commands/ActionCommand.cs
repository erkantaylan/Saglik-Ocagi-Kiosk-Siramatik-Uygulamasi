using System;
using System.Windows.Input;

namespace HealtCare.Common.Commands {

    public class ActionCommand : ICommand {
        private readonly Action<object> action;
        private readonly Predicate<object> predicate;

        public ActionCommand(Action<object> action, Predicate<object> predicate = null) {
            this.action = action ?? throw new ArgumentNullException(nameof(action), "You must specify an Action<T>.");
            this.predicate = predicate;
        }

        public bool CanExecute(object parameter) {
            return predicate == null || predicate(parameter);
        }

        public void Execute(object parameter = null) {
            action(parameter);
        }

        public event EventHandler CanExecuteChanged {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }

}