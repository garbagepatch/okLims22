using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace okLims.Models
{
    public class RequestLine
    {
      public int Count { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        [NotMapped]
        public List<RequestModel> RequestModels { get; set; }

    }
}
