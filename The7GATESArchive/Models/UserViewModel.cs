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

        public ICollection<UserGate> UserGates { get; set; }
    }
}