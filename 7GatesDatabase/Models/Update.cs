using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace The7GATESArchive.Models
{
    public class Update
    { 
        [Key]
        public DateTime TimeStamp { get; set; }
        public int Gate { get; set; }
        public string AdditionalInfo { get; set; }

    }
}