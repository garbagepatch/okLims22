using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkflowEngineTest;

namespace okLims.Models
{
    public class DocumentListModel
    {
        public int Count { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<DocumentModel> Docs { get; set; }
    }
}