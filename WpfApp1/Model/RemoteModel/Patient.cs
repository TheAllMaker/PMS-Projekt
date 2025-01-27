﻿using System;
using System.ComponentModel;
using System.Windows.Threading;

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
        private object _uUID;
        private object _alive;

        private bool _isHeartRateOutOfRange;
        private bool _isRespirationRateOutOfRange;
        private bool _isOxygenLevelOutOfRange;
        private bool _isBloodPressureDiastolicOutOfRange;
        private bool _isBloodPressureSystolicOutOfRange;
        private bool _isTemperatureOutOfRange;

        private bool _isBlinking;

        private DispatcherTimer updateTimer;

        public DispatcherTimer UpdateTimer
        {
            get { return updateTimer; }
            set
            {
                if (updateTimer != value)
                {
                    updateTimer = value;
                    OnPropertyChanged(nameof(UpdateTimer));
                }
            }
        }

        public bool IsHeartRateOutOfRange
        {
            get { return _isHeartRateOutOfRange; }
            set
            {
                if (_isHeartRateOutOfRange != value)
                {
                    _isHeartRateOutOfRange = value;
                    OnPropertyChanged(nameof(IsHeartRateOutOfRange));
                }
            }
        }

        public bool IsRespirationRateOutOfRange
        {
            get { return _isRespirationRateOutOfRange; }
            set
            {
                if (_isRespirationRateOutOfRange != value)
                {
                    _isRespirationRateOutOfRange = value;
                    OnPropertyChanged(nameof(IsRespirationRateOutOfRange));
                }
            }
        }

        public bool IsOxygenLevelOutOfRange
        {
            get { return _isOxygenLevelOutOfRange; }
            set
            {
                if (_isOxygenLevelOutOfRange != value)
                {
                    _isOxygenLevelOutOfRange = value;
                    OnPropertyChanged(nameof(IsOxygenLevelOutOfRange));
                }
            }
        }

        public bool IsBloodPressureDiastolicOutOfRange
        {
            get { return _isBloodPressureDiastolicOutOfRange; }
            set
            {
                if (_isBloodPressureDiastolicOutOfRange != value)
                {
                    _isBloodPressureDiastolicOutOfRange = value;
                    OnPropertyChanged(nameof(IsBloodPressureDiastolicOutOfRange));
                }
            }
        }

        public bool IsBloodPressureSystolicOutOfRange
        {
            get { return _isBloodPressureSystolicOutOfRange; }
            set
            {
                if (_isBloodPressureSystolicOutOfRange != value)
                {
                    _isBloodPressureSystolicOutOfRange = value;
                    OnPropertyChanged(nameof(IsBloodPressureSystolicOutOfRange));
                }
            }
        }

        public bool IsTemperatureOutOfRange
        {
            get { return _isTemperatureOutOfRange; }
            set
            {
                if (_isTemperatureOutOfRange != value)
                {
                    _isTemperatureOutOfRange = value;
                    OnPropertyChanged(nameof(IsTemperatureOutOfRange));
                }
            }
        }

        public bool IsBlinking
        {
            get { return _isBlinking; }
            set
            {
                if (_isBlinking != value)
                {
                    _isBlinking = value;
                    OnPropertyChanged(nameof(IsBlinking));
                }
            }
        }

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

        public object UUID
        {
            get
            {
                return _uUID;
            }

            set
            {
                if (_uUID != value)
                {
                    _uUID = value;
                    OnPropertyChanged(nameof(UUID));
                }
            }
        }

        public object Alive
        {
            get
            {
                return _alive;
            }

            set
            {
                if (_alive != value)
                {
                    _alive = value;
                    OnPropertyChanged(nameof(Alive));
                }
            }
        }

    }
    
}
