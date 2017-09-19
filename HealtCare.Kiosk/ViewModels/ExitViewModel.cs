using System.Windows;
using System.Windows.Input;
using HealtCare.Common.Commands;

namespace HealtCare.Kiosk.ViewModels {

    internal class ExitViewModel {
        private readonly string password;
        private string current;

        public ExitViewModel(string password) {
            this.password = password;
        }

        public ICommand ExitCommand => new ActionCommand(Exit, o => true);

        private void Exit(object obj) {
            current += obj as string;
            if (current.Length >= password.Length) {
                if (current.Contains(password)) {
                    Application.Current.MainWindow.Close();
                }
            }
        }
    }

}