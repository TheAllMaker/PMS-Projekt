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
                    newContentControlForGrid.ContentTemplate =control.FindResource("CrossButton") as DataTemplate;
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
                    ContentControl newContent = new ContentControl();
                    newContent.ContentTemplate = control.FindResource("PatientTemplate") as DataTemplate;
                    newContent.Content = Application.Current.Resources["TestPatient2"]; // Set the content you want to display
                    var CrossButtonBlock = control.FindName("CrossButtonBlock") as ToggleButton;
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


        //private void OptionsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    //if (OptionsListBox.SelectedItem is ListBoxItem selectedOption)
        //    //{
        //    //    MessageBox.Show($"Sie haben '{selectedOption.Content}' ausgewählt.");
        //    //}

        //    //// Schließen Sie das Popup nach der Auswahl
        //    //OptionsPopup.IsOpen = false;
        //    if (OptionsListBox.SelectedItem != null)
        //    {
        //        string selectedOption = OptionsListBox.SelectedItem.ToString();

        //        // Erstellen eines neuen Buttons
        //        ContentControl newButton = new ContentControl
        //        {
        //            ContentTemplate = (DataTemplate)Resources["CrossButton"],
        //            // Weitere Eigenschaften des Buttons können hier festgelegt werden
        //        };

        //        // Fügen Sie eine Click-Ereignishandler-Methode für den neuen Button hinzu, falls erforderlich
        //        //newButton.Click += NewButton_Click;

        //        // Platzieren des Buttons im Grid
        //        PatientenMonitorDynGrid.Children.Add(newButton);

        //        // Optional: Setzen von Grid.Row und Grid.Column, wenn Sie ein mehrspaltiges/mehrreihiges Grid haben
        //        // Grid.SetRow(newButton, rowIndex);
        //        // Grid.SetColumn(newButton, columnIndex);

        //        // Zurücksetzen der Auswahl, falls gewünscht
        //        OptionsListBox.SelectedItem = null;
        //    }
        //}

    }
}
