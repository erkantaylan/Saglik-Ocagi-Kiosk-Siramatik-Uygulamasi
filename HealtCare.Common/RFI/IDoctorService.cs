using Hik.Communication.ScsServices.Service;

namespace HealtCare.Common.RFI {

    [ScsService]
    public interface IDoctorService {
        /// <summary>
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>Returns Doctor Id</returns>
        string Login(string username, string password);

        string GetPatients(int userId);
        bool SetHoliday(int userId, string date);
        bool EndHoliday(int userId);
    }

}