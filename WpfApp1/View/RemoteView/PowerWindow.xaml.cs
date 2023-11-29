using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace MediTrack.View.RemoteView
{
    public partial class PowerWindow : Window
    {
        public PowerWindow()
        {
            InitializeComponent();
            Closing += PowerWindowClosing;
        }

        private void PowerWindowClosing(object sender, CancelEventArgs e)
        {
            var confirmationDialog = new ConfirmationDialog();
            confirmationDialog.Owner = this;

            confirmationDialog.ShowDialog();

            if (!confirmationDialog.UserConfirmed)
            {
                e.Cancel = true;
            }
        }

        private void Button_Click_Confirm(object sender, RoutedEventArgs e)
        {
            // Implementierung für den Confirm-Button
            var button = sender as Button; // Wenn nötig
            // ... Weitere Aktionen beim Bestätigen
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            // Implementierung für den Cancel-Button
            var button = sender as Button; // Wenn nötig
            // ... Weitere Aktionen beim Abbrechen
        }

        public partial class ConfirmationDialog : Window
        {
            public bool UserConfirmed { get; private set; }

            private void Button_Click_Confirm(object sender, RoutedEventArgs e)
            {
                UserConfirmed = true;
                Close();
            }

            private void Button_Click_Cancel(object sender, RoutedEventArgs e)
            {
                UserConfirmed = false;
                Close();
            }
        }
    }
}
