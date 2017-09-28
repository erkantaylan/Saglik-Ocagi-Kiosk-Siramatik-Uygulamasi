using HealtCare.Common.Aggregator;
using HealtCare.DoctorApp.ViewModels;

namespace HealtCare.DoctorApp.Windows {

    public partial class CallWindow {
        public CallWindow(IEventAggregator ea, string healtCenterName) {
            InitializeComponent();
            DataContext = new CallWindowViewModel(ea, healtCenterName);
        }
    }

}