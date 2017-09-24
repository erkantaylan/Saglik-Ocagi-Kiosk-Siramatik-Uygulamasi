using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using HealtCare.Common.Aggregator;
using HealtCare.Common.Commands;
using HealtCare.Common.Models;
using HealtCare.Common.RFI;
using HealtCare.DoctorApp.Annotations;
using MahApps.Metro.Controls.Dialogs;

namespace HealtCare.DoctorApp.ViewModels {

    internal sealed partial class LoginViewModel : ISubscriber<Doctor> {
        private readonly IDialogCoordinator dialog;
        private readonly IDoctorService service;
        private string password;
        private string username;

        public LoginViewModel(IDialogCoordinator dialog, IDoctorService service) {
            this.dialog = dialog;
            this.service = service;
        }


        public string Username {
            get => username;
            set {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password {
            get => password;
            set {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public ICommand LoginCommand => new ActionCommand(Login, CanLogin);

        public async void OnEventHandler(Doctor e) {
            await dialog.ShowMessageAsync(this, "Hello from title", $"[{e.Name}|{e.Username}]");
        }

        private void Login(object obj) {
            LoginResult result = service.Login(Username, Password);
            result.EventAggregator.SubsribeEvent(this);
        }

        private bool CanLogin(object obj) {
            if (string.IsNullOrWhiteSpace(Username)) {
                return false;
            }
            if (string.IsNullOrWhiteSpace(Password)) {
                return false;
            }
            return true;
        }
    }


    internal sealed partial class LoginViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}