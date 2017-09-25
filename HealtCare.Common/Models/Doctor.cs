using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using HealtCare.Common.Annotations;
using HealtCare.Common.Controllers;
using Newtonsoft.Json;

namespace HealtCare.Common.Models {

    public partial class Doctor : ISaveble {
        private string holidayEndDate;

        public Doctor() {
            if (Doctors.Count == 0) {
                Id = 1;
            } else {
                Id = Doctors.Max(o => o.Id) + 1;
            }

            Doctors.Add(this);
        }

        [JsonIgnore]
        public static List<Doctor> Doctors { get; private set; } = new List<Doctor>();

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }

        public string HolidayEndDate {
            get => holidayEndDate;
            set {
                holidayEndDate = value;
                OnPropertyChanged(nameof(HolidayEndDate));
            }
        }

        [JsonIgnore]
        public List<Patient> Patients { get; set; } = new List<Patient>();

        void ISaveble.Save() {
            Save();
        }

        public static Doctor InitializeDoctor(string json) {
            return JsonConvert.DeserializeObject<Doctor>(json);
        }

        public string SerialzieDoctor() {
            return JsonConvert.SerializeObject(this);
        }

        public static void Save() {
            Saver<List<Doctor>>.Save(Doctors, MagicStrings.DoctorsTxtLocation);
        }

        public static void Load() {
            Doctors = Loader<List<Doctor>>.Load(MagicStrings.DoctorsTxtLocation);
        }
    }

    public partial class Doctor : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}