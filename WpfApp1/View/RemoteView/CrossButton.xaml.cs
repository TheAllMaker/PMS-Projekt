using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
        public CrossButton()
        {
            InitializeComponent();
        }


        private void ShowOptions_Click1(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("soyoz Neruwinlju resiuvnik");
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
