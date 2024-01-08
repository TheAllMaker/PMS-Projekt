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
using MediTrack.Model.RemoteModel;

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




        private void CrossButtonSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;

            if (listBox != null && listBox.SelectedItem != null)
            {
                // Der ausgewählte Wert im ListBox-Element
                var selectedValue = listBox.SelectedItem.ToString();

                if (int.TryParse(selectedValue, out int intValue))
                {
                    // Wert erfolgreich geparst, jetzt können Sie ihn verwenden
                    ActiveMonitorIDManager.InsertActiveMonitor(intValue);
                }
                else
                {
                    Console.Write("Test");
                }
            }
        }




        //ContentControl newContentControlForGrid = new ContentControl();
        //newContentControlForGrid.ContentTemplate = control.FindResource("CrossButton") as DataTemplate;
        //_mainWindow.PatientenMonitorDynGrid.Children.Add(newContentControlForGrid);

        //Patient PatientenInstanz = _mainWindow.GetPatient();
        //ContentControl newContent = new ContentControl();
        //newContent.ContentTemplate = control.FindResource("PatientTemplate") as DataTemplate;
        //newContent.Content = Application.Current.Resources["TestPatient2"]; // Set the content you want to display
        //newContent.Content = PatientenInstanz;

        //newContent.Width = 460;
        //newContent.Height = 220;


        //var CrossButtonBlock = control.FindName("CrossButtonBlock") as ToggleButton;
        ////CrossButtonBlock.Content = newContent;
        //var popup = control.FindName("CrossButtonOptionsPopUp") as Popup;


        //private void ShowOptions_Click(object sender, RoutedEventArgs e)
        //{
        //    OptionsPopup.IsOpen = true;
        //}


        //private void OptionsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (OptionsListBox.SelectedItem != null)
        //    {
        //        // Erstellen eines neuen ContentControls für das nächste UniformGrid
        //        ContentControl newContentControlForGrid = new ContentControl
        //        {
        //            ContentTemplate = (DataTemplate)FindResource("CrossButton"),

        //            Margin = new Thickness(5)
        //        };

        //        // Hinzufügen des neuen ContentControls zum nächsten UniformGrid
        //        PatientenMonitorDynGrid.Children.Add(newContentControlForGrid);

        //        ContentControl newContent = new ContentControl();
        //        newContent.ContentTemplate = this.Resources["PatientTemplate"] as DataTemplate;
        //        //newContent.Content = Application.Current.Resources["TestPatient2"]; // Set the content you want to display
        //        newContent.Content = GetPatient();
        //        newContent.Width = 465;
        //        newContent.Height = 220;

        //        PatientNe9tworkIcon.Content = newContent;

        //        // Close the popup if necessary
        //        OptionsPopup.IsOpen = false;
        //        // Zurücksetzen der Auswahl im Popup und Schließen des Popups
        //        OptionsListBox.SelectedItem = null;
        //        OptionsPopup.IsOpen = false;
        //    }
        //}


    }
}
