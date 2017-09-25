using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using HealtCare.Common.Commands;
using HealtCare.Common.Models;
using HealtCare.Kiosk.Annotations;

namespace HealtCare.Kiosk.ViewModels {

    internal partial class DoctorViewModel {
        private Doctor doctor;

        public DoctorViewModel(Doctor doctor) {
            this.doctor = doctor;
            Name = doctor.Title + " " + doctor.Name;
            ImagePath = doctor.ImagePath;
        }

        public string Name { get; set; }
        public string ImagePath { get; set; }

        public ICommand TakeLineCommand => new ActionCommand(TakeOrder, CanTakeOrder);

        private bool CanTakeOrder(object obj) {
            return true;
        }

        private void TakeOrder(object obj) {
            doctor.Patients.Add(
                new Patient {
                    DoctorId = doctor.Id,
                    Type = obj.ToString()
                });
            MessageBox.Show($"{Name}|{obj}");
        }
    }

    internal partial class DoctorViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged( string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}