using System.Windows;

namespace HealtCare.Kiosk {

    public partial class App {
        #region Overrides of Application

        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);
            new Shell().Show();
        }

        #endregion
    }

}