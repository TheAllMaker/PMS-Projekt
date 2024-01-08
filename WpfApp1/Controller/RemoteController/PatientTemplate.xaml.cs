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

        MainWindow _mainWindow = Application.Current.MainWindow as MainWindow;

        public PatientTemplate()
        {
            InitializeComponent();
        }

        private void PatientZoomButton_Click(object sender, RoutedEventArgs e)
        {

            if (WindowCounter.OpenWindows < 1)
            {
                DetailedWindow detailedWindow = new DetailedWindow
                {
                    Title = "Threshold Editor",

                    WindowStartupLocation = WindowStartupLocation.CenterScreen
                };
                detailedWindow.Show();
                WindowCounter.OpenWindows++;
                detailedWindow.Closed += (s, args) => WindowCounter.OpenWindows--;
            }

        }

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
                _mainWindow.PatientenMonitorDynGrid.Children.Remove(contentControl);
                 MainWindow.RemoteWindowCounter -= 1;
            }

            var tag =(int) contentControl.Tag;
            OptionsData.OptionsPop(tag);
            ActiveMonitorIDManager.DeactivateMonitor(tag);


        }

    }
}