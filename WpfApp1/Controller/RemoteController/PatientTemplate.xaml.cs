using MediTrack.View.RemoteView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MediTrack.Controller.RemoteController
{
    public partial class PatientTemplate : ResourceDictionary 
    {
        public PatientTemplate()
        {
            InitializeComponent();
        }

        private void PatientZoomButton_Click(object sender, RoutedEventArgs e)
        {

            if (WindowCounter.OpenWindows < 1)
            {
                DetailedWindow detailedWindow = new DetailedWindow();
                detailedWindow.Show();
                WindowCounter.OpenWindows++;
                detailedWindow.Closed += (s, args) => WindowCounter.OpenWindows--;


            }
        }

        private static class WindowCounter
        {
            public static int OpenWindows = 0;
        }

        //public partial class PatientTemplate : Window
        //{

        //    ////public PatientTemplate()
        //    ////{
        //    ////    InitializeComponent();
        //    ////}


        //    //public void OpenDetailed(object sender, RoutedEventArgs e)
        //    //{
        //    //    Window DetailedWindow = new Window
        //    //    {
        //    //        Title = "Cut-Off Monitor",
        //    //        Width = SystemParameters.PrimaryScreenWidth * 0.75,
        //    //        Height = SystemParameters.PrimaryScreenHeight * 0.75,
        //    //        WindowStartupLocation = WindowStartupLocation.CenterScreen
        //    //    };
        //    //    DetailedWindow.Show();
        //    //    DetailedWindow.Owner = this;
        //    //    // PatientHealthIcon.Visibility = Visibility.Collapsed;
        //    //}


        //}
    }
}