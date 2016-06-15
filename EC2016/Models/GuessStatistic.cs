using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EC2016.Models
{
    public class GuessStatistic
    {
        public GuessStatistic()
        {
            Stats = new List<StatHelper>();
        }

        public string Type { get; set; }

        public string UserName { get; set; }

        public List<StatHelper> Stats { get; set; }

        public class StatHelper
        {

            public string homeTeam { get; set; }

            public string awayTeam { get; set; }

            public string homeScore { get; set; }

            public string awayScore { get; set; }

            public string homeGuess { get; set; }

            public string awayGuess { get; set; }

        }

    }
}