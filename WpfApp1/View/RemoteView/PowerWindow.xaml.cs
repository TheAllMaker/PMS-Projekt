﻿using System;
using System.ComponentModel;
using System.Windows;

namespace MediTrack.View.RemoteView
{
    public partial class PowerWindow : Window
    {
        private bool UserConfirmed;

        public PowerWindow()
        {
            InitializeComponent();
            Closing += PowerWindowClosing;
            ShowConfirmationDialog();
        }

        private void PowerWindowClosing(object sender, CancelEventArgs e)
        {
            // Allow the window to close, regardless of UserConfirmed value
            // This will be handled by the Button_Click_Cancel method.
        }

        private void ShowConfirmationDialog()
        {
            // Assign event handlers
            ConfirmButton.Click += Button_Click_Confirm;
            CancelButton.Click += Button_Click_Cancel;
        }

        private void Button_Click_Confirm(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
