using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace The7GATESArchive.Models
{
    public class UserViewModel
    {
        public string Username;
        public int Keys;
        public TimeSpan TimeForAllGates;
        public Guid ID;
        public int Rank;
        public double? Percentile;
        public string PrizeStatus;
        public int Insight1 { get; set; }
        public int Insight2 { get; set; }
        public int Insight3 { get; set; }
        public string Username2;
        public int Keys2;
        public TimeSpan TimeForAllGates2;
        public Guid ID2;
        public int Rank2;
        public double? Percentile2;
        public string PrizeStatus2;
        public int Insight12 { get; set; }
        public int Insight22 { get; set; }
        public int Insight32 { get; set; }

        public ICollection<UserGate> UserGates { get; set; }
        public ICollection<UserGate> UserGates2 { get; set; }
    }
}