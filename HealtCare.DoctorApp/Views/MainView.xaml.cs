using HealtCare.Common.Aggregator;
using HealtCare.Common.Models;
using HealtCare.Common.RFI;
using HealtCare.DoctorApp.ViewModels;
using HealtCare.DoctorApp.Windows;

namespace HealtCare.DoctorApp.Views {

    public partial class MainView {

        public MainView(IDoctorService service, Doctor doctor, IEventAggregator ea, CallWindow window) {
            InitializeComponent();
            DataContext = new MainViewModel(service, doctor, ea);
            screenArranger.DataContext = new ScreenArrangerViewModel(window); 

        }
    }

}