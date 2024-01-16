using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using VitaldataSimulator;

namespace MediTrack.View.RemoteView
{
    public partial class PowerWindow : Window
    {
        public event EventHandler ConfirmClicked;
        private bool UserConfirmed = false;

        public PowerWindow()
        {
            InitializeComponent();
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
