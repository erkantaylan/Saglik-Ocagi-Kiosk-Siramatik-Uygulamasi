using System.Globalization;
using HealtCare.Common.Models;
using HealtCare.Common.RFI;
using HealtCare.DoctorApp.ViewModels;

namespace HealtCare.DoctorApp.Views {

    public partial class MainView {

        public MainView() { }

        public MainView(IDoctorService service, Doctor doctor) {
            InitializeComponent();
            //CultureInfo ci = new CultureInfo("Tr-tr");
            //ci.DateTimeFormat.LongTimePattern = "";
            //HolidayEndDate.Culture = ci;
            DataContext = new MainViewModel(service, doctor);
        }
    }

}