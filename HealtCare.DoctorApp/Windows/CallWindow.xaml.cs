using System.ComponentModel;
using HealtCare.Common.Aggregator;
using HealtCare.DoctorApp.ViewModels;

namespace HealtCare.DoctorApp.Windows {

    public partial class CallWindow {
        public CallWindow() {
            InitializeComponent();
            Closing += CallWindow_Closing;
        }

        public CallWindow(IEventAggregator ea, string healtCenterName) {
            InitializeComponent();
            DataContext = new CallWindowViewModel(ea, healtCenterName);
        }

        private void CallWindow_Closing(object sender, CancelEventArgs e) {
            e.Cancel = true;
        }
    }

}