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
using HealtCare.Common.Commands;
using HealtCare.Common.Models;
using HealtCare.Common.RFI;
using HealtCare.DoctorApp.Annotations;
using Newtonsoft.Json;

namespace HealtCare.DoctorApp.ViewModels {

    internal sealed partial class MainViewModel {
        private readonly Doctor doctor;
        private ObservableCollection<Patient> patients;
        private DateTime selectedDate = DateTime.Now;
        private readonly IDoctorService service;

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

        public ICommand CallCommand => new ActionCommand(Call, o => true);
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

        public string Message { get; set; }

        private bool CanDisableKioskWithDate(object obj) {
            return true;
        }

        private void DisableKioskWithDate(object obj) {
            MessageBox.Show(SelectedDate.ToString("D", new CultureInfo("Tr-tr")));
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
                    Patients = new ObservableCollection<Patient>(list);
                }).Start();
        }

        private void DisableKiosk(object obj) {
            service.SetHoliday(doctor.Id, DateTime.Now.ToString(MagicStrings.DateFormat));
        }

        private void SendText(object obj) {
            throw new NotImplementedException();
        }

        private void Remove(object obj) {
            throw new NotImplementedException();
        }

        private void Call(object obj) {
            throw new NotImplementedException();
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