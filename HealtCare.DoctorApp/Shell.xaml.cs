using System.Windows;
using HealtCare.DoctorApp.ViewModels;

namespace HealtCare.DoctorApp {

    public partial class Shell {
        public Shell() {
            InitializeComponent();
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            LoginView.DataContext = new LoginViewModel();
        }
    }

}