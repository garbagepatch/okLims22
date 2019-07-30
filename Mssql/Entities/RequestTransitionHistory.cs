namespace Mssql
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("RequestTransitionHistory")]
    public partial class RequestTransitionHistory
    {
        public Guid Id { get; set; }

        public Guid RequestId { get; set; }

        public Guid? EmployeeId { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string AllowedToEmployeeNames { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? TransitionTime { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Order { get; set; }

        [Required]
        [StringLength(1024)]
        public string InitialState { get; set; }

        [Required]
        [StringLength(1024)]
        public string DestinationState { get; set; }

        [Required]
        [StringLength(1024)]
        public string Command { get; set; }

        public virtual Request Request { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
