﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MediTrack.Controller.RemoteController
{
    public partial class PatientTemplate : Window
    {
        public void OpenDetailed()
        {
            Window DetailedWindow = new Window
            {
                Title = "DetailWindow",
                Width = SystemParameters.PrimaryScreenWidth * 0.75,
                Height = SystemParameters.PrimaryScreenHeight * 0.75,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            DetailedWindow.Show();
            DetailedWindow.Owner = this;
        }
    }
}