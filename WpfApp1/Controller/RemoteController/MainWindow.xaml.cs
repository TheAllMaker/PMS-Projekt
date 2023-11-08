using MediTrack.Model.RemoteModel;
using MediTrack.View.RemoteView;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace MediTrack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PatientTest patientTest = new PatientTest();
            patientTest.TestPatientCall1();
            patientTest.TestPatientCall2();

            ContentControl contentControl = new ContentControl
            {
                ContentTemplate = (DataTemplate)Resources["PatientTemplate"],
                Content = Application.Current.Resources["TestPatient2"],
                Margin = new Thickness(5)
            };


            PatientenMonitorDynGrid.Children.Add(contentControl);
            //PatientenMonitorDynGrid.Children.Remove(contentControl);


            DateTime startTime = DateTime.Now;
            TimeSpan duration = TimeSpan.FromSeconds(20);

            while (DateTime.Now - startTime < duration)
            {

                Thread.Sleep(100);
            }

            MqttHandler handler = new MqttHandler();
            handler.ConnectToServer();
            handler.SubScribeToTopic();
            
            //Console.WriteLine("Ich warte ...");
            //Console.ReadKey();
            Debug.WriteLine("Done");




        }




        private void API_Button_Clicked(object sender, RoutedEventArgs e)
        {
        }




        private void Select_Button_Clicked(object sender, RoutedEventArgs e)
        {
            // Instanziert ein neues Fenster
            Window SelectWindow = new SelectionWindow
            {
                Title = "Select Patient", // Name des neuen Fenster's
                Width = SystemParameters.PrimaryScreenWidth * 0.75,
                Height = SystemParameters.PrimaryScreenHeight * 0.75,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            SelectWindow.Show();
            SelectWindow.Owner = this;
            //SelectWindow.ShowDialog();


        }






        private void Add_Button_Clicked(object sender, RoutedEventArgs e)
        {
            Window AddNewPatient = new AddPatientWindow
            {
                Title = "Add a new Patient",
                Width = SystemParameters.PrimaryScreenWidth * 0.75,
                Height = SystemParameters.PrimaryScreenHeight * 0.75,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            AddNewPatient.Show();
            AddNewPatient.Owner = this;

        }

        private void POWER_Button_Clicked(object sender, RoutedEventArgs e)
        {

        }


    }
}

