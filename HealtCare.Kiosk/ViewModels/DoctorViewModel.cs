using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using HealtCare.Common.Commands;
using HealtCare.Kiosk.Annotations;

namespace HealtCare.Kiosk.ViewModels {

    internal partial class DoctorViewModel {
        public ICommand NormalCommand => new ActionCommand(TakeOrder, CanTakeOrder);
        public ICommand PregnantCommand => new ActionCommand(TakeOrder, CanTakeOrder);
        public ICommand DisabledCommand => new ActionCommand(TakeOrder, CanTakeOrder);
        public ICommand OldCommand => new ActionCommand(TakeOrder, CanTakeOrder);
        public ICommand ChildCommand => new ActionCommand(TakeOrder, CanTakeOrder);

        private bool CanTakeOrder(object obj) {
            return true;
        }

        private void TakeOrder(object obj) {
            
        }
    }

    internal partial class DoctorViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}