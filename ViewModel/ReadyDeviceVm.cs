using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeviceExamine.ViewModel
{
    public class ReadyDeviceVm
    {
        public string TestOn { get; set; }
        public string TestBy { get; set; }
        public string Imei { get; set; }
        public string GpsSerial { get; set; }
        public string GsmSignal { get; set; }
        public string GpsSignal { get; set; }
        public string GpsVoltage1 { get; set; }
        public string MainVoltage { get; set; }
        public string MainRegulatorVoltage { get; set; }
        public string MainCurrentVoltage { get; set; }
        public string RelayOutput { get; set; }
    }
}