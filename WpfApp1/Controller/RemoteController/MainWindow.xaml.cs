using MediTrack.Model.DataBaseModelConnection;
using MediTrack.Model.RemoteModel;
using MediTrack.Properties;
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


    /*
     *MainWindow Class in MediTrack.Model.RemoteModel Namespace
     * 
     * Overview:
     * The MainWindow class is the colletive main logic class designed to handle all incoming MQTT messages and 
     * distribute them through the entire RemoteWindow
     * 
     * Usage:
     * 
     * Details:
     */



    public partial class MainWindow : Window
    {
        private CancellationTokenSource _cancellationTokenSource;

        public Patient PatientenInstanz;
        public static int RemoteWindowCounter;


        public MainWindow()
        {
            // UI Constructor/Intializer -> constructs the entire UI elements
            InitializeComponent();

            // Binding of the EventHandler -> after a successfull InitializeComponent we're calling 
            // for our defined fucntions inside it -> Logic Constrcutor in a nutshell
            Loaded += InitializeComponents;

            //PatientTest.TestPatientCall2();

            _cancellationTokenSource = new CancellationTokenSource();
            Loaded += async (sender, args) => await ProcessMQTTMessages(_cancellationTokenSource.Token);
        }

        private void InitializeComponents(object sender, RoutedEventArgs e)
        {
            ConnectToMQTTBroker();
            StartCrossButton();
        }

        private static void ConnectToMQTTBroker()
        {
            MqttHandler handler = new MqttHandler();
            handler.ConnectToServer();
            handler.SubScribeToTopic();
            Console.WriteLine(StringContainer.HandlerIntializer);
        }




        private void StartCrossButton()
        {
            DataTemplate crossButtonTemplate = (DataTemplate)Resources["CrossButton"];
       

            ContentControl contentControl = new ContentControl
            {
                ContentTemplate = crossButtonTemplate
            };
            contentControl.Tag = "Killme";
            contentControl.Name = "Killme";
            PatientenMonitorDynGrid.Children.Add(contentControl);
        }

        private void RemoveCrossButton()
        {
            foreach (var child in PatientenMonitorDynGrid.Children)
            {
                if (child is FrameworkElement element && element.Tag as string == "Killme")
                {
                    PatientenMonitorDynGrid.Children.Remove(child as UIElement);
                    break; 
                }
            }
        }


        private void NewCrossButton()
        {

            DataTemplate crossButtonTemplate = (DataTemplate)Resources["CrossButton"];

            ContentControl contentControl = new ContentControl
            {
                ContentTemplate = crossButtonTemplate
            };
            contentControl.Tag = "Killme";
            PatientenMonitorDynGrid.Children.Add(contentControl);
        }



        public async Task ProcessMQTTMessages(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(30);

                object[] mqttMessageQueueArray = MqttMessageQueue.Dequeue();



                // 1)  if MQTTMessages != null + 2) if UUID known + 3) IsAlive == null -> kill the patient and remove him of the dictionaries
                if ((mqttMessageQueueArray.Length != 0) && (PatientDictionary.DictionaryContainer(mqttMessageQueueArray[0])) && (mqttMessageQueueArray[8] is int value && value == 0) && (mqttMessageQueueArray[0] is int intValue01) && (ActiveMonitorIDManager.IsThisAnActiveMonitor(intValue01)))
                {
                    UuidDictionary.DictionaryRemover(mqttMessageQueueArray[0]);

                    Dispatcher.Invoke(() =>
                    {
                        //PatientenMonitorDynGrid.Children.Remove(PatientTemplateContentAddition);
                        RemoteWindowCounter -= 1;
                    });
                    PatientDictionary.DictionaryRemover(mqttMessageQueueArray[0]);
                }







                // wenn Queue mit Inhalt sowie Patient gefunden date patient up
                else if ((mqttMessageQueueArray.Length != 0) && PatientDictionary.DictionaryContainer(mqttMessageQueueArray[0]) && (mqttMessageQueueArray[0] is int intValue02) && (ActiveMonitorIDManager.IsThisAnActiveMonitor(intValue02)))
                //if ((mqttMessageQueueArray.Length != 0) && PatientDictionary.DictionaryContainer(mqttMessageQueueArray[0]))
                {

                    Console.WriteLine(StringContainer.MonitorIDFound);
                    try
                    {
                        Patient existingPatient = PatientDictionary.DictionaryCaller(mqttMessageQueueArray[0]);
                        object comparevalue = UuidDictionary.UUIDDictionaryCaller(mqttMessageQueueArray[0]);


                        string value1 = mqttMessageQueueArray[7]?.ToString();
                        string value2 = comparevalue?.ToString();

                        if (string.Equals(value1, value2))
                        {
                            existingPatient.HeartRate = mqttMessageQueueArray[1];
                            existingPatient.OxygenLevel = mqttMessageQueueArray[3];
                            existingPatient.BloodPressureDiastolic = mqttMessageQueueArray[5];
                            existingPatient.RespirationRate = mqttMessageQueueArray[2];
                            existingPatient.BloodPressureSystolic = mqttMessageQueueArray[4];
                            existingPatient.Temperature = mqttMessageQueueArray[6];


                            existingPatient.OnPropertyChanged(nameof(existingPatient.HeartRate));
                            existingPatient.OnPropertyChanged(nameof(existingPatient.OxygenLevel));
                            existingPatient.OnPropertyChanged(nameof(existingPatient.BloodPressureDiastolic));
                            existingPatient.OnPropertyChanged(nameof(existingPatient.RespirationRate));
                            existingPatient.OnPropertyChanged(nameof(existingPatient.BloodPressureSystolic));
                            existingPatient.OnPropertyChanged(nameof(existingPatient.Temperature));

                        }

                    }
                    catch (Exception ex)
                    {

                        //Überlegt euch was ihr da haben wollt 
                    }
                }

                else if ((mqttMessageQueueArray.Length != 0) && (mqttMessageQueueArray[0] is int intValue03) && (ActiveMonitorIDManager.IsThisAnActiveMonitor(intValue03)))
                {

                    object mqttDataString = DataBaseRemoteConnection.CallMonitorIDtoPatientID(mqttMessageQueueArray[0]);
                    object[] patientDataString = DataBaseRemoteConnection.CallForPatientThroughID(mqttDataString);
                    try
                    {
                        PatientenInstanz = new Patient()
                        {

                            LastName = patientDataString[0],
                            FirstName = patientDataString[1],
                            RoomNumber = patientDataString[2],
                            BedNumber = patientDataString[3],

                            PatientNumber = mqttDataString,



                            PatientMonitor = mqttMessageQueueArray[0],
                            HeartRate = mqttMessageQueueArray[1],
                            RespirationRate = mqttMessageQueueArray[2],
                            OxygenLevel = mqttMessageQueueArray[3],
                            BloodPressureSystolic = mqttMessageQueueArray[4],
                            BloodPressureDiastolic = mqttMessageQueueArray[5],
                            Temperature = mqttMessageQueueArray[6],

                        };

                        RemoveCrossButton();
                        if (RemoteWindowCounter <= 16)
                        {
                            Dispatcher.Invoke(() =>
                            {
                                ContentControl PatientTemplateContentAddition = new ContentControl
                                {
                                    ContentTemplate = (DataTemplate)Resources["PatientTemplate"],
                                    Content = PatientenInstanz,
                                    Margin = new Thickness(5)
                                };
                                PatientTemplateContentAddition.Tag = mqttMessageQueueArray[0];
                                PatientenMonitorDynGrid.Children.Add(PatientTemplateContentAddition);
                                RemoteWindowCounter += 1;
                            });


                            // do it only 16 Times 
                            if (RemoteWindowCounter <= 16)
                            {
                                NewCrossButton();
                            }
                            PatientDictionary.DictionaryInput(mqttMessageQueueArray[0], PatientenInstanz);
                            UuidDictionary.DictionaryInput(mqttMessageQueueArray[0], mqttMessageQueueArray[7]);
                        }
                    }
                    catch
                    {

                    }
                    }

                else if(mqttMessageQueueArray.Length != 0)
                {
                    OptionsData.Options.Add(mqttMessageQueueArray[0]);
                }
            }
        }


        public Patient GetPatient()
        {
            return PatientenInstanz;
        }


        private static class WindowCounter
        {
            public static int OpenWindows = 0;
        }

        private void POWER_Button_Clicked(object sender, RoutedEventArgs e)
        {
            if (WindowCounter.OpenWindows < 1)
            {

                Window PowerWindow = new PowerWindow
                {
                    Title = "Power Window",
                    Width = 800,
                    Height = 450,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen
                };
                PowerWindow.Show();
                PowerWindow.Owner = this;
                WindowCounter.OpenWindows++;
                PowerWindow.Closed += (s, args) => WindowCounter.OpenWindows--;
            }
        }


        //private void ShowOptions_Click(object sender, RoutedEventArgs e)
        //{
        //    OptionsPopup.IsOpen = true;
        //}
       

        //private void OptionsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (OptionsListBox.SelectedItem != null)
        //    {
        //        // Erstellen eines neuen ContentControls für das nächste UniformGrid
        //        ContentControl newContentControlForGrid = new ContentControl
        //        {
        //            ContentTemplate = (DataTemplate)FindResource("CrossButton"),
                    
        //            Margin = new Thickness(5)
        //        };

        //        // Hinzufügen des neuen ContentControls zum nächsten UniformGrid
        //        PatientenMonitorDynGrid.Children.Add(newContentControlForGrid);

        //        ContentControl newContent = new ContentControl();
        //        newContent.ContentTemplate = this.Resources["PatientTemplate"] as DataTemplate;
        //        //newContent.Content = Application.Current.Resources["TestPatient2"]; // Set the content you want to display
        //        newContent.Content = GetPatient();
        //        newContent.Width = 465;
        //        newContent.Height = 220;

        //        PatientNe9tworkIcon.Content = newContent;

        //        // Close the popup if necessary
        //        OptionsPopup.IsOpen = false;
        //        // Zurücksetzen der Auswahl im Popup und Schließen des Popups
        //        OptionsListBox.SelectedItem = null;
        //        OptionsPopup.IsOpen = false;
        //    }
        //}



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