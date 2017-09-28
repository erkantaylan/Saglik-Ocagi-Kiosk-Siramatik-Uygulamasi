using System.Windows;
using HealtCare.Common;
using HealtCare.Common.RFI;
using HealtCare.DoctorApp.ViewModels;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.ScsServices.Client;

namespace HealtCare.DoctorApp {

    public partial class Shell {
        private readonly IScsServiceClient<IDoctorService> client;

        public Shell() {
            InitializeComponent();
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            client = ScsServiceClientBuilder.CreateClient<IDoctorService>(new ScsTcpEndPoint("127.0.0.1", MagicStrings.Port));
            //client.Timeout = 5000;
            client.Connect();

            //string result = client.ServiceProxy.Login("garfield", "1234");
            LoginView.DataContext = new LoginViewModel(client.ServiceProxy);
        }
    }

}