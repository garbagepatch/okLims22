using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace okLims.Models
{
    public class RequestLine
    {
        public int RequestLineId { get; set; }
        public Request Request { get; set; }
        public int RequestId { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string DateCompleted { get; set; }
    }
}
