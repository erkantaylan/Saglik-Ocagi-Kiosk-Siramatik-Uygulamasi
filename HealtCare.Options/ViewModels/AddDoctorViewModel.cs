using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using HealtCare.Common.Commands;
using HealtCare.Common.Models;
using HealtCare.Options.Annotations;
using Microsoft.Win32;

namespace HealtCare.Options.ViewModels {

    internal partial class AddDoctorViewModel {
        private string imagePath;
        private string name;
        private string password;
        private string title;
        private string username;

        public string Name {
            get => name;
            set {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Password {
            get => password;
            set {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string Title {
            get => title;
            set {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public string ImagePath {
            get => imagePath;
            set {
                imagePath = value;
                OnPropertyChanged(nameof(ImagePath));
            }
        }

        public string Username {
            get => username;
            set {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public ICommand SaveCommand => new ActionCommand(Save, CanSave);
        public ICommand ChoosePictureCommand => new ActionCommand(ChoosePicture, o => true);

        private void ChoosePicture(object obj) {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Title = "RESİM SEÇ";
            dialog.CheckFileExists = true;
            dialog.ShowDialog();
            ImagePath = dialog.FileName;
        }

        private bool CanSave(object obj) {
            if (string.IsNullOrWhiteSpace(Username)) {
                return false;
            }
            if (string.IsNullOrWhiteSpace(Password)) {
                return false;
            }
            if (string.IsNullOrWhiteSpace(Name)) {
                return false;
            }
            if (string.IsNullOrWhiteSpace(ImagePath)) {
                return false;
            }
            if (!File.Exists(ImagePath)) {
                return false;
            }
            return true;
        }

        private void Save(object obj) {
            Doctor dr = new Doctor {
                ImagePath = ImagePath,
                Name = Name,
                Username = Username,
                Password = Password,
                Title = Title
            };
            try {
                Doctor.Save();
            }
            catch (Exception exception) {
                MessageBox.Show(exception.ToString());
            }
        }
    }

    internal partial class AddDoctorViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}