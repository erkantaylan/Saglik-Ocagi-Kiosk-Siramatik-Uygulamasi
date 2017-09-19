using HealtCare.Kiosk.ViewModels;

namespace HealtCare.Kiosk.Views {

    public partial class ExitView {
        public ExitView() {
            InitializeComponent();
            this.DataContext = new ExitViewModel("5555");
        }
    }

}