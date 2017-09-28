using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using HealtCare.Common.Models;

namespace HealtCare.Common.Controllers {

    public static class PatientController {
        public static void Load() {
            try {
                List<Patient> allPatients = Loader<List<Patient>>.Load(MagicStrings.PatientsTxtLocation);
                foreach (Doctor doctor in Doctor.Doctors) {
                    doctor.Patients = new List<Patient>(allPatients.Where(t => t.DoctorId == doctor.Id));
                }
            }
            catch (UnauthorizedAccessException exception) {
                MessageBox.Show($"Dosyaya erişim izni yok:{MagicStrings.PatientsTxtLocation}\n{exception}");
            }
            catch (Exception exception) {
                MessageBox.Show($"{exception}");
            }
        }

        public static void Save() {
            try {
                List<Patient> patients = new List<Patient>();
                foreach (Doctor doctor in Doctor.Doctors) {
                    patients.AddRange(doctor.Patients);
                }
                Saver<List<Patient>>.Save(patients, MagicStrings.PatientsTxtLocation);
            }
            catch (Exception exception) {
                MessageBox.Show($"{exception}");
            }
        }
    }

}