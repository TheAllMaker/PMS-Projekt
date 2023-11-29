using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MediTrack.View.RemoteView
{
    /// <summary>
    /// Interaktionslogik für PowerWindow.xaml
    /// </summary>
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
            confirmationDialog.Owner = this; // Set the owner to center the dialog on the main window

            confirmationDialog.ShowDialog();

            if (!confirmationDialog.UserConfirmed)
            {
                e.Cancel = true; // Cancel the closing event if the user clicked "No"
            }
        }

        public partial class ConfirmationDialog : Window
        {
            public bool UserConfirmed { get; private set; }


            private void YesButton_Click(object sender, RoutedEventArgs e)
            {
                UserConfirmed = true;
                Close();
            }

            private void NoButton_Click(object sender, RoutedEventArgs e)
            {
                UserConfirmed = false;
                Close();
            }
        }
    }



    
    
    }

