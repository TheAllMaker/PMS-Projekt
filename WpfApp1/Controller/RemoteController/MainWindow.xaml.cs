using MediTrack.Model.DataBaseModelConnection;
using MediTrack.Model.RemoteModel;
using MediTrack.View.RemoteView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

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
        public Dictionary<int, Patient> patientenListe = new Dictionary<int, Patient>();
        public Dictionary<int, ContentControl> patientenDictionary = new Dictionary<int, ContentControl>();
        public List<object> mainList = new List<object>();

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
            contentControl.Tag = "FirstCrossButtonIdentifier";
            contentControl.Name = "FirstCrossButtonIdentifier";
            PatientenMonitorDynGrid.Children.Add(contentControl);
        }

        public void RemoveCrossButton()
        {
            foreach (var child in PatientenMonitorDynGrid.Children)
            {
                if (child is FrameworkElement element && element.Tag as string == "FirstCrossButtonIdentifier")
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
            contentControl.Tag = "FirstCrossButtonIdentifier";
            PatientenMonitorDynGrid.Children.Add(contentControl);
        }



        public async Task ProcessMQTTMessages(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(50);
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

                                Patient existingPatient = PatientDictionary.DictionaryCaller(intValue);
                                var varLastName = existingPatient.LastName;
                                var varFirstName = existingPatient.FirstName;
                                string AssociatedEntireValue = $"{intValue}: {varLastName}, {varFirstName}";
                                OptionsData.OptionsPop(AssociatedEntireValue);
                            }
                        }
                    });
                    PatientDictionary.DictionaryRemover(mqttMessageQueueArray[0]);
                    //UuidDictionary.DictionaryRemover(mqttMessageQueueArray[0]);
                    ActiveMonitorIDManager.DeactivateMonitor(mqttMessageQueueArray[0]);
                    mainList.Remove(mqttMessageQueueArray[0]);
                }


                // wenn Queue mit Inhalt sowie Patient gefunden date patient up
                else if ((mqttMessageQueueArray.Length != 0) && PatientDictionary.DictionaryContainer(mqttMessageQueueArray[0]) && (mqttMessageQueueArray[0] is int intValue02) && (ActiveMonitorIDManager.IsThisAnActiveMonitor(intValue02)))
                
                {
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

                            int id = Convert.ToInt32(mqttMessageQueueArray[0]);
                            threshold = Threshold.GetThresholdByMonitorID(Convert.ToInt32(mqttMessageQueueArray[0]));

                            if (threshold != null)
                            {
                                bool isHeartRateWithinThreshold = threshold.CheckHeartRate(Convert.ToInt32(mqttMessageQueueArray[1]));
                                bool isRespirationRateWithinThreshold = threshold.CheckRespirationRate(Convert.ToInt32(mqttMessageQueueArray[2]));
                                bool isOxygenLevelWithinThreshold = threshold.CheckOxygenLevel(Convert.ToInt32(mqttMessageQueueArray[3]));
                                bool isBloodPressureSystolicWithinThreshold = threshold.CheckBloodPressureSystolic(Convert.ToInt32(mqttMessageQueueArray[4]));
                                bool isBloodPressureDiastolicWithinThreshold = threshold.CheckBloodPressureDiastolic(Convert.ToInt32(mqttMessageQueueArray[5]));
                                bool isTemperatureWithinThreshold = threshold.CheckTemperature(Convert.ToInt32(mqttMessageQueueArray[6]));

                                if (patientenDictionary.ContainsKey(id))
                                {
                                    Patient patientInstance = (Patient)patientenDictionary[id].Content;

                                    patientInstance.IsHeartRateOutOfRange = !isHeartRateWithinThreshold;
                                    patientInstance.IsRespirationRateOutOfRange = !isRespirationRateWithinThreshold;
                                    patientInstance.IsOxygenLevelOutOfRange = !isOxygenLevelWithinThreshold;
                                    patientInstance.IsBloodPressureDiastolicOutOfRange = !isBloodPressureDiastolicWithinThreshold;
                                    patientInstance.IsBloodPressureSystolicOutOfRange = !isBloodPressureSystolicWithinThreshold;
                                    patientInstance.IsTemperatureOutOfRange = !isTemperatureWithinThreshold;
                                }
                            }

                            if (patientenDictionary.ContainsKey(id))
                            {
                                Patient patientInstance = (Patient)patientenDictionary[id].Content;
                                patientInstance.UpdateTimer?.Stop();


                                if (patientInstance.IsBlinking)
                                {
                                    patientInstance.IsBlinking = false;
                                    
                                }

                                DispatcherTimer timer = new DispatcherTimer
                                {
                                    Interval = TimeSpan.FromSeconds(5)
                                };

                                timer.Tick += (sender, e) =>
                                {
                                    // Hier wird der Button blinken ausgelöst
                                    patientInstance.IsBlinking = true;
                                    timer.Stop();
                                };

                                timer.Start();

                                patientInstance.UpdateTimer = timer;
                            }

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
                        int id = Convert.ToInt32(mqttMessageQueueArray[0]);
                        patientenListe.Add(id,PatientenInstanz);
                        PatientDictionary.DictionaryInput(mqttMessageQueueArray[0], PatientenInstanz);
                        UuidDictionary.DictionaryInput(mqttMessageQueueArray[0], mqttMessageQueueArray[7]);
                        string AssociatedEntireValue = $"{mqttMessageQueueArray[0]}: {patientDataString[0]}, {patientDataString[1]}";

                        OptionsData.Options.Remove(AssociatedEntireValue);
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
                                };
                                // ? 
                                patientenDictionary.Add(Convert.ToInt32(mqttMessageQueueArray[0]), PatientTemplateContentAddition);
                                PatientenMonitorDynGrid.Children.Add(PatientTemplateContentAddition);
                                RemoteWindowCounter += 1;
                            });


                            
                            if (RemoteWindowCounter <= 15)
                            {
                                NewCrossButton();
                            }

                        }
                    }
                    catch
                    {

                    }
                }

                else if (mqttMessageQueueArray.Length != 0)
                {
                    if  (!PatientDictionary.DictionaryContainer(mqttMessageQueueArray[0]) && !mainList.Contains(mqttMessageQueueArray[0]))
                    {
                        object mqttDataString =
                            DataBaseRemoteConnection.CallMonitorIDtoPatientID(mqttMessageQueueArray[0]);
                        object[] patientDataString = DataBaseRemoteConnection.CallForPatientThroughID(mqttDataString);
                        string AssociatedEntireValue =
                            $"{mqttMessageQueueArray[0]}: {patientDataString[0]}, {patientDataString[1]}";
                        OptionsData.Options.Add(AssociatedEntireValue);
                        mainList.Add(mqttMessageQueueArray[0]);
                    }
                    //else
                    //{
                    //    Patient existingPatient = PatientDictionary.DictionaryCaller(mqttMessageQueueArray[0]);
                    //    var varLastName = existingPatient.LastName;
                    //    var varFirstName = existingPatient.FirstName;
                    //    string AssociatedEntireValue = $"{mqttMessageQueueArray[0]}: {varLastName}, {varFirstName}";
                    //    OptionsData.Options.Add(AssociatedEntireValue);
                    //}
                    
                }
            }
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
