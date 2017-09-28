using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using HealtCare.Common.Commands;
using HealtCare.DoctorApp.Annotations;
using HealtCare.DoctorApp.Windows;

namespace HealtCare.DoctorApp.ViewModels {

    internal sealed partial class ScreenArrangerViewModel {
        private Screen selectedScreen;
        private CallWindow window;

        public ScreenArrangerViewModel(CallWindow window) {
            this.window = window;
            Screens = Screen.AllScreens.ToList();
            SelectedScreen = Screens.FirstOrDefault(t => !t.Primary);
        }

        public List<Screen> Screens { get; set; }

        public Screen SelectedScreen {
            get => selectedScreen;
            set {
                selectedScreen = value;
                OnPropertyChanged(nameof(SelectedScreen));
            }
        }

        public ICommand ShowCommand => new ActionCommand(Show, CanShow);

        public ICommand HideCommand => new ActionCommand(Hide, CanHide);

        private bool CanShow(object obj) {
            return true;
        }

        private void Show(object obj) {
            window.Show();
            window.Left = SelectedScreen.Bounds.Left;
            window.Top = SelectedScreen.Bounds.Top;
            window.Height = SelectedScreen.Bounds.Height;
            window.Width = SelectedScreen.Bounds.Width;
            window.WindowState = WindowState.Maximized;
        }

        private void Hide(object obj) {
            window.Hide();
        }

        private bool CanHide(object obj) {
            return true;
        }
    }

    internal sealed partial class ScreenArrangerViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}