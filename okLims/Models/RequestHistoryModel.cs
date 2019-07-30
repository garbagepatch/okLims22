using Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace okLims.Models
{
    public class RequestHistoryModel
    {
        public List<RequestTransitionHistory> Items { get; set; }
    }
}
