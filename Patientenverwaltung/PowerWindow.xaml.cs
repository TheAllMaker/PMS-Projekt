using System.ComponentModel;
using System.Windows;

namespace Patientenverwaltung
{
    public partial class PowerWindow : Window
    {
        private bool UserConfirmed { get; set; }

        public PowerWindow()
        {
            InitializeComponent();
            Closing += PowerWindowClosing;
            ShowConfirmationDialog();
        }

        private void PowerWindowClosing(object sender, CancelEventArgs e)
        {
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
    }
}
