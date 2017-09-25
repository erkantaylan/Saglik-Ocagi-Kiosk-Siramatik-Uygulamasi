using System;
using System.Collections.Generic;
using System.Linq;
using HealtCare.Common.Models;
using HealtCare.Common.RFI;
using Hik.Communication.ScsServices.Service;
using Newtonsoft.Json;

namespace HealtCare.Kiosk.Services {

    public sealed class DoctorService : ScsService, IDoctorService {
        
        /// <summary>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetPatients(int userId) {
            Doctor dr = GetDoctor(userId);
            if (dr == null) {
                throw new Exception($"Doktor bilgisi bulunamadi. user_id:{userId}");
            }
            return JsonConvert.SerializeObject(dr.Patients);
        }

        /// <summary>
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public bool SetHoliday(int userId, string date) {
            try {
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

        /// <summary>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool EndHoliday(int userId) {
            Doctor dr = GetDoctor(userId);
            if (dr == null) {
                return false;
            }
            dr.HolidayEndDate = null;
            Doctor.Save();
            return true;
        }

        /// <summary>
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string Login(string username, string password) {
            Doctor dr = Doctor.Doctors.FirstOrDefault(o => o.Username == username && o.Password == password);
            if (dr == null) {
                return "";
            }
            return JsonConvert.SerializeObject(dr);
        }

        private static Doctor GetDoctor(int userId) {
            return Doctor.Doctors.FirstOrDefault(o => o.Id == userId);
        }
    }

}