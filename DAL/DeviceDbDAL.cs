using DeviceExamine.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceExamine.DAL
{
    public class DeviceDbDAL:DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("TblEmployee");
            modelBuilder.Entity<LoginInfo>().ToTable("TblLoginTime");
            modelBuilder.Entity<Device>().ToTable("TblDevice");
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<LoginInfo> LoginInfos { get; set; }
        public DbSet<Device> Devices { get; set; }
    }
}
