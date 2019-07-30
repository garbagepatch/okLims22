using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkflowEngineTest;
using OptimaJet.Workflow.Core.Model;

namespace okLims.Models
{
    public class DesignerModel
    {
        public Guid? processId { get; set; }
        public string SchemeName { get; set; }
        public string Scheme { get; set; }
    }
}
