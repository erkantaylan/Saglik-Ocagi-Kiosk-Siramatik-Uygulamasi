﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using HealtCare.Common.Aggregator;
using HealtCare.Common.Models;
using HealtCare.DoctorApp.Annotations;

namespace HealtCare.DoctorApp.ViewModels {

    internal sealed partial class CallWindowViewModel : ISubscriber<Patient>, ISubscriber<string> {
        private string message;
        private string patientNo;

        public CallWindowViewModel(IEventAggregator ea, string healtCenterName) {
            ea.SubsribeEvent(this);
        }

        public string Message {
            get => message;
            set {
                message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public string PatientNo {
            get => patientNo;
            set {
                patientNo = value;
                OnPropertyChanged(nameof(PatientNo));
            }
        }
        
        public void OnEventHandler(Patient e) {
            PatientNo = e.No.ToString();
        }

        public void OnEventHandler(string message) {
            Message = message;
        }
    }

    internal sealed partial class CallWindowViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}