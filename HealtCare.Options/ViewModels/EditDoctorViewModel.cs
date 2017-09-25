using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using HealtCare.Common.Commands;
using HealtCare.Common.Models;
using HealtCare.Options.Annotations;

namespace HealtCare.Options.ViewModels {

    public sealed partial class EditDoctorViewModel {
        public EditDoctorViewModel() {
            Doctors = new ObservableCollection<Doctor>(Doctor.Doctors);
        }

        public ObservableCollection<Doctor> Doctors { get; set; }
        
        public ICommand RemoveCommand => new ActionCommand(Remove, CanRemove);

        private bool CanRemove(object obj) {
            return Doctors?.Count != 0;
        }

        private void Remove(object obj) {
            Doctor o = obj as Doctor;
            if (o != null) {
                Doctors.Remove(o);
            }
        }
    }

    public sealed partial class EditDoctorViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged( string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}