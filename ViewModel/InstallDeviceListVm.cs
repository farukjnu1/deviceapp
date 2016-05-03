using DeviceExamine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeviceExamine.ViewModel
{
    public class InstallDeviceListVm:BaseVm
    {
        public IEnumerable<InstallDeviceVm> InstallDevices { get; set; }
        public Pager Pager { get; set; }
    }
}