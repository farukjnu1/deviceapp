using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceExamine.Models
{
    public class LoginInfo
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(20, ErrorMessage="Max char of username 20")]
        public string Username { get; set; }

        public DateTime LoginTime { get; set; }
    }
}
