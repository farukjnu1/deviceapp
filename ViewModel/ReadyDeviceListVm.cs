using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeviceExamine.ViewModel
{
    public class ReadyDeviceListVm:BaseVm
    {
        public List<ReadyDeviceVm> ReadyDevices { get; set; }
    }
}