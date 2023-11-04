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
    /// Interaktionslogik für SelectionWindow.xaml
    /// </summary>
    public partial class SelectionWindow : Window
    {
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
        }



   
    }
}
