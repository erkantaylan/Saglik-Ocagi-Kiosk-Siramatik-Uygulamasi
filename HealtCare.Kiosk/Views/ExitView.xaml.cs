using HealtCare.Common.Models;
using HealtCare.Kiosk.ViewModels;

namespace HealtCare.Kiosk.Views {

    public partial class ExitView {
        public ExitView() {
            InitializeComponent();
            KioskOptions kioskOptions = KioskOptions.Load();
            DataContext = new ExitViewModel(kioskOptions.ExitPassword);
        }
    }

}