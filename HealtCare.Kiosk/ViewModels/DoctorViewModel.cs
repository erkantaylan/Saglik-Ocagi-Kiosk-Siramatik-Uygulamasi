using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using HealtCare.Common;
using HealtCare.Common.Aggregator;
using HealtCare.Common.Commands;
using HealtCare.Common.Models;
using HealtCare.Kiosk.Annotations;

namespace HealtCare.Kiosk.ViewModels {

    internal sealed partial class DoctorViewModel {
        private readonly Doctor doctor;
        private bool notAtVacation;

        public DoctorViewModel(Doctor doctor) {
            this.doctor = doctor;
            Name = doctor.Title + " " + doctor.Name;
            ImagePath = doctor.ImagePath;
            doctor.PropertyChanged += Doctor_PropertyChanged;
            SetVacation();
        }

        public bool NotAtVacation {
            get => notAtVacation;
            set {
                notAtVacation = value;
                OnPropertyChanged(nameof(NotAtVacation));
            }
        }

        public string Name { get; set; }
        public string ImagePath { get; set; }

        public ICommand TakeLineCommand => new ActionCommand(TakeOrder, o => true);

        private void Doctor_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SetVacation();
        }

        private void SetVacation() {
            if (string.IsNullOrWhiteSpace(doctor.HolidayEndDate)) {
                NotAtVacation = true;
            } else {
                try {
                    DateTime dt = DateTime.ParseExact(
                        doctor.HolidayEndDate,
                        MagicStrings.DateFormat,
                        CultureInfo.CurrentCulture);
                    NotAtVacation = !CalculateVacation(dt);
                }
                catch {
                    NotAtVacation = true;
                }
            }
        }

        private static bool CalculateVacation(DateTime dt) {
            DateTime today = DateTime.Now;
            if (today.Year > dt.Year) {
                return false;
            }
            if (today.Month > dt.Month) {
                return false;
            }
            if (today.Day > dt.Day) {
                return false;
            }
            return true;
        }

        private void TakeOrder(object obj) {
            doctor.Patients.Add(
                new Patient(
                    doctor.Id,
                    obj.ToString(),
                    GetPatientNumber()));
            MessageBox.Show($"{Name}|{obj}");
        }

        private int GetPatientNumber() {
            return doctor.LastPatientNumber++;
        }
    }

    internal sealed partial class DoctorViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged(string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}