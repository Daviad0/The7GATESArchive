using System;
using System.Collections.Generic;

namespace The7GATESArchive.Models
{
    public class User
    {
        public Guid ID { get; set; }
        public string Username { get; set; }
        public int Keys { get; set; }


        public virtual ICollection<UserGate> UserGates { get; set; }
    }
}