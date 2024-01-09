using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediTrack.Model.RemoteModel
{
    public class Threshold
    {
        private int monitorID;
        private int heartRateMin;
        private int heartRateMax;

        public Threshold(int MonitorID, int HeartRateMin, int HeartRateMax)
        {
            monitorID = MonitorID;
            heartRateMin = HeartRateMin;
            heartRateMax = HeartRateMax;
        }
    }
}
