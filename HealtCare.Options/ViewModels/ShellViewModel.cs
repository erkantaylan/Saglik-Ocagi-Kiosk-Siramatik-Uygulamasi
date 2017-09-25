using System.ComponentModel;
using System.Runtime.CompilerServices;
using HealtCare.Options.Annotations;

namespace HealtCare.Options.ViewModels {

    internal partial class ShellViewModel {
        
    }

    internal partial class ShellViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged( string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}