using System.Windows;
using HealtCare.Common.Models;
using HealtCare.Options.Views;

namespace HealtCare.Options {

    public partial class App {
        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);
            Doctor.Load();
            ShellView view = new ShellView();
            Current.MainWindow = view;
            view.Show();
        }
    }

}