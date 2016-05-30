using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EC2016.Models
{
    public class TeamModel
    {
        public int Id { get; set; }
        public int TournamentId { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }

        public int Count { get; set; }


        public int Position { get; set; }

        public int Scored { get; set; }

        public int Get { get; set; }

        public int Points { get; set; }

        public int Win { get; set; }

        public int Loose { get; set; }

        public int Draw { get; set; }

    }
}