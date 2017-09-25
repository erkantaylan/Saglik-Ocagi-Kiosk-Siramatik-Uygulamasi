using System;
using System.Collections.Generic;
using System.Linq;
using HealtCare.Common.Controllers;
using Newtonsoft.Json;

namespace HealtCare.Common.Models {

    public class Doctor : ISaveble {

        public Doctor() {
            if (Doctors.Count == 0) {
                Id = 1;
            } else {
                Id = Doctors.Max(o => o.Id) + 1;
            }

            Doctors.Add(this);
        }

        public static Doctor InitializeDoctor(string json) {
            return JsonConvert.DeserializeObject<Doctor>(json);
        }

        public string SerialzieDoctor() {
            return JsonConvert.SerializeObject(this);
        }

        [JsonIgnore]
        public static List<Doctor> Doctors { get; private set; } = new List<Doctor>();

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public bool AtHoliday { get; set; }
        public string HolidayEndDate { get; set; }
        [JsonIgnore]
        public List<Patient> Patients { get; set; }

        void ISaveble.Save() {
            Save();
        }

        public static void Save() {
            Saver<List<Doctor>>.Save(Doctors, MagicStrings.DoctorsTxtLocation);
        }

        public static void Load() {
            Doctors = Loader<List<Doctor>>.Load(MagicStrings.DoctorsTxtLocation);
        }
    }

}