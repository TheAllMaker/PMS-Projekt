//using MediTrack.Model.DataBaseModelConnection;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Windows.Controls;
//using System.Windows;
//using System.Threading.Tasks;

//namespace MediTrack.Model.RemoteModel
//{
//    public class BackGroundLogicManager
//    {



   






//        public static Patient mainLogic()
//        {


//            while(true)
//            {
//                Thread.Sleep(4000);
//                int?[] queuearray = MqttMessageQueue.Dequeue();

//                if (queuearray == null)
//                {
//                    Console.WriteLine("still null");
//                    continue;
//                }
               

//                if (PatientDictionary.DictionaryContainer(queuearray[0]))
//                {
//                    Console.WriteLine("Selected MonitorID found.");
                    
//                }

//                else
//                {
//                   int? puffer  = DataBaseRemoteConnection.CallMonitorIDtoPatientID( queuearray[0]);
//                   string[] sstring = DataBaseRemoteConnection.CallForPatientThroughID(puffer);
//                   PatientDictionary.DictionaryInput(queuearray[0],puffer);
//                    Console.WriteLine("MOID" + queuearray[0]);
//                    Console.WriteLine("PID" + puffer);
//                    Console.WriteLine(sstring[0]);
//                    Console.WriteLine(sstring[1]);




//                    Patient PatientenInstanz = new Patient()
//                    {
//                        FirstName = sstring[0],
//                        LastName = sstring[1],
//                        PatientNumber = puffer,
//                        PatientMonitor = queuearray[0],
//                        HeartRate = queuearray[1],
//                        OxygenLevel = queuearray[3],
//                        BloodPressureDiastolic = queuearray[5],
//                        RespirationRate = queuearray[2],
//                        BloodPressureSystolic = queuearray[4],
//                        PatientScore = 1,
//                        Temperature = queuearray[6],
//                    };
//                    return PatientenInstanz;

//                }

//            }





//        }


 






//    }
//}
