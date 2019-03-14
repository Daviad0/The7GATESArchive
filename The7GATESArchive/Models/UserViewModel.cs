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
        public int Insight1;
        public int Insight2;
        public int Insight3;
        public bool GateError; 

        public ICollection<UserGate> UserGates { get; set; }
    }
}