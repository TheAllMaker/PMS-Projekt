using MediTrack.Model.DataBaseModelConnection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    
    public partial class SelectionWindow : Window
    {
        //private bool buttonClicked = false;
        //private bool isGreenState = true;
        // boolean Var für Toogle Button
        //private bool isGreen = false;


        public SelectionWindow()
        {
            InitializeComponent();
            Loaded += SelectionWindow_Loaded;
            // Anfrage an Database -> alle Einträge auslesen 
            // Dann per :
            //ContentControl contentControl = new ContentControl
            //{
            //    ContentTemplate = (DataTemplate)Resources[""],
            //    Content = Application.Current.Resources["TestPatient2"],
            //    Margin = new Thickness(5)
            //};
            // als Block einbinden und alle Einträge ausgeben
            // beim ersten Start alle Blöcke grau 
            // beim zweiten Betätigen grün 
            // :
            //PatientenMonitorDynGrid.Children.Add(contentControl);
            // wenn nochmal Block auf rot stellen und folgende Aktion ausführen:
            //PatientenMonitorDynGrid.Children.Remove(contentControl);

            // Weiterhin brauchen wir zwei Knöpfe die Ok und Abbrechen heißen
            // Bei okay werden die Blöcke in Ihrem Zustand gehidet also gespeichert
            // Bei Abbruch und ohne Änderung sollen sie wie gehidet werden.
            // Fenster nicht schließen da sonst neu instanziert werden muss
        }


        private void SelectionWindow_Loaded(object sender, RoutedEventArgs e)
        {
            InputCollector();
        }

        private void InputCollector()
        {
            DataBaseRemoteConnection.DataBaseEntireEntryCall();
            int counterstrike = DataBaseRemoteConnection.DataBaseEntries.Count();
            List<int> ListInput = new List<int>();
            ListInput.AddRange(DataBaseRemoteConnection.DataBaseEntries);
            Console.WriteLine(counterstrike);


            foreach(object varI in ListInput )
            {

                ContentControl SelectionUI = new ContentControl
                {
                    ContentTemplate = (DataTemplate)Resources["SelectionWindowPatientBox"],
                    Content = varI,
                    Margin = new Thickness(5)
                };
                BoxInputGrid.Children.Add(SelectionUI);
            }


        }


        private void Button_Click_Confirm(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        




        // simpler ToggleButton: Dadurch das wir oben den Grünzustand bei der Initialisierung auf false stellen,
        // wird beim ersten Durchlaufen dieser auf grün geschaltet, unsere isGreen ToggleVariable wird
        // daraufhin vom unteren Statement wieder negiert -> praktischen erreichen wir einen zwei Zustand State machine




        //private void PatientToggleButtonFeatureClicked(object sender, RoutedEventArgs e)
        //{
        //    if(!isGreen)
        //    {
        //        NameKey.Background = Brushes.Green;
        //    }
        //    else
        //    {
        //        NameKey.Background = Brushes.Red;
        //    }

        //    isGreen = !isGreen;
        //}

    }
}
