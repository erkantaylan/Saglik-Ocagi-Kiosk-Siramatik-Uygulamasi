using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using HealtCare.Common.Models;
using HealtCare.Common.RFI;
using Hik.Communication.ScsServices.Service;
using Newtonsoft.Json;

namespace HealtCare.Kiosk.Services {

    public sealed class DoctorService : ScsService, IDoctorService {
        private readonly LoginResult result;
        private readonly List<Doctor> doctors;

        public DoctorService(LoginResult result, List<Doctor> doctors) {
            this.result = result;
            this.doctors = doctors;
        }
        

        public string GetPatients(int userId) {
            Doctor dr = GetDoctor(userId);
            if (dr == null) {
                throw new Exception($"Doktor bilgisi bulunamadi. user_id:{userId}");
            }
            return JsonConvert.SerializeObject(dr.Patients);
        }

        private static Doctor GetDoctor(int userId) {
            return Doctor.Doctors.FirstOrDefault(o => o.Id == userId);
        }

        public bool SetHoliday(int userId, string date) {
            try {
                //DateTime dt = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                Doctor dr = GetDoctor(userId);
                if (dr == null) {
                    return false;
                }
                dr.HolidayEndDate = date;
                Doctor.Save();
            }
            catch {
                return false;
            }
            return true;
        }

        public bool EndHoliday(int userId) {
            Doctor dr = GetDoctor(userId);
            if (dr == null) {
                return false;
            }
            dr.HolidayEndDate = null;
            Doctor.Save();
            return true;
        }

        public string Login(string username, string password) {
            Doctor dr = Doctor.Doctors.FirstOrDefault(o => o.Username == username && o.Password == password);
            if (dr == null) {
                return "Yanlış Kullanıcı Adı veya Şifre";
            }
            return JsonConvert.SerializeObject(dr);
        }
    }

}