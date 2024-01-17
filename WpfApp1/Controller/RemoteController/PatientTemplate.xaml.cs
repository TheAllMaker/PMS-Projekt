using MediTrack.Model.DataBaseModelConnection;
using MediTrack.Model.RemoteModel;
using MediTrack.View.RemoteView;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace MediTrack.Controller.RemoteController
{
    public partial class PatientTemplate : ResourceDictionary
    {
        DetailedWindow detailedWindow;
        MainWindow _mainWindow = Application.Current.MainWindow as MainWindow;

        public PatientTemplate()
        {
            InitializeComponent();

        }

        private void DetailedWindowConstructor(int MonitorID)
        {
            detailedWindow = new DetailedWindow(MonitorID)
            {
                Title = "Threshold Editor",
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
            };

        }

        private void PatientZoomButton_Click(object sender, RoutedEventArgs e)
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
            int monitorID = Convert.ToInt32(contentControl.Tag);

            if (WindowCounter.OpenWindows < 1)
            {
                DetailedWindowConstructor(monitorID);
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
                _mainWindow.RemoveCrossButton();
                _mainWindow.PatientenMonitorDynGrid.Children.Remove(contentControl);
                 MainWindow.RemoteWindowCounter -= 1;
                _mainWindow.StartCrossButton();
            }

            int monitorID = Convert.ToInt32(contentControl.Tag);
            Threshold.RemoveThresholdByMonitorID(monitorID);
            ActiveMonitorIDManager.DeactivateMonitor(monitorID);
            PatientDictionary.DictionaryRemover(monitorID);
            object mqttDataString = DataBaseRemoteConnection.CallMonitorIDtoPatientID(monitorID);
            object[] patientDataString = DataBaseRemoteConnection.CallForPatientThroughID(mqttDataString);
            string associatedEntireValue = $"{monitorID}: {patientDataString[0]}, {patientDataString[1]}";
            OptionsData.OptionsPop(associatedEntireValue);
            UuidDictionary.DictionaryRemover(monitorID);
        }

        



        public void oxygenred()
        {

        }

    }
    public class ScaleConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double parentSize = (double)values[0];
            const double baseSize = 300; // Die Basisgröße, bei der der Text normal dargestellt wird

            double scale = Math.Min(1.0, parentSize / baseSize);

            return scale;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}