
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace okLims.Models
{
   
   

    public class RequestModel
    {
   
      
        public Guid RequestId { get; set; }
        public int? Number { get; set; }
        public int ControllerID { get; set; }
        public int SizeID { get; set; }
        public int FilterID { get; set; }
        public string State { get; set; }
        [Required]
        [StringLength(256)]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [ForeignKey("SizeID")]
        public FilterSize FilterSize { get; set; }
        [ForeignKey("FilterID")]
        public FilterType FilterType { get; set; }
  [ForeignKey("ControllerID")]
        public ControllerType ControllerType {get; set;}
        public int LaboratoryId { get; set; }
        
        public string SpecialDetails { get; set; }
        public string RequesterEmail { get; set; }
        [ForeignKey("LaboratoryId")]
        public Laboratory Laboratory { get; set; }

        public Guid AuthorId { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Author")]
        public string AuthorName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Manager")]
        public Guid? ManagerId { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Manager")]
        public string ManagerName { get; set; }


        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

        public List<RequestLine> RequestLines { get; set; } = new List<RequestLine>();
        [NotMapped]
        public Dictionary<string, string> AvailiableStates { get; set; }
        public RequestCommandModel[] Commands { get; set; }

 

        public RequestModel()
        {
            Commands = new RequestCommandModel[0];
            AvailiableStates = new Dictionary<string, string> { };
            HistoryModel = new RequestHistoryModel();
        }

        public string StateToSet { get; set; }

        public RequestHistoryModel HistoryModel { get; set; }
    }
    public class RequestCommandModel
    {
        public string key { get; set; }
        public string value { get; set; }
        public OptimaJet.Workflow.Core.Model.TransitionClassifier Classifier { get; set; }
    }
}

