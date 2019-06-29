using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace okLims.Models
{
    public class Request
    {
        public int RequestId { get; set; }
     
        [Required]
        public string MethodName { get; set; }

        public string RequesterEmail { get; set; }


        [Required]
        public string LaboratoryName { get; set; }
        [Required]
        public string Start { get; set; }
        [Required]
        public string End { get; set; }
        public List<RequestLine> RequestLines { get; set; } = new List<RequestLine>();
    }
}
