using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MediTrack.View.RemoteView
{
    /// <summary>
    /// Interaktionslogik für CrossButton.xaml
    /// </summary>
    public partial class CrossButton : ResourceDictionary
    {
        MainWindow _mainWindow = Application.Current.MainWindow as MainWindow;

        public CrossButton()
        {
            InitializeComponent();
        }




        private void ShowBlockOptions(object sender, RoutedEventArgs e)
        {
            ////var popup = Application.Current.FindRe("CrossButtonOptionsPopUp") as Popup;
            //var popup = e.FindName("CrossButtonOptionsPopUp") as Popup;
            //if (popup != null)
            //{
            //    popup.IsOpen = true;
            //}
            var control = sender as FrameworkElement;
            if (control != null)
            {
                // Suchen des Popups im gleichen Namensraum wie 'sender'
                var popup = control.FindName("CrossButtonOptionsPopUp") as Popup;
                if (popup != null)
                {
                    popup.IsOpen = true;
                }
            }
        }




        //ContentControl newContent = new ContentControl();
        //newContent.ContentTemplate = this.Resources["PatientTemplate"] as DataTemplate;
        //        newContent.Content = Application.Current.Resources["TestPatient2"]; // Set the content you want to display

        //     PatientNe9tworkIcon.Content = newContent;

        //        // Close the popup if necessary
        //        OptionsPopup.IsOpen = false;
        //        // Zurücksetzen der Auswahl im Popup und Schließen des Popups
        //        OptionsListBox.SelectedItem = null;
        //        OptionsPopup.IsOpen = false;
        //    }

        private void CrossButtonSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var control = sender as FrameworkElement;
            if (control != null)
            {
                var itemCount = control.FindName("CrossButtonListBox") as ListBox;
                Console.WriteLine("ddwdw");
                if (itemCount.SelectedItem != null)
                {
                    // Create a new ContentControl for the new content
                    ContentControl newContentControlForGrid = new ContentControl();
                    newContentControlForGrid.ContentTemplate = control.FindResource("CrossButton") as DataTemplate;

                    // Set the size of the new ContentControl
                    

                    // Add the new ContentControl to the grid
                    _mainWindow.PatientenMonitorDynGrid.Children.Add(newContentControlForGrid);

                    // Create a new ContentControl for the content of CrossButtonBlock
                    ContentControl newContent = new ContentControl();
                    newContent.ContentTemplate = control.FindResource("PatientTemplate") as DataTemplate;
                    newContent.Content = Application.Current.Resources["TestPatient2"]; // Set the content you want to display

                    // Set the size of the new ContentControl
                    newContent.Width = 460;
                    newContent.Height = 220;

                    // Set the new content for CrossButtonBlock
                    var CrossButtonBlock = control.FindName("CrossButtonBlock") as Button;
                    CrossButtonBlock.Content = newContent;

                    var popup = control.FindName("CrossButtonOptionsPopUp") as Popup;
                    // Close the popup if necessary
                    //popup.IsOpen = false;
                    // Zurücksetzen der Auswahl im Popup und Schließen des Popups
                    //itemCount.SelectedItem = null;
                    //popup.IsOpen = false;
                }
            }
        }



    }
}
