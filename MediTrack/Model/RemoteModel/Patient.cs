using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediTrack.Model.RemoteModel
{
    public class Patient
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? PatientNumber { get; set; }
        public int? PatientMonitor { get; set; }
        public float RespirationRate { get; set; }
        public float Temperature { get; set; }
        public float HeartRate { get; set; }
        public float OxygenLevel { get; set; }
        public float BloodPressure { get; set; }
        public float PatientScore { get; set; }
        public float? EWSScore { get; set; }
    }
    
}
