using System;

namespace The7GATESArchive.Models
{

    public class UserGate
    {
        public int UserGateID { get; set; }
        public int GateID { get; set; }
        public Guid UserID { get; set; }
        public TimeSpan Time { get; set; }
        public int Rank { get; set; }

        public virtual Gate Gate { get; set; }
        public virtual User User { get; set; }
        //add migration for Rank
    }
}