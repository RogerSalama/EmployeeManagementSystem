using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Entities
{
    public class Account
    {
        [Key]
        public int AccountID { get; set; }

        public int EmployeeID { get; set; }
        [ForeignKey("EmployeeID")]
        public Employee Employee { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime LastLoginTime { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
    }
}
