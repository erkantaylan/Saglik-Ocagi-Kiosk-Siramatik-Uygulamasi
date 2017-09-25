﻿using System;
using System.ComponentModel;
using HealtCare.Common;
using HealtCare.Common.Aggregator;
using HealtCare.Common.Models;
using HealtCare.Common.RFI;
using HealtCare.Kiosk.Services;
using HealtCare.Kiosk.ViewModels;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.ScsServices.Service;

namespace HealtCare.Kiosk {

    public partial class Shell {
        private IScsServiceApplication server;

        public Shell() {
            InitializeComponent();
            ContentRendered += Shell_ContentRendered;
            Closing += Shell_Closing;
        }

        private void Shell_Closing(object sender, CancelEventArgs e) {
            server.Stop();
        }

        private void Shell_ContentRendered(object sender, EventArgs e) {
            Doctor.Load();
            server = ScsServiceBuilder.CreateService(new ScsTcpEndPoint(MagicStrings.Port));
            LoginResult result = new LoginResult();
            result.EventAggregator = new EventAggregator();
            DoctorService service = new DoctorService(result, Doctor.Doctors);
            server.AddService<IDoctorService, DoctorService>(service);
            server.Start();
            //items.ItemsSource = Doctor.Doctors;
            DoctorsView.DataContext = new DoctorsViewModel();
        }
    }

}