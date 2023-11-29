using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;

namespace MediTrack.Controller.RemoteController
{
    public partial class SelectionWindowPatientBox : ResourceDictionary
    {
        private bool buttonClicked = false;
        private bool isGreenState = true;
        private Button clickButton;

        public SelectionWindowPatientBox()
        {
            InitializeComponent();
            WireEvents(); 
        }


        private void WireEvents()
        {
            
            Button clickButton = (Button)FindName("clickButton");

            
            if (clickButton != null)
            {
                
                clickButton.Click += ClickButton_Click;
            }
        }






        private void ClickButton_Click(object sender, RoutedEventArgs e)
        {
            if (!buttonClicked)
            {
                // Change the button color to green on the first click
                clickButton.Background = new SolidColorBrush(Colors.Green);
                buttonClicked = true;
            }
            else
            {
                // Toggle between green and red after the initial click
                clickButton.Background = isGreenState ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.Green);
                isGreenState = !isGreenState;
            }
        }
    }
}
