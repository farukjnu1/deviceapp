using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeviceExamine.ViewModel
{
    public class InstallDeviceVm : BaseVm
    {
        public string Imei { get; set; }
        public string Camera { get; set; }
        public string TestOn { get; set; }
        public string TestBy { get; set; }
        public string Comment { get; set; }
        public string TerminalSim { get; set; }
        public string CarNo { get; set; }
        public string Technician { get; set; }
        public string InstallDate { get; set; }
    }
}