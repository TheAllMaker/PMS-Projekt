using System.ComponentModel;

namespace MediTrack.Model.RemoteModel
{
    public class Patient : INotifyPropertyChanged
    {

        // Patient is the main data structure class 
        // for all further remote window operations

        private object _instanceId;
        private object _firstName;
        private object _lastName;
        private object _patientNumber;
        private object _patientMonitor; 
        private object _respirationRate;
        private object _temperature;
        private object _heartRate;
        private object _oxygenLevel;
        private object _bloodPressureDiastolic;
        private object _bloodPressureSystolic;
        private object _roomNumber;
        private object _bedNumber;

        // INotifyPropertyChanged is a .NET interface and notfiy subscribers ( the Patient member access in mainwindow.cs)
        // when properties are changed. _classvariable declares a private field -> if the customer get&set are 
        // accessed we are invoking the function OnPropertyChanged which reports the changes to them 
        // -> which results in updates of our RW patient windows.

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        // from now: Custom Get/Set functions

        public object InstanceId
        {
            get
            { 
                return _instanceId;
            }

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
            get
            { 
                return _heartRate;
            }

            set
            {
                if (_heartRate != value)
                {
                    _heartRate = value;
                    OnPropertyChanged(nameof(HeartRate));
                }
            }
        }

        public object RespirationRate
        {
            get
            { 
                return _respirationRate;
            }

            set
            {
                if (_respirationRate != value)
                {
                    _respirationRate = value;
                    OnPropertyChanged(nameof(RespirationRate));
                }
            }
        }

        public object OxygenLevel
        {
            get 
            {
                return _oxygenLevel; 
            }

            set
            {
                if (_oxygenLevel != value)
                {
                    _oxygenLevel = value;
                    OnPropertyChanged(nameof(OxygenLevel));
                }
            }
        }

        public object Temperature
        {
            get 
            { 
                return _temperature;
            }

            set
            {
                if (_temperature != value)
                {
                    _temperature = value;
                    OnPropertyChanged(nameof(Temperature));
                }
            }
        }

        public object BloodPressureDiastolic
        {
            get 
            { 
                return _bloodPressureDiastolic;
            }

            set
            {
                if (_bloodPressureDiastolic != value)
                {
                    _bloodPressureDiastolic = value;
                    OnPropertyChanged(nameof(BloodPressureDiastolic));
                }
            }
        }

        public object BloodPressureSystolic
        {
            get
            { 
                return _bloodPressureSystolic;
            }

            set
            {
                if (_bloodPressureSystolic != value)
                {
                    _bloodPressureSystolic = value;
                    OnPropertyChanged(nameof(BloodPressureSystolic));
                }
            }
        }

        public object FirstName
        {
            get 
            { 
                return _firstName;
            }

            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    OnPropertyChanged(nameof(FirstName));
                }
            }
        }

        public object LastName
        {
            get 
            { 
                return _lastName; 
            }

            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged(nameof(LastName));
                }
            }
        }

        public object PatientNumber
        {
            get 
            {
                return _patientNumber;
            }

            set
            {
                if (_patientNumber != value)
                {
                    _patientNumber = value;
                    OnPropertyChanged(nameof(PatientNumber));
                }
            }
        }

        public object PatientMonitor
        {
            get 
            { 
                return _patientMonitor; 
            }

            set
            {
                if (_patientMonitor != value)
                {
                    _patientMonitor = value;
                    OnPropertyChanged(nameof(PatientMonitor));
                }
            }
        }

        public object RoomNumber
        {
            get
            {
                return _roomNumber;
            }

            set
            {
                if (_roomNumber != value)
                {
                    _roomNumber = value;
                    OnPropertyChanged(nameof(RoomNumber));
                }
            }
        }

        public object BedNumber
        {
            get
            {
                return _bedNumber;
            }

            set
            {
                if (_bedNumber != value)
                {
                    _bedNumber = value;
                    OnPropertyChanged(nameof(BedNumber));
                }
            }
        }

    }
    
}
