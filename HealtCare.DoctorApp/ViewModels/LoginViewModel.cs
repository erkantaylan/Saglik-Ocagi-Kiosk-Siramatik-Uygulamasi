using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using HealtCare.Common;
using HealtCare.Common.Aggregator;
using HealtCare.Common.Commands;
using HealtCare.Common.Models;
using HealtCare.Common.RFI;
using HealtCare.DoctorApp.Annotations;
using HealtCare.DoctorApp.Views;
using HealtCare.DoctorApp.Windows;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.ScsServices.Client;
using Newtonsoft.Json;

namespace HealtCare.DoctorApp.ViewModels {

    internal sealed partial class LoginViewModel {
        private const string IpTxtPath = "KioskIp";

        private bool canLogin = true;
        private string kioskIp;
        private string password;
        private string username;

        public LoginViewModel() {
            if (File.Exists(IpTxtPath)) {
                string ip = File.ReadAllText(IpTxtPath);
                KioskIp = ip.Trim();
            }
        }


        public string KioskIp {
            get => kioskIp;
            set {
                kioskIp = value;
                OnPropertyChanged(nameof(KioskIp));
            }
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
            canLogin = false;
            new Thread(
                () => {
                    try {
                        IScsServiceClient<IDoctorService> client = ScsServiceClientBuilder.CreateClient<IDoctorService>(
                            new ScsTcpEndPoint(
                                KioskIp,
                                MagicStrings.Port));
                        client.Timeout = 5000;
                        client.Connect();
                        IDoctorService service = client.ServiceProxy;
                        string doctorJson = service.Login(Username, Password);
                        if (string.IsNullOrWhiteSpace(doctorJson)) {
                            MessageBox.Show("Kullanıcı adı veya şifre yanlış");
                        } else {
                            Doctor doctor = Doctor.InitializeDoctor(doctorJson);
                            string patientsJson = service.GetPatients(doctor.Id);
                            doctor.Patients = JsonConvert.DeserializeObject<List<Patient>>(patientsJson);
                            IEventAggregator ea = new EventAggregator();
                            string healtCenterName = service.GetHealtCenterName();
                            Application.Current.Dispatcher.Invoke(
                                () => {
                                    CallWindow window = new CallWindow(ea, healtCenterName);
                                    Application.Current.MainWindow.Content = new MainView(service, doctor, ea, window);
                                });
                            File.WriteAllText(IpTxtPath, KioskIp);
                        }
                    }
                    catch (Exception exception) {
                        MessageBox.Show($"Giriş başarısız\n{exception}");
                    }
                    finally {
                        canLogin = true;
                    }
                }).Start();
        }

        private bool CanLogin(object obj) {
            if (string.IsNullOrWhiteSpace(Username)) {
                return false;
            }
            if (string.IsNullOrWhiteSpace(Password)) {
                return false;
            }
            return canLogin;
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