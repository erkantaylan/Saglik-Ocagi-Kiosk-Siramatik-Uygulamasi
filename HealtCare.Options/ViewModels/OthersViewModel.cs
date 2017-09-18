using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using HealtCare.Common.Commands;
using HealtCare.Options.Annotations;

namespace HealtCare.Options.ViewModels {

    internal sealed partial class OthersViewModel {

        public OthersViewModel() {
            Common.Models.Options o = Common.Models.Options.Load();
            if (o != null) {
                Name = o.HealtCareCenterName;
                Password = o.ExitPassword;
            }
        }
        public string Name { get; set; }
        public string Password { get; set; }
        public ICommand SaveCommand => new ActionCommand(Save, CanSave);

        private bool CanSave(object obj) {
            if (string.IsNullOrWhiteSpace(Name)) {
                return false;
            }
            if (string.IsNullOrWhiteSpace(Password)) {
                return false;
            }
            return true;
        }

        private void Save(object obj) {
            Common.Models.Options o = new Common.Models.Options {
                ExitPassword = Password,
                HealtCareCenterName = Name
            };
            o.Save();
        }
    }

    internal sealed partial class OthersViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}