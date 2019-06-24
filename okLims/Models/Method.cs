using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace okLims.Models
{
   public class Method
    {
        public int MethodId { get; set; }
        [Required]
        public string MethodName { get; set; }
        [Required]
        public bool? Validated { get; set; }
        public List<MethodLine> MethodLines = new List<MethodLine>();
    }
}
