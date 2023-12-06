using MediTrack.Model.DataBaseModelConnection;
using MediTrack.Model.RemoteModel;
using MediTrack.View.RemoteView;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.InteropServices;
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

    public partial class MainWindow : Window
    {
        Patient existingPatient = null;
        public MainWindow()
        {
            InitializeComponent();
            Loaded += InitializeComponents;
            Loaded += MainWindowLoaded;
            
        }



        //PatientTest patientTest = new PatientTest();
        //patientTest.TestPatientCall1();
        //patientTest.TestPatientCall2();

        //ContentControl contentControl = new ContentControl
        //{
        //    ContentTemplate = (DataTemplate)Resources["PatientTemplate"],
        //    Content = Application.Current.Resources["TestPatient2"],
        //    Margin = new Thickness(5)
        //};

        private void InitializeComponents(object sender, RoutedEventArgs e)
        {
            ConnectToMQTTBroker();
        }

        private void ConnectToMQTTBroker()
        {
            MqttHandler handler = new MqttHandler();
            handler.ConnectToServer();
            handler.SubScribeToTopic();
            Console.WriteLine("MQTT start done");
        }


        private async void MainWindowLoaded(object sender, RoutedEventArgs e)
        {
            while (true)
            {
                await Task.Delay(100);

                int?[] queuearray = MqttMessageQueue.Dequeue();

                if (queuearray == null)
                {
                    Console.WriteLine("still null");
                    continue;
                }

                else if (PatientDictionary.DictionaryContainer(queuearray[0]))
                {
                    Console.WriteLine("Selected MonitorID found.");
                    Patient existingPatient = PatientDictionary.DictionaryCaller(queuearray[0]);

                    // Update the properties
                    existingPatient.HeartRate = queuearray[1];
                    existingPatient.OxygenLevel = queuearray[3];
                    existingPatient.BloodPressureDiastolic = queuearray[5];
                    existingPatient.RespirationRate = queuearray[2];
                    existingPatient.BloodPressureSystolic = queuearray[4];
                    existingPatient.Temperature = queuearray[6];

                    // Trigger PropertyChanged for the updated properties
                    existingPatient.OnPropertyChanged(nameof(existingPatient.HeartRate));
                    existingPatient.OnPropertyChanged(nameof(existingPatient.OxygenLevel));
                    existingPatient.OnPropertyChanged(nameof(existingPatient.BloodPressureDiastolic));
                    existingPatient.OnPropertyChanged(nameof(existingPatient.RespirationRate));
                    existingPatient.OnPropertyChanged(nameof(existingPatient.BloodPressureSystolic));
                    existingPatient.OnPropertyChanged(nameof(existingPatient.Temperature));
                }
                else
                {
                    int? puffer = DataBaseRemoteConnection.CallMonitorIDtoPatientID(queuearray[0]);
                    string[] sstring = DataBaseRemoteConnection.CallForPatientThroughID(puffer);

                    Patient PatientenInstanz = new Patient()
                    {
                        FirstName = sstring[1],
                        LastName = sstring[0],
                        PatientNumber = puffer,
                        PatientMonitor = queuearray[0],
                        HeartRate = queuearray[1],
                        OxygenLevel = queuearray[3],
                        BloodPressureDiastolic = queuearray[5],
                        RespirationRate = queuearray[2],
                        BloodPressureSystolic = queuearray[4],
                        PatientScore = 1,
                        Temperature = queuearray[6],
                    };

                    // Create a new ContentControl for each patient
                    Dispatcher.Invoke(() =>
                    {
                        ContentControl contentControl2 = new ContentControl
                        {
                            ContentTemplate = (DataTemplate)Resources["PatientTemplate"],
                            Content = PatientenInstanz,
                            Margin = new Thickness(5)
                        };
                        PatientenMonitorDynGrid.Children.Add(contentControl2);
                    });

                    PatientDictionary.DictionaryInput(queuearray[0], PatientenInstanz);
                }
            }
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
            Window PowerWindow = new PowerWindow
            {
                Title = "Power Window",
                Width = SystemParameters.PrimaryScreenWidth * 0.75,
                Height = SystemParameters.PrimaryScreenHeight * 0.75,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            PowerWindow.Show();
            PowerWindow.Owner = this;
        }


    }
}

//private async Task StartAsyncBackGroundLogic()
//{

//    await Task.Run(async  () => 
//    { 
//    while(true)
//        {
//            // Führen Sie Ihre Logik aus

