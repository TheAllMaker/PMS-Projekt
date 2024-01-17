using MediTrack.Model.DataBaseModelConnection;
using MediTrack.Model.RemoteModel;
using MediTrack.View.RemoteView;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MediTrack.Controller.RemoteController
{
    public partial class PatientTemplate : ResourceDictionary
    {
        private DetailedWindow _detailedWindow;
        private readonly MainWindow _mainWindow = Application.Current.MainWindow as MainWindow;

        public PatientTemplate()
        {
            InitializeComponent();

        }

        private void DetailedWindowConstructor(int monitorId)
        {
            _detailedWindow = new DetailedWindow(monitorId)
            {
                Title = "Threshold Editor",
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
            };

        }

        private void ThresholdSettings_Click(object sender, RoutedEventArgs e)
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
                _detailedWindow.Show();
                WindowCounter.OpenWindows++;
                _detailedWindow.Closed += (s, args) => WindowCounter.OpenWindows--;
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

    }
}