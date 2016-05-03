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
    public class EmployeeBLL
    {
        public List<Employee> GetEmployees()
        {
            DeviceDbDAL deviceDal = new DeviceDbDAL();
            return deviceDal.Employees.ToList();
        }

        public Employee TopOne()
        {
            DeviceDbDAL deviceDal = new DeviceDbDAL();
            Employee e = new Employee();
            e = (from o in deviceDal.Employees select o).First();
            return e;
        }

        public List<LoginInfo> GetLoginInfos()
        {
            DeviceDbDAL deviceDal = new DeviceDbDAL();
            return deviceDal.LoginInfos.ToList();
        }

        public UserStatus GetUserValidity(UserVm u)
        {
            string strPw = Encrypt(u.Password);
            DeviceDbDAL deviceDal = new DeviceDbDAL();
            Employee e = new Employee();
            e = (from o in deviceDal.Employees where o.Username == u.UserName && o.Password == strPw && o.IsActive == true select o).FirstOrDefault();
            if (e == null)
            {
                return UserStatus.NonAuthenticatedUser; 
            }
            else
            {
                if (e.Privilege == "1")
                {
                    return UserStatus.AuthenticatedAdmin;
                }
                else if (e.Privilege == "2")
                {
                    return UserStatus.AuthentucatedUser;
                }
                else
                {
                    return UserStatus.NonAuthenticatedUser;
                }
            }
        }

        private static string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(clearText);
            using (System.Security.Cryptography.Aes encryptor = System.Security.Cryptography.Aes.Create())
            {
                System.Security.Cryptography.Rfc2898DeriveBytes pdb = new System.Security.Cryptography.Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    using (System.Security.Cryptography.CryptoStream cs = new System.Security.Cryptography.CryptoStream(ms, encryptor.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public int AddLoginInfo(LoginInfo li)
        {
            DeviceDbDAL deviceD = new DeviceDbDAL();
            deviceD.LoginInfos.Add(li);
            return deviceD.SaveChanges();
        }
    }
}
