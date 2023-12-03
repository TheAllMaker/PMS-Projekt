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
        public int _instanceId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? PatientNumber { get; set; }
        public int? PatientMonitor { get; set; }
        public int? RespirationRate { get; set; }
        public int? Temperature { get; set; }
        public int? _heartRate;
        public int? OxygenLevel { get; set; }
        public int? BloodPressureDiastolic { get; set; }
        public int? BloodPressureSystolic { get; set; }
        public int PatientScore { get; set; }
        public int EWSScore { get; set; }



        public int InstanceId
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

        public int? HeartRate
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
