using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mssql
{
    [Table("EmployeeRole")]
    public class EmployeeRole
    {
       
       
        public Guid EmployeeId { get; set; }
        public Guid RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }

    }
}
