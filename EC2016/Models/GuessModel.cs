using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EC2016.Models
{
    public class GuessModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int MatchId { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }

    }
}