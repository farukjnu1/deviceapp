using DeviceExamine.DAL;
using DeviceExamine.Models;
using DeviceExamine.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceExamine.BLL
{
    public class DeviceBLL
    {
        public List<Device> GetDevices()
        {
            DeviceDbDAL deviceDal = new DeviceDbDAL();
            return deviceDal.Devices.ToList();
        }

        public List<Device> GetReadyDevices()
        {
            DeviceDbDAL deviceDal = new DeviceDbDAL();
            List<Device> listDevices = (from o in deviceDal.Devices orderby o.TestOn descending where o.IsInstalled == false select o).ToList();
            return listDevices;
        }

        public int AddDevice(Device d) 
        {
            DeviceDbDAL deviceD = new DeviceDbDAL();
            deviceD.Devices.Add(d);
            return deviceD.SaveChanges();
        }

        public bool IsImeiNumExist(string imei) 
        {
            DeviceDbDAL deviceDal = new DeviceDbDAL();
            Device d = (from o in deviceDal.Devices where o.Imei == imei select o).FirstOrDefault();
            if (d == null)
            {
                return false;
            }
            return true;
        }

        public ReadyDeviceDetailVm GetReadyDeviceByImei(string imei)
        {
            DeviceDbDAL deviceD = new DeviceDbDAL();
            Device d = (from o in deviceD.Devices where o.Imei == imei select o).FirstOrDefault();
            ReadyDeviceDetailVm rddVm = new ReadyDeviceDetailVm();
            if (d == null)
            {
                return rddVm;
            }
            rddVm.TestOn = d.TestOn.ToString("yyyy-MMM-dd hh:mm");
            rddVm.TestBy = d.TestBy;
            rddVm.Imei = d.Imei;
            rddVm.GpsSerial = d.GpsSerial;
            rddVm.GsmSignal = d.GsmSignal.ToString();
            rddVm.GpsSignal = d.GpsSignal.ToString();
            string sos = "not tested";
            if (Convert.ToBoolean(d.Sos))
            {
                sos = "ok";
            }
            rddVm.Sos = sos;
            string engine = "not tested";
            if (Convert.ToBoolean(d.EngState))
            {
                engine = "ok";
            }
            rddVm.EngineStatus = engine;
            string ac = "not tested";
            if (Convert.ToBoolean(d.AcState))
            {
                ac = "ok";
            }
            rddVm.AcStatus = ac;
            rddVm.GpsVoltage1 = d.GpsVoltage1;
            rddVm.GpsVoltage2 = d.GpsVoltage2;
            rddVm.GpsVoltage3 = d.GpsVoltage3;
            rddVm.GpsVoltage4 = d.GpsVoltage4;
            rddVm.MainVoltage = d.MainVoltage;
            rddVm.MainRegulatorVoltage = d.MainRegulatorVoltage;
            rddVm.TestWithoutBattery = d.TestWithoutBattery;
            rddVm.TestOnlyBattery = d.TestOnlyBattery;
            rddVm.MainCurrentVoltage = d.MainCurrentVoltage.ToString();
            rddVm.RelayOutput = d.RelayOutput;
            rddVm.Camera = d.Camera;
            rddVm.EbomTest = d.EbomTest;
            rddVm.Comment = d.Note;
            return rddVm;
        }

        public ReadyDeviceDetailVm GetProceedToInstallDeviceById(string imei)
        {
            DeviceDbDAL deviceD = new DeviceDbDAL();
            Device d = (from o in deviceD.Devices where o.Imei == imei select o).FirstOrDefault();
            ReadyDeviceDetailVm rddVm = new ReadyDeviceDetailVm();
            if (d == null)
            {
                return rddVm;
            }
            rddVm.Imei = d.Imei;
            rddVm.Camera = d.Camera;
            rddVm.TestOn = d.TestOn.ToString("yyyy-MMM-dd hh:mm");
            rddVm.TestBy = d.TestBy;
            rddVm.Comment = d.Note;
            return rddVm;
        }

        public int InstallADevice(string Imei, string TerminalSim, string CarNo, string Technician, string InstallTime)
        {
            DeviceDbDAL deviceDal = new DeviceDbDAL();
            Device d = (from o in deviceDal.Devices where o.Imei == Imei select o).FirstOrDefault();
            if (d != null)
            {
                d.IsInstalled = true;
                d.TerminalSim = TerminalSim;
                d.CarNo = CarNo;
                d.Technician = Technician;
                d.InstallTime = Convert.ToDateTime(InstallTime);
                return deviceDal.SaveChanges();
            }
            return 0;
        }

        public List<Device> GetInstallDevices()
        {
            DeviceDbDAL deviceDal = new DeviceDbDAL();
            List<Device> listDevices = (from o in deviceDal.Devices orderby o.TestOn descending where o.IsInstalled == true select o).ToList();
            return listDevices;
        }

        public InstallDeviceDetailVm GetInstallDevicesByImei(string imei)
        {
            DeviceDbDAL deviceD = new DeviceDbDAL();
            Device d = (from o in deviceD.Devices where o.Imei == imei select o).FirstOrDefault();
            InstallDeviceDetailVm iddVm = new InstallDeviceDetailVm();
            if (d == null)
            {
                return iddVm;
            }
            iddVm.TestOn = d.TestOn.ToString("yyyy-MMM-dd hh:mm");
            iddVm.TestBy = d.TestBy;
            iddVm.Imei = d.Imei;
            iddVm.GpsSerial = d.GpsSerial;
            iddVm.GsmSignal = d.GsmSignal.ToString();
            iddVm.GpsSignal = d.GpsSignal.ToString();
            string sos = "not tested";
            if (Convert.ToBoolean(d.Sos))
            {
                sos = "ok";
            }
            iddVm.Sos = sos;
            string engine = "not tested";
            if (Convert.ToBoolean(d.EngState))
            {
                engine = "ok";
            }
            iddVm.EngState = engine;
            string ac = "not tested";
            if (Convert.ToBoolean(d.AcState))
            {
                ac = "ok";
            }
            iddVm.AcState = ac;
            iddVm.GpsVoltage1 = d.GpsVoltage1;
            iddVm.GpsVoltage2 = d.GpsVoltage2;
            iddVm.GpsVoltage3 = d.GpsVoltage3;
            iddVm.GpsVoltage4 = d.GpsVoltage4;
            iddVm.MainVoltage = d.MainVoltage;
            iddVm.MainRegulatorVoltage = d.MainRegulatorVoltage;
            iddVm.TestWithoutBattery = d.TestWithoutBattery;
            iddVm.TestOnlyBattery = d.TestOnlyBattery;
            iddVm.MainCurrentVoltage = d.MainCurrentVoltage.ToString();
            iddVm.RelayOutput = d.RelayOutput;
            iddVm.Camera = d.Camera;
            iddVm.EbomTest = d.EbomTest;
            iddVm.Note = d.Note;
            return iddVm;
        }
    }
}
