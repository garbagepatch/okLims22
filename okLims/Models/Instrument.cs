﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace okLims.Models
{
    public class Instrument
    {

        public int InstrumentId { get; set; }
        [Required]
        public string InstrumentName { get; set; }
        public DateTimeOffset CalibrationDue { get; set; }
        public DateTimeOffset CalibrationDate { get; set;}
        public int CalibrationLength { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public int MaintenanceInterval { get; set; }

    }
}
