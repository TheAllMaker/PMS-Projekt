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
    /// <summary>
    /// Interaktionslogik für SelectionWindow.xaml
    /// </summary>
    public partial class SelectionWindow : Window
    {

        // boolean Var für Toogle Button
        //private bool isGreen = false;


        public SelectionWindow()
        {
            InitializeComponent();

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

        private void SelectionWindowClosing(object sender, CancelEventArgs e)
        {

        }

        private void SelectionWindowConfirmed(object sender, CancelEventArgs e)
        {
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
