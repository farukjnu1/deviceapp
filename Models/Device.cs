using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceExamine.Models
{
    public class Device
    {
        [Key]
        [MaxLength(10, ErrorMessage="Max char of IMEI 10")]
        public string Imei { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "Max char of GPS Serial 10")]
        public string GpsSerial { get; set; }

        [Required]
        public int GsmSignal { get; set; }

        [Required]
        public int GpsSignal { get; set; }

        [Required]
        public bool Sos { get; set; }

        [Required]
        public bool EngState { get; set; }

        [Required]
        public bool AcState { get; set; }

        [Required]
        [MaxLength(5, ErrorMessage="GPS Voltage 1 is out of rang")]
        public string GpsVoltage1 { get; set; }

        [Required]
        [MaxLength(5, ErrorMessage = "GPS Voltage 2 is out of rang")]
        public string GpsVoltage2 { get; set; }

        [Required]
        [MaxLength(5, ErrorMessage = "GPS Voltage 3 is out of rang")]
        public string GpsVoltage3 { get; set; }

        [Required]
        [MaxLength(5, ErrorMessage = "GPS Voltage 4 is out of rang")]
        public string GpsVoltage4 { get; set; }

        [Required]
        [MaxLength(5, ErrorMessage="Invalid main voltage")]
        public string MainVoltage { get; set; }

        [Required]
        [MaxLength(5, ErrorMessage = "Main Regulator Voltage is out of rang")]
        public string MainRegulatorVoltage { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "Max char of test without battery 10")]
        public string TestWithoutBattery { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "Max char of test only battery 10")]
        public string TestOnlyBattery { get; set; }

        [Required]
        public int MainCurrentVoltage { get; set; }

        [MaxLength(10, ErrorMessage = "Max char of relay output 10")]
        [Required]
        public string RelayOutput { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "Max char of camera 10")]
        public string Camera { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "Max char of ebom test 10")]
        public string EbomTest { get; set; }

        [Required]
        public DateTime TestOn { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Max char of tester name 20")]
        public string TestBy { get; set; }

        [MaxLength(50, ErrorMessage = "Max char of note 50")]
        public string Note { get; set; }

        [Required]
        public bool IsInstalled { get; set; }

        [MaxLength(23, ErrorMessage = "Max char of terminal SIM 23")]
        public string TerminalSim { get; set; }

        [MaxLength(23, ErrorMessage = "Max char of car number 23")]
        public string CarNo { get; set; }

        [MaxLength(50, ErrorMessage = "Max char of technician 50")]
        public string Technician { get; set; }

        public DateTime InstallTime { get; set; }
    }
}
