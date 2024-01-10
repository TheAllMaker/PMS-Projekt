using MediTrack.Controller.RemoteController;
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
        Threshold threshold;


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




        public void StartCrossButton()
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

        public void RemoveCrossButton()
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
                        var itemToRemove = PatientenMonitorDynGrid.Children
                            .OfType<ContentControl>()
                            .FirstOrDefault(cc => cc.Tag.Equals(mqttMessageQueueArray[0]));

                        if (itemToRemove != null)
                        {
                            RemoveCrossButton();
                            PatientenMonitorDynGrid.Children.Remove(itemToRemove);
                            StartCrossButton();
                            RemoteWindowCounter -= 1;
                            if (mqttMessageQueueArray[0] is int intValue)
                            {
                                OptionsData.OptionsPop(intValue);
                            }
                        }
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

                            //// hier muss eine ThresholdGuard Funktion rein die alle Werte mal einliest oder ähnliches und sie im ThresholdGuard vergleicht


                            ////////////////////////////////

                            existingPatient.HeartRate = mqttMessageQueueArray[1];
                            existingPatient.OxygenLevel = mqttMessageQueueArray[3];
                            existingPatient.BloodPressureDiastolic = mqttMessageQueueArray[5];
                            existingPatient.RespirationRate = mqttMessageQueueArray[2];
                            existingPatient.BloodPressureSystolic = mqttMessageQueueArray[4];
                            existingPatient.Temperature = mqttMessageQueueArray[6];

                            // Rufe ThresholdCheck auf und übergebe die Liste
                            threshold = Threshold.GetThresholdByMonitorID(Convert.ToInt32(mqttMessageQueueArray[0]));

                            if (threshold != null)
                            {
                                bool isHeartRateWithinThreshold = threshold.CheckHeartRate(Convert.ToInt32(mqttMessageQueueArray[1]));
                                bool isOxygenLevelWithinThreshold = threshold.CheckOxygenLevel(Convert.ToInt32(mqttMessageQueueArray[3]));
                                bool isBloodPressureDiastolicWithinThreshold = threshold.CheckBloodPressureDiastolic(Convert.ToInt32(mqttMessageQueueArray[5]));
                                bool isRespirationRateWithinThreshold = threshold.CheckRespirationRate(Convert.ToInt32(mqttMessageQueueArray[2]));
                                bool isBloodPressureSystolicWithinThreshold = threshold.CheckBloodPressureSystolic(Convert.ToInt32(mqttMessageQueueArray[4]));
                                bool isTemperatureWithinThreshold = threshold.CheckTemperature(Convert.ToInt32(mqttMessageQueueArray[6]));
                            }

                            // timer = timer(5000)
                            // if (mqttMessageQueueArray[0]) keine neuen Daten
                            // => trigger event für Netzwerk Icon


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
                        if (RemoteWindowCounter <= 15)
                        {
                            Dispatcher.Invoke(() =>
                            {
                                ContentControl PatientTemplateContentAddition = new ContentControl
                                {
                                    ContentTemplate = (DataTemplate)Resources["PatientTemplate"],
                                    Content = PatientenInstanz,
                                    Margin = new Thickness(5),
                                    Tag = mqttMessageQueueArray[0],
                                    //Tag = PatientenInstanz,
                                };

                                //(PatientTemplateContentAddition.Content as PatientTemplate).MqttMessageQueueItem = mqttMessageQueueArray[0];
                                PatientenMonitorDynGrid.Children.Add(PatientTemplateContentAddition);
                                RemoteWindowCounter += 1;
                                //CreateAndManageDetailedWindow(patientTemplateContentAddition.Tag);
                            });


                            // do it only 16 Times 
                            if (RemoteWindowCounter <= 15)
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

                else if (mqttMessageQueueArray.Length != 0)
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