using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using HealtCare.Common.Annotations;
using HealtCare.Common.Controllers;
using Newtonsoft.Json;

namespace HealtCare.Common.Models {

    public partial class Doctor {
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

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("unvan")]
        public string Title { get; set; }

        [JsonProperty("isim")]
        public string Name { get; set; }

        [JsonProperty("resim_yolu")]
        public string ImagePath { get; set; }

        [DefaultValue(1)]
        [JsonProperty("son_hasta_numarasi", DefaultValueHandling = DefaultValueHandling.Populate)]
        public int LastPatientNumber { get; set; } = 1;

        public string HolidayEndDate {
            get => holidayEndDate;
            set {
                holidayEndDate = value;
                OnPropertyChanged(nameof(HolidayEndDate));
            }
        }

        [JsonIgnore]
        public List<Patient> Patients { get; set; } = new List<Patient>();
        
        public static Doctor InitializeDoctor(string json) {
            return JsonConvert.DeserializeObject<Doctor>(json);
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