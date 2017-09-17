using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HealtCare.Common.Models;
using HealtCare.Options.Annotations;

namespace HealtCare.Options.ViewModels {

    public sealed partial class EditDoctorViewModel {
        public ObservableCollection<Doctor> Doctors { get; set; }

        public EditDoctorViewModel() {
            Doctors = new ObservableCollection<Doctor>(Doctor.Doctors);
        }
    }

    public sealed partial class EditDoctorViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}