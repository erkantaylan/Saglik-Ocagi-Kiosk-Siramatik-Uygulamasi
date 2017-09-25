using System.Windows;

namespace HealtCare.Kiosk {

    public partial class App {
        #region Overrides of Application

        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);
            Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            new Shell().Show();
        }

        #endregion
    }

}