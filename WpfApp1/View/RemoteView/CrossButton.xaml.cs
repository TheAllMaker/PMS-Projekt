using MediTrack.Model.RemoteModel;
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

                    ContentControl newContentControlForGrid = new ContentControl();
                    newContentControlForGrid.ContentTemplate = control.FindResource("CrossButton") as DataTemplate;
                    //var dataTemplate = this.FindResource("CrossButton") as DataTemplate;




                    // Hinzufügen des neuen ContentControls zum nächsten UniformGrid
                    _mainWindow.PatientenMonitorDynGrid.Children.Add(newContentControlForGrid);

                    // Aktualisieren des ContentTemplates des ausgewählten CrossButtons
                    //if (PatientNe9tworkIcon.Content is ContentControl currentContentControl)
                    //{
                    //    currentContentControl.ContentTemplate = (DataTemplate)FindResource("PatientTemplate");
                    //    //currentContentControl.Content = Application.Current.Resources["TestPatient2"] /* Hier das entsprechende Content-Objekt setzen */;
                    //    //    ContentTemplate = (DataTemplate)Resources["PatientTemplate"],
                    //    //    Content = Application.Current.Resources["TestPatient2"],
                    //}

                    Patient PatientenInstanz = _mainWindow.GetPatient();

                    ContentControl newContent = new ContentControl();
                    newContent.ContentTemplate = control.Resources["PatientTemplate"] as DataTemplate;
                    newContent.Content = PatientenInstanz; // Set the content you want to display

                    //PatientNe9tworkIcon.Content = newContent;

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
