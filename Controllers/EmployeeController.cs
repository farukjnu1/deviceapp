using DeviceExamine.BLL;
using DeviceExamine.Filters;
using DeviceExamine.Models;
using DeviceExamine.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeviceExamine.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        // Developed by
        // Faruk Abdullah
        // Cell: +8801920334664
        // Email: farukjnu1@gmail.com

        // Date: 15-Apr-2016

        [HeaderFilter]
        public ActionResult Index()
        {
            ReadyDeviceListVm rdListVm = new ReadyDeviceListVm();

            DeviceBLL deviceB = new DeviceBLL();
            List<Device> devices = new List<Device>();
            devices = deviceB.GetReadyDevices();

            List<ReadyDeviceVm> listReadyDeviceVm = new List<ReadyDeviceVm>();

            foreach (Device d in devices)
            {
                ReadyDeviceVm rdVm = new ReadyDeviceVm();
                rdVm.TestOn = d.TestOn.ToString("yyyy-MMM-dd hh:mm");
                rdVm.TestBy = d.TestBy;
                rdVm.Imei = d.Imei;
                rdVm.GpsSerial = d.GpsSerial;
                rdVm.GsmSignal = d.GsmSignal.ToString();
                rdVm.GpsSignal = d.GpsSignal.ToString();
                rdVm.GpsVoltage1 = d.GpsVoltage1;
                rdVm.MainVoltage = d.MainVoltage;
                rdVm.MainRegulatorVoltage = d.MainRegulatorVoltage;
                rdVm.MainCurrentVoltage = d.MainCurrentVoltage.ToString();
                rdVm.RelayOutput = d.RelayOutput;
                listReadyDeviceVm.Add(rdVm);
            }
            rdListVm.ReadyDevices = listReadyDeviceVm;
            return View("Index", rdListVm);
        }

        [HeaderFilter]
        public ActionResult DeviceOne()
        {
            return View(new BaseVm());
        }

        [HeaderFilter]
        public ActionResult DeviceOnePost(string Imei, string GpsSerial)
        {
            DeviceBLL deviceB = new DeviceBLL();
            if (string.IsNullOrEmpty(Imei))
            {
                ModelState.AddModelError("CredentialError", "IMEI number is required");
                return View("DeviceOne", new BaseVm());
            }
            else if(Imei.Length > 10)
            {
                ModelState.AddModelError("CredentialError", "IMEI number should not not be more than 10 letters");
                return View("DeviceOne", new BaseVm());
            }
            else if (deviceB.IsImeiNumExist(Imei))
            {
                ModelState.AddModelError("CredentialError", "IMEI number already exist, try with new one");
                return View("DeviceOne", new BaseVm());
            }
            else if (string.IsNullOrEmpty(GpsSerial))
            {
                ModelState.AddModelError("CredentialError", "GPS Serial number is required");
                return View("DeviceOne", new BaseVm());
            }
            else if (GpsSerial.Length > 10)
            {
                ModelState.AddModelError("CredentialError", "GPS Serial should not not be more than 10 letters");
                return View("DeviceOne", new BaseVm());
            }
            else
            {
                Session["device"] = Imei + "," + GpsSerial;
                return RedirectToAction("DeviceTwo");
            }
        }

        [HeaderFilter]
        public ActionResult DeviceTwo()
        {
            if (Session["device"] == null)
            {
                return RedirectToAction("DeviceOne", "Employee");
            }
            else if (!Session["device"].ToString().Contains(','))
            {
                return RedirectToAction("DeviceOne", "Employee");
            }
            else if (Session["device"].ToString().Split(',').Length > 2)
            {
                return RedirectToAction("DeviceOne", "Employee");
            }
            else
            {
                return View(new BaseVm());
            }
        }

        [HeaderFilter]
        public ActionResult DeviceTwoPost(string GpsSignal, string GsmSignal, string Sos)
        {
            int nGpsSignal = 0;
            int nGsmSignal = 0;

            if (string.IsNullOrEmpty(GpsSignal))
            {
                ModelState.AddModelError("CredentialError", "GPS signal is required");
                return View("DeviceTwo", new BaseVm());
            }
            else if(!Int32.TryParse(GpsSignal, out nGpsSignal)) 
            {
                ModelState.AddModelError("CredentialError", "GPS signal is not valid");
                return View("DeviceTwo", new BaseVm());
            }
            else if (nGpsSignal < 6)
            {
                ModelState.AddModelError("CredentialError", "GPS signal should be more than 5");
                return View("DeviceTwo", new BaseVm());
            }
            else if (string.IsNullOrEmpty(GsmSignal))
            {
                ModelState.AddModelError("CredentialError", "GSM signal is required");
                return View("DeviceTwo", new BaseVm());
            }
            else if (!Int32.TryParse(GsmSignal, out nGsmSignal))
            {
                ModelState.AddModelError("CredentialError", "GSM signal is not valid");
                return View("DeviceTwo", new BaseVm());
            }
            else if (nGsmSignal < 6)
            {
                ModelState.AddModelError("CredentialError", "GSM signal should be more than 20");
                return View("DeviceTwo", new BaseVm());
            }
            else if (Sos != "on")
            {
                ModelState.AddModelError("CredentialError", "SOS test is must!");
                return View("DeviceTwo", new BaseVm());
            }
            Session["device"] += "," + GpsSignal + "," + GsmSignal + "," + true;
            return RedirectToAction("DeviceThree");
        }

        [HeaderFilter]
        public ActionResult DeviceThree()
        {
            if (Session["device"] == null)
            {
                return RedirectToAction("DeviceOne", "Employee");
            }
            else if (!Session["device"].ToString().Contains(','))
            {
                return RedirectToAction("DeviceOne", "Employee");
            }
            return View(new BaseVm());
        }

        [HeaderFilter]
        public ActionResult DeviceThreePost(string EngineStatus, string AcStatus)
        {
            if (EngineStatus != "on")
            {
                ModelState.AddModelError("CredentialError", "Engine Status check is must!");
                return View("DeviceThree", new BaseVm());
            }
            else if (AcStatus != "on")
            {
                ModelState.AddModelError("CredentialError", "Engine status check is must!");
                return View("DeviceThree", new BaseVm());
            }
            else
            {
                Session["device"] += "," + true + "," + true;
                return RedirectToAction("DeviceFour");
            }
        }

        [HeaderFilter]
        public ActionResult DeviceFour()
        {
            if (Session["device"] == null)
            {
                return RedirectToAction("DeviceOne", "Employee");
            }
            else if (!Session["device"].ToString().Contains(','))
            {
                return RedirectToAction("DeviceOne", "Employee");
            }
            return View(new BaseVm());
        }

        [HeaderFilter]
        public ActionResult DeviceFourPost(string GpsVoltage1, string GpsVoltage2, string GpsVoltage3, string GpsVoltage4)
        {
            float fGpsV1 = 0;
            float fGpsV2 = 0;
            float fGpsV3 = 0;
            float fGpsV4 = 0;
            if (string.IsNullOrEmpty(GpsVoltage1))
            {
                ModelState.AddModelError("CredentialError", "GPS Voltage 1 is required");
                return View("DeviceFour", new BaseVm());
            }
            else if(!float.TryParse(GpsVoltage1, out fGpsV1))
            {
                ModelState.AddModelError("CredentialError", "GPS Voltage 1 is not valid");
                return View("DeviceFour", new BaseVm());
            }
            else if (fGpsV1 <3.2)
            {
                ModelState.AddModelError("CredentialError", "GPS Voltage 1 should be more than 3.1");
                return View("DeviceFour", new BaseVm());
            }
            else if (string.IsNullOrEmpty(GpsVoltage2))
            {
                ModelState.AddModelError("CredentialError", "GPS Voltage 2 is required");
                return View("DeviceFour", new BaseVm());
            }
            else if (!float.TryParse(GpsVoltage2, out fGpsV2))
            {
                ModelState.AddModelError("CredentialError", "GPS Voltage 2 is not valid");
                return View("DeviceFour", new BaseVm());
            }
            else if (fGpsV2 < 3.2)
            {
                ModelState.AddModelError("CredentialError", "GPS Voltage 2 should be more than 3.1");
                return View("DeviceFour", new BaseVm());
            }
            else if (string.IsNullOrEmpty(GpsVoltage3))
            {
                ModelState.AddModelError("CredentialError", "GPS Voltage 3 is required");
                return View("DeviceFour", new BaseVm());
            }
            else if (!float.TryParse(GpsVoltage3, out fGpsV3))
            {
                ModelState.AddModelError("CredentialError", "GPS Voltage 3 is not valid");
                return View("DeviceFour", new BaseVm());
            }
            else if (fGpsV3 < 3.2)
            {
                ModelState.AddModelError("CredentialError", "GPS Voltage 3 should be more than 3.1");
                return View("DeviceFour", new BaseVm());
            }
            else if (string.IsNullOrEmpty(GpsVoltage4))
            {
                ModelState.AddModelError("CredentialError", "GPS Voltage 4 is required");
                return View("DeviceFour", new BaseVm());
            }
            else if (!float.TryParse(GpsVoltage4, out fGpsV4))
            {
                ModelState.AddModelError("CredentialError", "GPS Voltage 4 is not valid");
                return View("DeviceFour", new BaseVm());
            }
            else if (fGpsV3 < 2.6)
            {
                ModelState.AddModelError("CredentialError", "GPS Voltage 4 should be more than 2.5");
                return View("DeviceFour", new BaseVm());
            }
            Session["device"] +=","+ GpsVoltage1 + "," + GpsVoltage2 + "," + GpsVoltage3 + "," + GpsVoltage4;
            return RedirectToAction("DeviceFive");
        }

        [HeaderFilter]
        public ActionResult DeviceFive()
        {
            if (Session["device"] == null)
            {
                return RedirectToAction("DeviceOne", "Employee");
            }
            else if (!Session["device"].ToString().Contains(','))
            {
                return RedirectToAction("DeviceOne", "Employee");
            }
            return View(new BaseVm());
        }

        [HeaderFilter]
        public ActionResult DeviceFivePost(string MainVoltage, string MainRegulatorVoltage)
        {
            float fMrv = 0;
            if (string.IsNullOrEmpty(MainVoltage))
            {
                ModelState.AddModelError("CredentialError", "Main Voltage is required");
                return View("DeviceFive", new BaseVm());
            }
            else if (string.IsNullOrEmpty(MainRegulatorVoltage))
            {
                ModelState.AddModelError("CredentialError", "Main Regulator Voltage is required");
                return View("DeviceFive", new BaseVm());
            }
            else if (!float.TryParse(MainRegulatorVoltage, out fMrv))
            {
                ModelState.AddModelError("CredentialError", "Main Regulator Voltage should be a number");
                return View("DeviceFive", new BaseVm());
            }
            else if (fMrv < 4.8)
            {
                ModelState.AddModelError("CredentialError", "Main Regulator Voltage should be more than 4.8");
                return View("DeviceFive", new BaseVm());
            }
            Session["device"] += "," + MainVoltage + "," + MainRegulatorVoltage;
            return RedirectToAction("DeviceSix");
        }

        [HeaderFilter]
        public ActionResult DeviceSix()
        {
            if (Session["device"] == null)
            {
                return RedirectToAction("DeviceOne", "Employee");
            }
            else if (!Session["device"].ToString().Contains(','))
            {
                return RedirectToAction("DeviceOne", "Employee");
            }
            return View(new BaseVm());
        }

        [HeaderFilter]
        public ActionResult DeviceSixPost(string TestWithoutBattery, string TestOnlyBattery, string MainCurrentVoltage)
        {
            int nMcv = 0;
            if (string.IsNullOrEmpty(TestWithoutBattery))
            {
                ModelState.AddModelError("CredentialError", "Test Without Battery is required");
                return View("DeviceSix", new BaseVm());
            }
            else if (string.IsNullOrEmpty(TestOnlyBattery))
            {
                ModelState.AddModelError("CredentialError", "Test Only Battery is required");
                return View("DeviceSix", new BaseVm());
            }
            else if (!Int32.TryParse(MainCurrentVoltage, out nMcv))
            {
                ModelState.AddModelError("CredentialError", "Main Current Voltage should be a number");
                return View("DeviceSix", new BaseVm());
            }
            else if (nMcv < 81)
            {
                ModelState.AddModelError("CredentialError", "Main Current Voltage should be more than 80");
                return View("DeviceSix", new BaseVm());
            }
            Session["device"] += "," + TestWithoutBattery + "," + TestOnlyBattery + "," + MainCurrentVoltage;
            return RedirectToAction("DeviceSeven");
        }

        [HeaderFilter]
        public ActionResult DeviceSeven()
        {
            if (Session["device"] == null)
            {
                return RedirectToAction("DeviceOne", "Employee");
            }
            else if (!Session["device"].ToString().Contains(','))
            {
                return RedirectToAction("DeviceOne", "Employee");
            }
            return View(new BaseVm());
        }

        [HeaderFilter]
        public ActionResult DeviceSevenPost(string RelayOutput, string Camera, string EbomTest, string Note)
        {
            if (string.IsNullOrEmpty(RelayOutput))
            {
                ModelState.AddModelError("CredentialError", "RelayOutput test is required");
                return View("DeviceSeven", new BaseVm());
            }
            else if (string.IsNullOrEmpty(Camera))
            {
                ModelState.AddModelError("CredentialError", "Camera test is required");
                return View("DeviceSeven", new BaseVm());
            }
            else if (string.IsNullOrEmpty(EbomTest))
            {
                ModelState.AddModelError("CredentialError", "Ebom test is required");
                return View("DeviceSeven", new BaseVm());
            }
            else if (Note.Length > 50)
            {
                ModelState.AddModelError("CredentialError", "Comment is note more than 50 letters");
                return View("DeviceSeven", new BaseVm());
            }
            Session["device"] += "," + RelayOutput + "," + Camera + "," + EbomTest + "," + Note;
            return RedirectToAction("DeviceSubmit");
        }

        [HeaderFilter]
        public ActionResult DeviceSubmit()
        {
            if (Session["device"] == null)
            {
                return RedirectToAction("DeviceOne", "Employee");
            }
            else if (!Session["device"].ToString().Contains(','))
            {
                return RedirectToAction("DeviceOne", "Employee");
            }
            return View(new BaseVm());
        }

        [HeaderFilter]
        public ActionResult DeviceSubmitPost(string btnSubmit)
        {
            if (btnSubmit != "btnSubmit")
            {
                return RedirectToAction("DeviceOne", "Employee");
            }
            else
            {
                if (Session["device"] == null)
                {
                    return RedirectToAction("DeviceOne", "Employee");
                }
                else if (!Session["device"].ToString().Contains(','))
                {
                    return RedirectToAction("DeviceOne", "Employee");
                }
                else
                {
                    int nEffected = 0;
                    DeviceBLL deviceB = new DeviceBLL();
                    var deviceModules = Session["device"].ToString().Trim().Split(',');
                    Device d = new Device();
                    d.Imei = deviceModules[0];
                    d.GpsSerial = deviceModules[1];
                    d.GsmSignal = Convert.ToInt32(deviceModules[3]);
                    d.GpsSignal = Convert.ToInt32(deviceModules[2]);
                    d.Sos = Convert.ToBoolean(deviceModules[4]);
                    d.EngState = Convert.ToBoolean(deviceModules[5]);
                    d.AcState = Convert.ToBoolean(deviceModules[6]);
                    d.GpsVoltage1 = deviceModules[7];
                    d.GpsVoltage2 = deviceModules[8];
                    d.GpsVoltage3 = deviceModules[9];
                    d.GpsVoltage4 = deviceModules[10];
                    d.MainVoltage = deviceModules[11];
                    d.MainRegulatorVoltage = deviceModules[12];
                    d.TestWithoutBattery = deviceModules[13];
                    d.TestOnlyBattery = deviceModules[14];
                    d.MainCurrentVoltage = Convert.ToInt32(deviceModules[15]);
                    d.RelayOutput = deviceModules[16];
                    d.Camera = deviceModules[17];
                    d.EbomTest = deviceModules[18];
                    d.TestOn = DateTime.Now;
                    d.TestBy = System.Web.HttpContext.Current.User.Identity.Name;
                    d.Note = deviceModules[19];
                    d.IsInstalled = false;
                    d.TerminalSim = string.Empty;
                    d.CarNo = string.Empty;
                    d.Technician = string.Empty;
                    d.InstallTime = DateTime.Now;
                    if (ModelState.IsValid)
                    {
                        nEffected = deviceB.AddDevice(d);
                    }
                    if (nEffected < 1)
                    {
                        return RedirectToAction("DeviceOne", "Employee");
                    }
                    return RedirectToAction("Index", "Employee");
                }
            }
        }

        [HeaderFilter]
        public ActionResult DeviceDetail(string Imei)
        {
            ReadyDeviceDetailVm rddVm = new ReadyDeviceDetailVm();
            DeviceBLL deviceB = new DeviceBLL();
            rddVm = deviceB.GetReadyDeviceByImei(Imei);
            return View(rddVm);
        }

        public ActionResult DeviceDetailPost(string Imei)
        {
            var command = Imei.Trim().Split('+');
            if (command[1] == "l")
            {
                return RedirectToAction("Index","Employee");
            }
            else if (command[1] == "i")
            {
                DeviceBLL deviceB = new DeviceBLL();
                ReadyDeviceDetailVm rddVm = new ReadyDeviceDetailVm();
                rddVm = deviceB.GetProceedToInstallDeviceById(command[0]);
                return View("InstallDevice", rddVm);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HeaderFilter]
        public ActionResult InstallDevice()
        {
            return View(new ReadyDeviceDetailVm());
        }

        [HeaderFilter]
        public ActionResult InstallDevicePost(string Imei, string TerminalSim, string CarNo, string Technician, string InstallTime)
        {
            DateTime dt;
            if (string.IsNullOrEmpty(TerminalSim))
            {
                ModelState.AddModelError("CredentialError", "Terminal SIM is required");
                return RedirectToAction("InstallDevice");
            }
            else if (TerminalSim.Length > 23)
            {
                ModelState.AddModelError("CredentialError", "Terminal SIM is not more than 23 letter");
                return RedirectToAction("InstallDevice");
            }
            else if (string.IsNullOrEmpty(CarNo))
            {
                ModelState.AddModelError("CredentialError", "Car Number  is required");
                return RedirectToAction("InstallDevice");
            }
            else if (CarNo.Length > 23)
            {
                ModelState.AddModelError("CredentialError", "Car Number is not more than 23 letter");
                return RedirectToAction("InstallDevice");
            }
            else if (string.IsNullOrEmpty(Technician))
            {
                ModelState.AddModelError("CredentialError", "Technician  is required");
                return RedirectToAction("InstallDevice");
            }
            else if (Technician.Length > 50)
            {
                ModelState.AddModelError("CredentialError", "Technician is not more than 50 letter");
                return RedirectToAction("InstallDevice");
            }
            else if (string.IsNullOrEmpty(InstallTime))
            {
                ModelState.AddModelError("CredentialError", "Install date is required");
                return RedirectToAction("InstallDevice");
            }
            else if (!DateTime.TryParse(InstallTime, out dt))
            {
                ModelState.AddModelError("CredentialError", "Install date is not valid");
                return RedirectToAction("InstallDevice");
            }
            DeviceBLL deviceB = new DeviceBLL();
            deviceB.InstallADevice(Imei, TerminalSim, CarNo, Technician, InstallTime);
            return RedirectToAction("InstalledDeviceList");
        }

        [HeaderFilter]
        public ActionResult InstalledDeviceList(int? page)
        {
            InstallDeviceListVm idListVm = new InstallDeviceListVm();

            DeviceBLL deviceB = new DeviceBLL();
            List<Device> devices = new List<Device>();
            devices = deviceB.GetInstallDevices();

            List<InstallDeviceVm> listInstallDeviceVm = new List<InstallDeviceVm>();

            foreach (Device d in devices)
            {
                InstallDeviceVm idVm = new InstallDeviceVm();
                idVm.Imei = d.Imei;
                idVm.Camera = d.Camera;
                idVm.TestOn = d.TestOn.ToString("yyyy-MMM-dd hh:mm");
                idVm.TestBy = d.TestBy;

                idVm.Comment = d.Note;
                idVm.TerminalSim = d.TerminalSim;
                idVm.CarNo = d.CarNo;
                idVm.Technician = d.Technician;
                idVm.InstallDate = d.InstallTime.ToString("yyyy-MMM-dd hh:mm");
                listInstallDeviceVm.Add(idVm);
            }
            idListVm.InstallDevices = listInstallDeviceVm;

            // pagination code
            var pager = new Pager(idListVm.InstallDevices.Count(), page);
            var vm = new InstallDeviceListVm
            {
                InstallDevices = idListVm.InstallDevices.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                Pager = pager
            };

            return View("InstalledDeviceList", vm);
        }

        [HeaderFilter]
        public ActionResult InstallDeviceDetail(string Imei)
        {
            InstallDeviceDetailVm rddVm = new InstallDeviceDetailVm();
            DeviceBLL deviceB = new DeviceBLL();
            rddVm = deviceB.GetInstallDevicesByImei(Imei);
            return View(rddVm);
        }
    }
}