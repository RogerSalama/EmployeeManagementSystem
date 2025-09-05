using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Entities
{
    public class Role
    {
        [Key]
        public String RoleLevel { get; set; }

        public string Title { get; set; }
   
    }
}