//            // Aktualisieren Sie die Benutzeroberfläche im Hauptthread
//            await Dispatcher.InvokeAsync(() =>
//            {
//                // Hier können Sie die Benutzeroberfläche aktualisieren
//                // Zum Beispiel: label.Content = counter.ToString();
//            });

//            // Warten Sie eine kurze Zeit, um die CPU nicht zu überlasten
//            await Task.Delay(1000);


//    }








//    });



//}









//PatientenMonitorDynGrid.Children.Add(contentControl);

//PatientenMonitorDynGrid.Children.Remove(contentControl);

// BackendManager.MQTTStart();
// Patient PatientenInstanz = BackendManager.mainLogic();

// ContentControl contentControl2 = new ContentControl
// {
//     ContentTemplate = (DataTemplate)Resources["PatientTemplate"],
//     //Content = Application.Current.Resources[PatientenInstanz],
//     Content = PatientenInstanz,
//     Margin = new Thickness(5)
// };
// PatientenMonitorDynGrid.Children.Add(contentControl2);


// Console.WriteLine("Stack Count" + MqttMessageQueue.Count);
////myMqtt.Disconnect();


// Debug.WriteLine("Done");

//private void API_Button_Clicked(object sender, RoutedEventArgs e)
//{
//}





//private async void MainWindowLoaded(object sender, RoutedEventArgs e)
//{

//    while (true)
//    {

//        await Task.Delay(1000);

//        int?[] queuearray = MqttMessageQueue.Dequeue();


//        if (queuearray == null)
//        {
//            Console.WriteLine("still null");
//            continue;
//        }



//        if (PatientDictionary.DictionaryContainer(queuearray[0]))
//        {
//            Console.WriteLine("Selected MonitorID found.");
//            Patient existingPatient = PatientDictionary.DictionaryCaller(queuearray[0]); ;

//            // Update the properties
//            existingPatient.HeartRate = queuearray[1];
//            existingPatient.OxygenLevel = queuearray[3];
//            existingPatient.BloodPressureDiastolic = queuearray[5];
//            existingPatient.RespirationRate = queuearray[2];
//            existingPatient.BloodPressureSystolic = queuearray[4];
//            existingPatient.Temperature = queuearray[6];

//            // Trigger PropertyChanged for the updated properties
//            existingPatient.OnPropertyChanged(nameof(existingPatient.HeartRate));
//            existingPatient.OnPropertyChanged(nameof(existingPatient.OxygenLevel));
//            existingPatient.OnPropertyChanged(nameof(existingPatient.BloodPressureDiastolic));
//            existingPatient.OnPropertyChanged(nameof(existingPatient.RespirationRate));
//            existingPatient.OnPropertyChanged(nameof(existingPatient.BloodPressureSystolic));
//            existingPatient.OnPropertyChanged(nameof(existingPatient.Temperature));

//        }

//        else
//        {
//            int? puffer = DataBaseRemoteConnection.CallMonitorIDtoPatientID(queuearray[0]);
//            string[] sstring = DataBaseRemoteConnection.CallForPatientThroughID(puffer);
//            //PatientDictionary.DictionaryInput(queuearray[0], puffer);
//            Console.WriteLine("MOID" + queuearray[0]);
//            Console.WriteLine("PID" + puffer);
//            Console.WriteLine(sstring[0]);
//            Console.WriteLine(sstring[1]);
//            Patient PatientenInstanz = new Patient()
//            {
//                FirstName = sstring[1],
//                LastName = sstring[0],
//                PatientNumber = puffer,
//                PatientMonitor = queuearray[0],
//                HeartRate = queuearray[1],
//                OxygenLevel = queuearray[3],
//                BloodPressureDiastolic = queuearray[5],
//                RespirationRate = queuearray[2],
//                BloodPressureSystolic = queuearray[4],
//                PatientScore = 1,
//                Temperature = queuearray[6],
//            };

//            existingPatient = PatientenInstanz;
//            //Dispatcher.Invoke(() =>
//            //{
//            ContentControl contentControl2 = new ContentControl
//            {
//                ContentTemplate = (DataTemplate)Resources["PatientTemplate"],
//                //Content = Application.Current.Resources[PatientenInstanz],
//                Content = existingPatient,
//                Margin = new Thickness(5)
//            };
//            PatientenMonitorDynGrid.Children.Add(contentControl2);


//            //});
//            PatientDictionary.DictionaryInput(queuearray[0], PatientenInstanz);
//        }





//    }
//}