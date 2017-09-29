using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using HealtCare.Common;
using HealtCare.Common.Aggregator;
using HealtCare.Common.Commands;
using HealtCare.Common.Models;
using HealtCare.Common.RFI;
using HealtCare.DoctorApp.Annotations;
using Newtonsoft.Json;

namespace HealtCare.DoctorApp.ViewModels {

    internal sealed partial class MainViewModel {
        private readonly Doctor doctor;
        private readonly IEventAggregator ea;
        private readonly IDoctorService service;
        private ObservableCollection<Patient> patients;
        private DateTime selectedDate = DateTime.Now;
        private Patient selectedPatient;

        public MainViewModel(IDoctorService service, Doctor doctor, IEventAggregator ea) {
            Patients = new ObservableCollection<Patient>();
            this.service = service;
            this.doctor = doctor;
            this.ea = ea;

            DispatcherTimer timer = new DispatcherTimer {
                Interval = TimeSpan.FromSeconds(5)
            };
            timer.Tick += (sender, args) => {
                GetPatientsAsync();
            };
            GetPatientsAsync();
            timer.Start();
        }

        public ICommand CallCommand => new ActionCommand(Call, CanCall);

        public ICommand RemoveCommand => new ActionCommand(Remove, o => true);
        public ICommand SendTextCommand => new ActionCommand(SendText, o => true);
        public ICommand DisableKioskCommand => new ActionCommand(DisableKiosk, o => true);
        public ICommand EnableKioskCommand => new ActionCommand(EnableKiosk, o => true);
        public ICommand DisableKioskWithDateCommand => new ActionCommand(DisableKioskWithDate, CanDisableKioskWithDate);

        public DateTime SelectedDate {
            get => selectedDate;
            set {
                if (value >= DateTime.Now) {
                    selectedDate = value;
                    OnPropertyChanged(nameof(SelectedDate));
                }
            }
        }

        public ObservableCollection<Patient> Patients {
            get => patients;
            set {
                patients = value;
                OnPropertyChanged(nameof(Patients));
            }
        }

        public Patient SelectedPatient {
            get => selectedPatient;
            set {
                selectedPatient = value;
                OnPropertyChanged(nameof(SelectedPatient));
            }
        }

        public string Message { get; set; }

        private bool CanCall(object o) {
            return SelectedPatient != null;
        }

        private bool CanDisableKioskWithDate(object obj) {
            return true;
        }

        private void DisableKioskWithDate(object obj) {
            string date = SelectedDate.ToString("D", new CultureInfo("Tr-tr"));
            service.SetHoliday(doctor.Id, date);
        }

        private void EnableKiosk(object obj) {
            if (service.EndHoliday(doctor.Id)) {
                MessageBox.Show(
                    "Kiosk sıra alma aktif edildi.",
                    "BAŞARILI",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            } else {
                MessageBox.Show(
                    "Kiosk sıra alma aktif edilemedi",
                    "BAŞARISIZ",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void GetPatientsAsync() {
            new Thread(
                () => {
                    string listJson = service.GetPatients(doctor.Id);
                    List<Patient> list = JsonConvert.DeserializeObject<List<Patient>>(listJson);
                    if (Patients.Count != list.Count) {
                        SetPatients(list);
                    }
                }).Start();
        }

        private void SetPatients(List<Patient> list) {
            Patients = new ObservableCollection<Patient>(list);
        }
        

        private void DisableKiosk(object obj) {
            service.SetHoliday(doctor.Id, DateTime.Now.ToString(MagicStrings.DateFormat));
        }

        private void SendText(object obj) {
            ea.PublishEvent(Message);
        }

        private void Remove(object obj) {
            try {
                RemovePatient();
            }
            catch (Exception exception) {
                MessageBox.Show($"{exception}");
            }
        }

        private void Call(object obj) {
            try {
                ea.PublishEvent(SelectedPatient);
                RemovePatient();
            }
            catch (Exception exception) {
                MessageBox.Show($"{exception}");
            }
        }

        private void RemovePatient() {
            service.RemovePatient(doctor.Id, SelectedPatient.No);
            Patients.Remove(SelectedPatient);
            SelectedPatient = null;
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