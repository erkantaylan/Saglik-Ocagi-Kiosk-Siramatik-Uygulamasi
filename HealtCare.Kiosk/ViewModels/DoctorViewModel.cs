using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Input;
using HealtCare.Common;
using HealtCare.Common.Commands;
using HealtCare.Common.Controllers;
using HealtCare.Common.Models;
using HealtCare.Kiosk.Annotations;
using HealtCare.Kiosk.Controller;

namespace HealtCare.Kiosk.ViewModels {

    internal sealed partial class DoctorViewModel {
        private bool notAtVacation;

        public DoctorViewModel(Doctor doctor) {
            DoctorInfo = doctor;
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

        public ICommand TakeLineCommand => new ActionCommand(TakeLine, o => true);

        public Doctor DoctorInfo { get; }

        private void Doctor_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SetVacation();
        }

        private void SetVacation() {
            if (string.IsNullOrWhiteSpace(DoctorInfo.HolidayEndDate)) {
                NotAtVacation = true;
            } else {
                try {
                    DateTime dt = DateTime.ParseExact(
                        DoctorInfo.HolidayEndDate,
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

        private void TakeLine(object obj) {
            int patientNumber = GetPatientNumber();
            DoctorInfo.Patients.Add(
                new Patient(
                    DoctorInfo.Id,
                    obj.ToString(),
                    patientNumber));
            PrinterController.Print(
                $"{DoctorInfo.Title} {DoctorInfo.Name}",
                patientNumber.ToString(),
                KioskOptions.Load().HealtCareCenterName);
            PatientController.Save();
        }

        private int GetPatientNumber() {
            return DoctorInfo.LastPatientNumber++;
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