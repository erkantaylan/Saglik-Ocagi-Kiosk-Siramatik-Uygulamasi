using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Input;
using System.Windows.Threading;
using HealtCare.Common.Commands;
using HealtCare.Common.Models;
using HealtCare.Common.RFI;
using HealtCare.DoctorApp.Annotations;
using Newtonsoft.Json;

namespace HealtCare.DoctorApp.ViewModels {

    internal sealed partial class MainViewModel {
        private Doctor doctor;
        private IDoctorService service;
        private ObservableCollection<Patient> patients;

        public ICommand CallCommand => new ActionCommand(Call, o => true);
        public ICommand RemoveCommand => new ActionCommand(Remove, o => true);
        public ICommand SendTextCommand => new ActionCommand(SendText, o => true);
        public ICommand DisableKioskCommand => new ActionCommand(DisableKiosk, o => true);

        public ObservableCollection<Patient> Patients {
            get {
                return patients;
            }
            set {
                patients = value;
                OnPropertyChanged(nameof(Patients));
            }
        }

        public MainViewModel(IDoctorService service, Doctor doctor) {
            Patients = new ObservableCollection<Patient>();
            this.service = service;
            this.doctor = doctor;
            DispatcherTimer timer = new DispatcherTimer {
                Interval = TimeSpan.FromSeconds(5)
            };
            timer.Tick += (sender, args) => {
                GetPatientsAsync();
            };
            timer.Start();
        }

        private void GetPatientsAsync() {
            new Thread(
                () => {
                    string listJson = service.GetPatients(doctor.Id);
                    List<Patient> list = JsonConvert.DeserializeObject<List<Patient>>(listJson);
                    Patients = new ObservableCollection<Patient>(list);
                }).Start();
        }

        private void DisableKiosk(object obj) {
            throw new System.NotImplementedException();
        }

        private void SendText(object obj) {
            throw new System.NotImplementedException();
        }

        private void Remove(object obj) {
            throw new System.NotImplementedException();
        }

        private void Call(object obj) {
            throw new System.NotImplementedException();
        }
    }

    internal sealed partial class MainViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}