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

            var control = sender as FrameworkElement;
            if (control != null)
            {



                var itemCount = control.FindName("CrossButtonListBox") as ListBox;
                
                if (itemCount.SelectedItem != null)
                {

                    ContentControl newContentControlForGrid = new ContentControl();
                    newContentControlForGrid.ContentTemplate = control.FindResource("CrossButton") as DataTemplate;
                    _mainWindow.PatientenMonitorDynGrid.Children.Add(newContentControlForGrid);

                    Patient PatientenInstanz = _mainWindow.GetPatient();
                    ContentControl newContent = new ContentControl();
                    newContent.ContentTemplate = control.FindResource("PatientTemplate") as DataTemplate;
                    //newContent.Content = Application.Current.Resources["TestPatient2"]; // Set the content you want to display
                    newContent.Content = PatientenInstanz;

                    newContent.Width = 460;
                    newContent.Height = 220;


                    var CrossButtonBlock = control.FindName("CrossButtonBlock") as ToggleButton;
                    CrossButtonBlock.Content = newContent;
                    var popup = control.FindName("CrossButtonOptionsPopUp") as Popup;
                }
            }
        }
    }
}
