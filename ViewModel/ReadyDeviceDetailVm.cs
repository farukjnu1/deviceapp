using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeviceExamine.ViewModel
{
    public class ReadyDeviceDetailVm:BaseVm
    {
        public string TestOn { get; set; }
        public string TestBy { get; set; }
        public string Imei { get; set; }
        public string GpsSerial { get; set; }
        public string GsmSignal { get; set; }
        public string GpsSignal { get; set; }
        public string Sos { get; set; }
        public string EngineStatus { get; set; }
        public string AcStatus { get; set; }
        public string GpsVoltage1 { get; set; }
        public string GpsVoltage2 { get; set; }
        public string GpsVoltage3 { get; set; }
        public string GpsVoltage4 { get; set; }
        public string MainVoltage { get; set; }
        public string MainRegulatorVoltage { get; set; }
        public string TestWithoutBattery { get; set; }
        public string TestOnlyBattery { get; set; }
        public string MainCurrentVoltage { get; set; }
        public string RelayOutput { get; set; }
        public string Camera { get; set; }
        public string EbomTest { get; set; }
        public string Comment { get; set; }
    }
}