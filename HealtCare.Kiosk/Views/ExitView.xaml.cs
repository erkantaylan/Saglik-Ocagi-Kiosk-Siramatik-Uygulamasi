using HealtCare.Common.Models;
using HealtCare.Kiosk.ViewModels;

namespace HealtCare.Kiosk.Views {

    public partial class ExitView {
        public ExitView() {
            InitializeComponent();
            Options options = Options.Load();
            this.DataContext = new ExitViewModel(options.ExitPassword);
        }
    }

}