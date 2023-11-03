using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediTrack.Model.RemoteModel
{
    public class Patient
    {
        public string? PatientNameI { get; set; }
        public string? PatientNameII { get; set; }
        public int? PatientNumber { get; set; }
        public float? PatientMonitor { get;set; }
        public float PatientRespirationRate { get; set; }
        public float PatientTemperature { get; set; }
        public float PatientHeartRate { get; set; }
        public float PatientOxygen { get; set; }
        public float PatientPressure { get; set;}
        public float PatientScore { get; set;}
    }
    
}
