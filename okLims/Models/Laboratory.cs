using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace okLims.Models
{
    public class Laboratory
    {


        public int LaboratoryId { get; set; }
        [Required]
        public string LaboratoryName { get; set; }


    }
}
