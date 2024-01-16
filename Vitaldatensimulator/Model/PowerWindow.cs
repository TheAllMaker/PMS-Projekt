using System;
using System.Windows;

namespace VitaldataSimulator.Model
{
    public partial class PowerWindow
    {
        public event EventHandler ConfirmClicked;

        public PowerWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_Confirm(object sender, RoutedEventArgs e)
        {
            ConfirmClicked?.Invoke(this, EventArgs.Empty);
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}