using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using HealtCare.Common.Aggregator;
using HealtCare.Common.Commands;
using HealtCare.Common.Models;
using HealtCare.Common.RFI;
using HealtCare.DoctorApp.Annotations;
using HealtCare.DoctorApp.Views;
using HealtCare.DoctorApp.Windows;
using Newtonsoft.Json;

namespace HealtCare.DoctorApp.ViewModels {

    internal sealed partial class LoginViewModel {
        private readonly IDoctorService service;
        private string password;
        private string username;

        public LoginViewModel(IDoctorService service) {
            this.service = service;
        }


        public string Username {
            get => username;
            set {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password {
            get => password;
            set {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public ICommand LoginCommand => new ActionCommand(Login, CanLogin);


        private void Login(object obj) {
            string doctorJson = service.Login(Username, Password);
            if (string.IsNullOrWhiteSpace(doctorJson)) {
                MessageBox.Show("Kullanıcı adı veya şifre yanlış");
            } else {
                Doctor doctor = Doctor.InitializeDoctor(doctorJson);
                string patientsJson = service.GetPatients(doctor.Id);
                doctor.Patients = JsonConvert.DeserializeObject<List<Patient>>(patientsJson);
                IEventAggregator ea = new EventAggregator();
                string healtCenterName = service.GetHealtCenterName();
                CallWindow window = new CallWindow(ea, healtCenterName);
                Application.Current.MainWindow.Content = new MainView(service, doctor, ea, window);
            }
            //json parse get id, etc
        }

        private bool CanLogin(object obj) {
            if (string.IsNullOrWhiteSpace(Username)) {
                return false;
            }
            if (string.IsNullOrWhiteSpace(Password)) {
                return false;
            }
            return true;
        }
    }


    internal sealed partial class LoginViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged(string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}