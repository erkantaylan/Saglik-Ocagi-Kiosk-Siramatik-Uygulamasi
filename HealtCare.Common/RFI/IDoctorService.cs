using HealtCare.Common.Models;
using Hik.Communication.ScsServices.Service;

namespace HealtCare.Common.RFI {

    [ScsService]
    public interface IDoctorService {
        /// <summary>
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>Returns Doctor Id</returns>
        LoginResult Login(string username, string password);
    }

}