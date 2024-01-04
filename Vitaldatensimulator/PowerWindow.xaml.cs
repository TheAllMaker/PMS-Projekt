using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Vitaldatensimulator;

namespace MediTrack.View.RemoteView
{
    public partial class PowerWindow : Window
    {
        private SimulatorUI simulatorUI;
        private bool isAlreadyClosing = false;

        public PowerWindow()
        {
            InitializeComponent();
            Closing += PowerWindow_Closing;
        }

        private void Button_Click_Confirm(object sender, RoutedEventArgs e)
        {
            simulatorUI.SetAliveStatusToZero();
            Application.Current.Shutdown();
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            isAlreadyClosing = true;
            this.Close();
        }

        public void PowerWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isAlreadyClosing)
            {
                this.Close();
            }
        }
    }
}
