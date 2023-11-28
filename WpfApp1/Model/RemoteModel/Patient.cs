using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediTrack.Model.RemoteModel
{
    public class Patient
    {
        public int InstanceId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? PatientNumber { get; set; }
        public int? PatientMonitor { get; set; }
        public int? RespirationRate { get; set; }
        public int? Temperature { get; set; }
        public int? HeartRate { get; set; }
        public int? OxygenLevel { get; set; }
        public int? BloodPressureDiastolic { get; set; }
        public int? BloodPressureSystolic { get; set; }
        public int PatientScore { get; set; }
        public int EWSScore { get; set; }
    }
    
}
