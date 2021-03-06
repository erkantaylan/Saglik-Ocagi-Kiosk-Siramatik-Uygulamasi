﻿using System.ComponentModel;
using System.Windows.Input;
using HealtCare.Common.Commands;
using HealtCare.Common.Models;
using HealtCare.Options.Annotations;

namespace HealtCare.Options.ViewModels {

    internal sealed partial class OthersViewModel {
        public OthersViewModel() {
            KioskOptions o = KioskOptions.Load();
            if (o != null) {
                Name = o.HealtCareCenterName;
                Password = o.ExitPassword;
            }
        }

        public int Rows { get; set; } = 2;
        public int Columns { get; set; } = 3;
        public string Name { get; set; }
        public string Password { get; set; }
        public ICommand SaveCommand => new ActionCommand(Save, CanSave);

        private bool CanSave(object obj) {
            if (string.IsNullOrWhiteSpace(Name)) {
                return false;
            }
            if (string.IsNullOrWhiteSpace(Password)) {
                return false;
            }
            return true;
        }

        private void Save(object obj) {
            KioskOptions o = new KioskOptions {
                ExitPassword = Password,
                HealtCareCenterName = Name,
                Rows = Rows,
                Columns = Columns
            };
            KioskOptions.Save(o);
        }
    }

    internal sealed partial class OthersViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged(string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}