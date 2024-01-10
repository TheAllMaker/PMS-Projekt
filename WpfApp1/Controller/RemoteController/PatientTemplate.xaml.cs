using MediTrack.Model.RemoteModel;
using MediTrack.View.RemoteView;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MediTrack.Controller.RemoteController
{
    public partial class PatientTemplate : ResourceDictionary
    {
        DetailedWindow detailedWindow;
        MainWindow _mainWindow = Application.Current.MainWindow as MainWindow;
        private object _tagValue; 

        public PatientTemplate()
        {
            InitializeComponent();

        }

        private void DetailedWindowConstructor(object tagValue)
        {
            detailedWindow = new DetailedWindow(tagValue)
            {
                Title = "Threshold Editor",
                
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Tag = tagValue
            };

        }

        //private void CreateAndManageDetailedWindow(object tagValue)
        //{
        //    DetailedWindow detailedWindow = new DetailedWindow
        //    {
        //        Title = "Threshold Editor",
        //        WindowStartupLocation = WindowStartupLocation.CenterScreen,
        //        Tag = tagValue
        //    };


        //}


        //private void PatientZoomButton_Click(object sender, RoutedEventArgs e)
        //{

        //    if (WindowCounter.OpenWindows < 1)
        //    {

        //        detailedWindow.Show();
        //       // detailedWindow.Tag = ;
        //        WindowCounter.OpenWindows++;
        //        detailedWindow.Closed += (s, args) => WindowCounter.OpenWindows--;
        //    }

        //}

        private void PatientZoomButton_Click(object sender, RoutedEventArgs e)
        {
            var control = sender as FrameworkElement;
            if (WindowCounter.OpenWindows < 1)
            {
                // Holen Sie den Tag-Wert vom Sender (Button) und übergeben Sie diesen an DetailedWindowConstructor
                if (control is FrameworkElement element && element.Tag != null)
                {
                    DetailedWindowConstructor(element.Tag);
                }

                detailedWindow.Show();
                WindowCounter.OpenWindows++;
                detailedWindow.Closed += (s, args) => WindowCounter.OpenWindows--;
            }
        }

        //public DetailedWindow GetDetailedWindowInstance()
        //{
        //    return detailedWindow;
        //}


        private static class WindowCounter
        {
            public static int OpenWindows = 0;
        }

        private void MinusButton(object sender, RoutedEventArgs e)
        {
            var button = sender as FrameworkElement;
            if (button == null) return;

            // Durchlaufen der übergeordneten Elemente, bis das ContentControl gefunden wird
            var parent = VisualTreeHelper.GetParent(button);
            while (parent != null && !(parent is ContentControl))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            
            var contentControl = parent as ContentControl;
            if (contentControl != null)
            {
                _mainWindow.RemoveCrossButton();
                _mainWindow.PatientenMonitorDynGrid.Children.Remove(contentControl);
                 MainWindow.RemoteWindowCounter -= 1;
                _mainWindow.StartCrossButton();
            }

            var tag =(int) contentControl.Tag;
            OptionsData.OptionsPop(tag);
            ActiveMonitorIDManager.DeactivateMonitor(tag);


        }

        public void SetId(int ID)
        {
            int iD = ID;

        }

    }
}