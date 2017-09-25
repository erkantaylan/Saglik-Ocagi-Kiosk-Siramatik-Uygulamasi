using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HealtCare.Common.Models;
using HealtCare.Kiosk.Annotations;

namespace HealtCare.Kiosk.ViewModels {

    internal partial class DoctorsViewModel {
        public DoctorsViewModel() {
            HealtCareName = "ERKAN TAYLAN ABDULL REZZAK BILMEM NE "
                            + "SAGLIK OCAGI AS LMT STI FUCK YOU BITCH"
                            + "";
            Doctors = new List<DoctorViewModel>(Doctor.Doctors.Count);
            foreach (Doctor doctor in Doctor.Doctors) {
                Doctors.Add(new DoctorViewModel(doctor));
            }
        }

        public List<DoctorViewModel> Doctors { get; set; }
        public int Columns { get; set; } = 3;
        public int Rows { get; set; } = 2;
        public string HealtCareName { get; set; }
    }

    internal partial class DoctorsViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}