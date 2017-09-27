using HealtCare.Common.Models;
using HealtCare.Common.RFI;
using HealtCare.DoctorApp.ViewModels;

namespace HealtCare.DoctorApp.Views {

    public partial class MainView {

        public MainView() { }

        public MainView(IDoctorService service, Doctor doctor) {
            InitializeComponent();
            DataContext = new MainViewModel(service, doctor);
        }
    }

}