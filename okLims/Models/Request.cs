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
        public Method Method { get; set; }
        [Required]
        public string MethodName { get; set; }

        public string RequesterEmail { get; set; }

        public Laboratory Laboratory { get; set; }
        [Required]
        public string LaboratoryName { get; set; }
        [Required]
        public DateTimeOffset DateSubmitted { get; set; }
        [Required]
        public DateTimeOffset DueDate { get; set; }
        public List<RequestLine> RequestLines { get; set; } = new List<RequestLine>();
    }
}
