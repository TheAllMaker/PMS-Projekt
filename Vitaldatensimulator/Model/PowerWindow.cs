using System;
using System.Windows;

namespace Vitaldatensimulator
{
    public partial class PowerWindow : Window
    {
        public event EventHandler ConfirmClicked;
        private bool _isAlreadyClosing;

        public PowerWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_Confirm(object sender, RoutedEventArgs e)
        {
            _isAlreadyClosing = true;
            ConfirmClicked?.Invoke(this, EventArgs.Empty);
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            _isAlreadyClosing = true;
            Close();
        }
    }
}