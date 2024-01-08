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
                    //Width = SystemParameters.PrimaryScreenWidth * 0.75,
                    //Height = SystemParameters.PrimaryScreenHeight * 0.75,
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

        //private void MinusButton(object sender, RoutedEventArgs e)
        //{
        //    Debug.WriteLine("fef");
        //    if (_mainWindow.PatientenMonitorDynGrid.Children.Contains(sender as ContentControl))
        //    {
        //        _mainWindow.PatientenMonitorDynGrid.Children.Remove(sender as ContentControl );
        //    }
        //}
        //private void MinusButton(object sender, RoutedEventArgs e)
        //{
        //    Button button = sender as Button;
        //    if (button != null)
        //    {
        //        // Durchlaufen Sie die visuellen Eltern des Buttons, um das übergeordnete ContentControl zu finden.
        //        DependencyObject parent = VisualTreeHelper.GetParent(button);

        //        while (parent != null && !(parent is ContentControl))
        //        {
        //            parent = VisualTreeHelper.GetParent(parent);
        //        }

        //        if (parent is ContentControl contentControl)
        //        {
        //            // Entfernen Sie das ContentControl (PatientTemplate) aus dem UniformGrid.
        //            if (_mainWindow.PatientenMonitorDynGrid.Children.Contains(contentControl))
        //            {
        //                _mainWindow.PatientenMonitorDynGrid.Children.Remove(contentControl);
        //            }
        //        }
        //    }
        //}

        private void MinusButton(object sender, RoutedEventArgs e)
        {
            // Finden Sie das "PatientTemplateElement" im UniformGrid und entfernen Sie es.
            UIElement elementToRemove = _mainWindow.PatientenMonitorDynGrid.FindName("PatientTemplateElement") as UIElement;

            if (elementToRemove != null && _mainWindow.PatientenMonitorDynGrid.Children.Contains(elementToRemove))
            {
                _mainWindow.PatientenMonitorDynGrid.Children.Remove(elementToRemove);
            }
        }
        //        Button button = sender as Button;
        //        if (button != null)
        //        {
        //            // Durchlaufen Sie die visuellen Eltern des Buttons, um das übergeordnete ContentControl zu finden.
        //            DependencyObject parent = VisualTreeHelper.GetParent(button);

        //            while (parent != null && !(parent is ContentControl))
        //            {
        //                parent = VisualTreeHelper.GetParent(parent);
        //            }

        //            if (parent is ContentControl contentControl)
        //            {
        //                // Finden Sie das DataTemplate, das auf das ContentControl angewendet wird.
        //                DataTemplate dataTemplate = contentControl.ContentTemplate;

        //                // Erzeugen Sie ein ContentPresenter, um das DataTemplate zu laden.
        //                ContentPresenter contentPresenter = new ContentPresenter();
        //                contentPresenter.ContentTemplate = dataTemplate;

        //                // Greifen Sie auf das geladene Inhaltselement (UIElement) zu.
        //                UIElement contentElement = contentPresenter.Content as UIElement;

        //                // Entfernen Sie das gefundene Inhaltselement aus dem UniformGrid.
        //                if (contentElement != null && _mainWindow.PatientenMonitorDynGrid.Children.Contains(contentElement))
        //                {
        //                    _mainWindow.PatientenMonitorDynGrid.Children.Remove(contentElement);
        //                }
        //            }
        //        }
        //    }

        //}
    }
}