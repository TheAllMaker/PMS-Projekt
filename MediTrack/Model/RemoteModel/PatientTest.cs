using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace MediTrack.Model.RemoteModel
{
    public class PatientTest
    {
    public void TestPatientCall1()
        {


            Patient TestPatient1 = new Patient()
            {
                PatientNameI = "Max",
                PatientNameII = "MusterMann",
                PatientNumber = 1,
                PatientMonitor = 1,
                PatientHeartRate = 20,
                PatientOxygen = 20,
                PatientPressure = 20,
                PatientRespirationRate = 20,
                PatientScore = 3,
                PatientTemperature = 20,

            };

            Application.Current.Resources["TestPatient1"] = TestPatient1;
        }

      public void TestPatientCall2()
        {


            Patient TestPatient2 = new Patient ()
            {
                PatientNameI = "TestMax",
                PatientNameII = "TextMusterMann",
                PatientNumber = 2,
                PatientMonitor = 2,
                PatientHeartRate = 20,
                PatientOxygen = 20,
                PatientPressure = 20,
                PatientRespirationRate = 20,
                PatientScore = 3,
                PatientTemperature = 20,

            };

            Application.Current.Resources["TestPatient2"] = TestPatient2;
        }
    }
}
