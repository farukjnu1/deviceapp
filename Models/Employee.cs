using DeviceExamine.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceExamine.Models
{
    public class Employee
    {
        [Key]
        [MaxLength(20, ErrorMessage="Max char of username 20")]
        public string Username { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Max char of password 100")]
        public string Password { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Max char of first name 30")]
        public string FirstName { get; set; }

        [MaxLength(30, ErrorMessage = "Max char of last name 30")]
        public string LastName { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Max char of phone 20")]
        public string Phone { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Max char of email 100")]
        public string Email { get; set; }

        [MaxLength(10, ErrorMessage = "Max char of privilege 10")]
        public string Privilege { get; set; }

        public bool IsActive { get; set; }

    }
}
