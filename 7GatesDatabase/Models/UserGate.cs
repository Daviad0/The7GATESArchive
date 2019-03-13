using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace The7GATESArchive.Models
{

    public class UserGate
    {
        [Key]
        [Column(Order = 1)]
        public int GateID { get; set; }
        [Key]
        [Column(Order = 2)]
        public Guid UserID { get; set; }
        public TimeSpan Time { get; set; }
        public int Rank { get; set; }
        public int Keys { get; set; }
        public TimeSpan CollectiveTime { get; set; }
        public float? Percentile { get; set; }
        public bool Finished { get; set; }


        public virtual Gate Gate { get; set; }
        public virtual User User { get; set; }
        //add migration for Rank
    }
}