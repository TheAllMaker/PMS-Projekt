using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediTrack.Model.RemoteModel
{
    public class Patient : INotifyPropertyChanged
    {
        public object _instanceId { get; set; }
        public object FirstName { get; set; }
        public object LastName { get; set; }
        public object PatientNumber { get; set; }
        public object PatientMonitor { get; set; }
        public object RespirationRate { get; set; }
        public object Temperature { get; set; }
        public object _heartRate;
        public object OxygenLevel { get; set; }
        public object BloodPressureDiastolic { get; set; }
        public object BloodPressureSystolic { get; set; }
        public object PatientScore { get; set; }
        public object EWSScore { get; set; }



        public object InstanceId
        {
            get { return _instanceId; }
            set
            {
                if (_instanceId != value)
                {
                    _instanceId = value;
                    OnPropertyChanged(nameof(InstanceId));
                }
            }
        }

        public object HeartRate
        {
            get { return _heartRate; }
            set
            {
                if (_heartRate != value)
                {
                    _heartRate = value;
                    OnPropertyChanged(nameof(HeartRate));
                }
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    
}
