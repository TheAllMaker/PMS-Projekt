using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace MediTrack.View.RemoteView
{
    public partial class PowerWindow : Window
    {
        private bool UserConfirmed { get; set; }

        public PowerWindow()
        {
            InitializeComponent();
            Closing += PowerWindowClosing;
        }

        private void PowerWindowClosing(object sender, CancelEventArgs e)
        {
            ShowConfirmationDialog();

            if (!UserConfirmed)
            {
                e.Cancel = true;
            }
        }

        private void ShowConfirmationDialog()
        {
            // Weisen Sie die Ereignishandler zu
            ConfirmButton.Click += Button_Click_Confirm;
            CancelButton.Click += Button_Click_Cancel;
        }

        private void Button_Click_Confirm(object sender, RoutedEventArgs e)
        {
            UserConfirmed = true;
            Application.Current.Shutdown();
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            UserConfirmed = false;
            Close();
        }

        private void Button_Click_Confirm_PowerWindow(object sender, RoutedEventArgs e)
        {
            // Implementierung für den Confirm-Button
            var button = sender as Button; // Wenn nötig
            // ... Weitere Aktionen beim Bestätigen
            this.Close(); // Schließt das Fenster
        }

        private void Button_Click_Cancel_PowerWindow(object sender, RoutedEventArgs e)
        {
            // Implementierung für den Cancel-Button
            var button = sender as Button; // Wenn nötig
            // ... Weitere Aktionen beim Abbrechen
        }

        
    }
}
