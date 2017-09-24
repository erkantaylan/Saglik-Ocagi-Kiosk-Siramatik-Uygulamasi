using System.Collections.Generic;
using HealtCare.Common.Aggregator;
using HealtCare.Common.Models;
using HealtCare.Common.RFI;
using Hik.Communication.ScsServices.Service;

namespace HealtCare.Kiosk.Services {

    public sealed class DoctorService : ScsService, IDoctorService {
        private readonly LoginResult result;
        private readonly List<Doctor> doctors;

        public DoctorService(LoginResult result, List<Doctor> doctors) {
            this.result = result;
            this.doctors = doctors;
        }

        public LoginResult Login(string username, string password) {
            foreach (Doctor doctor in doctors) {
                if (doctor.Username == username && doctor.Password == password) {
                    result.Doctor = doctor;
                    break;
                }
            }
            return result;
        }
    }

}