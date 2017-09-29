using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HealtCare.Common.Commands;
using HealtCare.Common.Controllers;
using HealtCare.Common.Models;

namespace HealtCare.Kiosk.ViewModels {

    internal sealed class DoctorsViewModel {
        public DoctorsViewModel() {
            Doctors = new List<DoctorViewModel>(Doctor.Doctors.Count);
            foreach (Doctor doctor in Doctor.Doctors) {
                Doctors.Add(new DoctorViewModel(doctor));
            }
            KioskOptions kioskOptions = KioskOptions.Load();
            PatientController.Load();
            Columns = kioskOptions.Columns;
            Rows = kioskOptions.Rows;
            HealtCareName = kioskOptions.HealtCareCenterName;
        }

        public List<DoctorViewModel> Doctors { get; set; }
        public int Columns { get; set; }
        public int Rows { get; set; }
        public string HealtCareName { get; set; }
        public ICommand VisitorPatientCommand => new ActionCommand(VisitorPatient, o => true);

        private void VisitorPatient(object obj) {
            List<DoctorViewModel> availableDoctors = Doctors.Where(o => o.NotAtVacation).ToList();
            if (availableDoctors.Any()) {
                DoctorViewModel min = availableDoctors[0];
                foreach (DoctorViewModel dr in availableDoctors) {
                    if (min.DoctorInfo.Patients.Count > dr.DoctorInfo.Patients.Count) {
                        min = dr;
                    }
                }
                min.TakeLineCommand.Execute("Visitor");
            }
        }
    }

}