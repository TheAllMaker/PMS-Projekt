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
                FirstName = "Max",
                LastName = "MusterMann",
                PatientNumber = 1,
                PatientMonitor = 1,
                HeartRate = 20,
                OxygenLevel = 20,
                BloodPressureDiastolic = 20,
                RespirationRate = 20,
                PatientScore = 3,
                Temperature = 20,

            };

            Application.Current.Resources["TestPatient1"] = TestPatient1;
        }

      public void TestPatientCall2()
        {


            Patient TestPatient2 = new Patient ()
            {
                FirstName = "TestMax",
                LastName = "TextMusterMann",
                PatientNumber = 2,
                PatientMonitor = 2,
                HeartRate = 20,
                OxygenLevel = 20,
                BloodPressureDiastolic = 20,
                RespirationRate = 20,
                PatientScore = 3,
                Temperature = 20,

            };

            Application.Current.Resources["TestPatient2"] = TestPatient2;
        }
    }
}
