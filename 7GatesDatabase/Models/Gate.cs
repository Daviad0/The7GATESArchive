using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace The7GATESArchive.Models
{
    public class Gate
    { 
        public int GateID { get; set; }
        public string Theme { get; set; }
        public int Keys { get; set; }

        public virtual ICollection<UserGate> UserGates { get; set; }
    }
}