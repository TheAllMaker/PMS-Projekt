using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

using MediTrack.Model.RemoteModel;

using System.ComponentModel;



namespace MediTrack.View.RemoteView
{

    public partial class CrossButton : ResourceDictionary 
    {
        MainWindow _mainWindow = Application.Current.MainWindow as MainWindow;

        public CrossButton()
        {
            InitializeComponent();
        }

        private void ShowBlockOptions(object sender, RoutedEventArgs e)
        {
            try
            {
                var control = sender as FrameworkElement;
                if (control != null)
                {
                    var popup = control.FindName("CrossButtonOptionsPopUp") as Popup;
                    if (popup != null)
                    {
                        popup.IsOpen = true;
                    }
                }
            }
            catch (Exception ex)
            {
                // Fehlerbehandlung hier
                Debug.WriteLine("Fehler beim Anzeigen der Optionen: " + ex.Message);
            }
        }




        private void CrossButtonSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;

            if (listBox != null && listBox.SelectedItem != null)
            {
                var selectedValue = listBox.SelectedItem.ToString();

                if (int.TryParse(selectedValue, out int intValue))
                {
                    ActiveMonitorIDManager.InsertActiveMonitor(intValue);
                }
                else
                {
                    Console.Write("Test");
                }
            }
        }

    }
}
