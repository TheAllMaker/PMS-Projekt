using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;


namespace MediTrack.Controller.RemoteController
{
    public partial class SelectionWindowPatientBox 
    {
        private bool isGreen = false;

        public SelectionWindowPatientBox()
        {
            InitializeComponent();
            
            
        }
        
    
        
        
        private void OnToggleButtonClicked(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                // Toggle den Zustand
                isGreen = !isGreen;

                // Aktualisiere die Button-Farbe
                UpdateButtonColor(button);
            }
        }

        private void UpdateButtonColor(Button button)
        {
            // Setze die Hintergrundfarbe basierend auf dem Zustand
            button.Background = isGreen ? new SolidColorBrush(Color.FromRgb(40, 151, 73)) : new SolidColorBrush(Color.FromRgb(172, 74, 83));

        }




    }
}
























//namespace MediTrack.Controller.RemoteController
//{
//    public partial class SelectionWindowPatientBox : ResourceDictionary
//    {
//        private bool buttonClicked = false;
//        private bool isGreenState = true;
//        private Button clickButton;

//        public SelectionWindowPatientBox()
//        {
//            InitializeComponent();
//            WireEvents();
//        }


//        private void WireEvents()
//        {
//            clickButton = (Button)FindName("clickButton");

//            if (clickButton != null)
//            {
//                clickButton.Click += ClickButton_Click;
//            }
//        }







//        private void ClickButton_Click(object sender, RoutedEventArgs e)
//        {
//            if (!buttonClicked)
//            {
//                // Change the button color to green on the first click
//                clickButton.Background = new SolidColorBrush(Colors.Green);
//                buttonClicked = true;
//            }
//            else
//            {
//                // Toggle between green and red after the initial click
//                clickButton.Background = isGreenState ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.Green);
//                isGreenState = !isGreenState;
//            }
//        }
//    }
//}
