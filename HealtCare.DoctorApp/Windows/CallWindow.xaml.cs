using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media.Animation;
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

        private void TxtNumber_OnTextChanged(object sender, TextChangedEventArgs e) {
            Storyboard s = (Storyboard) TryFindResource("StoryboardTextChanged");
            s.Begin();
        }
    }

}