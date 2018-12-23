using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace The7GATESArchive
{
    public class sevengates
    {
        public string rank;
        public Guid uuid;
        public int total_time;
        public int gates_solved;
        public int total_keys;
        public string username_raw;
        public string username_html;

    }
    public class apiresults
    {
        public int total;
        public List<sevengates> results;

    }
}