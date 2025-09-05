using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Entities
{
    public class BonusType
    {
        [Key]
        public int BonusTypeID { get; set; }
        public string Category { get; set; }
        public string BonusName { get; set; }
    }
}
