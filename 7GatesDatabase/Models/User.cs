using System;
using System.Collections.Generic;

namespace The7GATESArchive.Models
{
    public class User
    {
        public Guid ID { get; set; }
        public string Username { get; set; }
        public int Keys { get; set; }
        public TimeSpan TimeForAllGates { get; set; }
        public int Rank { get; set; }
        public float? Percentile { get; set; }
        public string PrizeStatus { get; set; }
        public int Insight1 { get; set; }
        public int Insight2 { get; set; }
        public int Insight3 { get; set; }

        public virtual ICollection<UserGate> UserGates { get; set; }
    }
}