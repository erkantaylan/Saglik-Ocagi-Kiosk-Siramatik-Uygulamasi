using HealtCare.Common;
using HealtCare.Common.Models;
using HealtCare.Common.RFI;
using HealtCare.DoctorApp.ViewModels;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.ScsServices.Client;
using MahApps.Metro.Controls.Dialogs;

namespace HealtCare.DoctorApp {

    public partial class Shell {
        private readonly IScsServiceClient<IDoctorService> client;

        public Shell() {
            InitializeComponent();
            client = ScsServiceClientBuilder.CreateClient<IDoctorService>(new ScsTcpEndPoint("127.0.0.1", MagicStrings.Port));
            client.Connect();
            string result = client.ServiceProxy.Login("garfield", "1234");

            LoginView.DataContext = new LoginViewModel(DialogCoordinator.Instance, client.ServiceProxy);
        }
    }

}