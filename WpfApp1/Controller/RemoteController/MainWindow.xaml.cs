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

    //PatientTest patientTest = new PatientTest();
    //patientTest.TestPatientCall1();
    //patientTest.TestPatientCall2();

    //ContentControl contentControl = new ContentControl
    //{
    //    ContentTemplate = (DataTemplate)Resources["PatientTemplate"],
    //    Content = Application.Current.Resources["TestPatient2"],
    //    Margin = new Thickness(5)
    //};



    public partial class MainWindow : Window
    {
        private CancellationTokenSource _cancellationTokenSource;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += InitializeComponents;
            PatientTest.TestPatientCall2();
            _cancellationTokenSource = new CancellationTokenSource();
            Loaded += async (sender, args) => await ProcessMQTTMessages(_cancellationTokenSource.Token);
        }

        private void InitializeComponents(object sender, RoutedEventArgs e)
        {
            ConnectToMQTTBroker();
        }

        private static void ConnectToMQTTBroker()
        {
            MqttHandler handler = new MqttHandler();
            handler.ConnectToServer();
            handler.SubScribeToTopic();
            Console.WriteLine(StringContainer.HandlerIntializer);
        }

        // auskommentiert weil Code noch nicht fertig von Selcuk 

        private async Task ProcessMQTTMessages(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(100);

                object[] mqttMessageQueueArray = MqttMessageQueue.Dequeue();
                // wenn Queue mit Inhalt sowie der Eintrag besteht sowie
                if ((mqttMessageQueueArray.Length != 0) && (PatientDictionary.DictionaryContainer(mqttMessageQueueArray[0])) && (mqttMessageQueueArray[8] is int value && value == 0))
                {
                    UuidDictionary.DictionaryRemover(mqttMessageQueueArray[0]);

                    Dispatcher.Invoke(() =>
                    {
                        //PatientenMonitorDynGrid.Children.Remove(PatientTemplateContentAddition);
                    });
                    PatientDictionary.DictionaryRemover(mqttMessageQueueArray[0]);
                }
                // wenn Queue mit Inhalt sowie Patient gefunden date patient up
                else if ((mqttMessageQueueArray.Length != 0) && PatientDictionary.DictionaryContainer(mqttMessageQueueArray[0]))
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

                else if ((mqttMessageQueueArray.Length != 0))
                {

                    object mqttDataString = DataBaseRemoteConnection.CallMonitorIDtoPatientID(mqttMessageQueueArray[0]);
                    object[] patientDataString = DataBaseRemoteConnection.CallForPatientThroughID(mqttDataString);
                    try
                    {
                        Patient PatientenInstanz = new Patient()
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


                        Dispatcher.Invoke(() =>
                        {
                            ContentControl PatientTemplateContentAddition = new ContentControl
                            {
                                ContentTemplate = (DataTemplate)Resources["PatientTemplate"],
                                Content = PatientenInstanz,
                                Margin = new Thickness(5)
                            };
                            PatientenMonitorDynGrid.Children.Add(PatientTemplateContentAddition);
                        });

                        PatientDictionary.DictionaryInput(mqttMessageQueueArray[0], PatientenInstanz);
                        UuidDictionary.DictionaryInput(mqttMessageQueueArray[0], mqttMessageQueueArray[7]);
                    }
                    catch
                    {

                    }
                    }
            }
        }






        //private void Select_Button_Clicked(object sender, RoutedEventArgs e)
        //{
        //    // Instanziert ein neues Fenster
        //    Window SelectWindow = new SelectionWindow
        //    {
        //        Title = "Select Patient", // Name des neuen Fenster's
        //        Width = SystemParameters.PrimaryScreenWidth * 0.75,
        //        Height = SystemParameters.PrimaryScreenHeight * 0.75,
        //        WindowStartupLocation = WindowStartupLocation.CenterScreen
        //    };
        //    SelectWindow.Show();
        //    SelectWindow.Owner = this;
        //    //SelectWindow.ShowDialog();


        //}




        //void AssignPatientData(Patient patient, object[] data)
        //{
        //    patient.HeartRate = data[1];
        //    patient.RespirationRate = data[2];
        //}

        //private void Add_Button_Clicked(object sender, RoutedEventArgs e)
        //{
        //    Window AddNewPatient = new AddPatientWindow
        //    {
        //        Title = "Add a new Patient",
        //        Width = SystemParameters.PrimaryScreenWidth * 0.75,
        //        Height = SystemParameters.PrimaryScreenHeight * 0.75,
        //        WindowStartupLocation = WindowStartupLocation.CenterScreen
        //    };
        //    AddNewPatient.Show();
        //    AddNewPatient.Owner = this;

        //}

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

        //private void PatientNetworkIcon_Click(object sender, RoutedEventArgs e)
        //{
        //    MyPopup.IsOpen = !MyPopup.IsOpen; // Schaltet die Sichtbarkeit des Popups um
        //}


        private void ShowOptions_Click(object sender, RoutedEventArgs e)
        {
            OptionsPopup.IsOpen = true;
        }

        //private void OptionsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    //if (OptionsListBox.SelectedItem is ListBoxItem selectedOption)
        //    //{
        //    //    MessageBox.Show($"Sie haben '{selectedOption.Content}' ausgewählt.");
        //    //}

        //    //// Schließen Sie das Popup nach der Auswahl
        //    //OptionsPopup.IsOpen = false;
        //    if (OptionsListBox.SelectedItem != null)
        //    {
        //        string selectedOption = OptionsListBox.SelectedItem.ToString();

        //        // Erstellen eines neuen Buttons
        //        ContentControl newButton = new ContentControl
        //        {
        //            ContentTemplate = (DataTemplate)Resources["CrossButton"],
        //            // Weitere Eigenschaften des Buttons können hier festgelegt werden
        //        };

        //        // Fügen Sie eine Click-Ereignishandler-Methode für den neuen Button hinzu, falls erforderlich
        //        //newButton.Click += NewButton_Click;

        //        // Platzieren des Buttons im Grid
        //        PatientenMonitorDynGrid.Children.Add(newButton);

        //        // Optional: Setzen von Grid.Row und Grid.Column, wenn Sie ein mehrspaltiges/mehrreihiges Grid haben
        //        // Grid.SetRow(newButton, rowIndex);
        //        // Grid.SetColumn(newButton, columnIndex);

        //        // Zurücksetzen der Auswahl, falls gewünscht
        //        OptionsListBox.SelectedItem = null;
        //    }
        //}

        private void OptionsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OptionsListBox.SelectedItem != null)
            {
                // Erstellen eines neuen ContentControls für das nächste UniformGrid
                ContentControl newContentControlForGrid = new ContentControl
                {
                    ContentTemplate = (DataTemplate)FindResource("CrossButton"),
                    
                    Margin = new Thickness(5)
                };

                // Hinzufügen des neuen ContentControls zum nächsten UniformGrid
                PatientenMonitorDynGrid.Children.Add(newContentControlForGrid);

                // Aktualisieren des ContentTemplates des ausgewählten CrossButtons
                //if (PatientNe9tworkIcon.Content is ContentControl currentContentControl)
                //{
                //    currentContentControl.ContentTemplate = (DataTemplate)FindResource("PatientTemplate");
                //    //currentContentControl.Content = Application.Current.Resources["TestPatient2"] /* Hier das entsprechende Content-Objekt setzen */;
                //    //    ContentTemplate = (DataTemplate)Resources["PatientTemplate"],
                //    //    Content = Application.Current.Resources["TestPatient2"],
                //}
                ContentControl newContent = new ContentControl();
                newContent.ContentTemplate = this.Resources["PatientTemplate"] as DataTemplate;
                newContent.Content = Application.Current.Resources["TestPatient2"]; // Set the content you want to display

             PatientNe9tworkIcon.Content = newContent;

                // Close the popup if necessary
                OptionsPopup.IsOpen = false;
                // Zurücksetzen der Auswahl im Popup und Schließen des Popups
                OptionsListBox.SelectedItem = null;
                OptionsPopup.IsOpen = false;
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