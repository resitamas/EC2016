using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EC2016.Models
{
    public class MatchGuessModel
    {

        public MatchGuessModel()
        {
            GuessList = new List<List<string>>();
            for (int i = 0; i < 9; i++)
            {
                GuessList.Add(new List<string>());
            }
        }

        public List<List<string>> GuessList { get; set; }

        public string HomeTeamName { get; set; }

        public string AwayTeamName { get; set; }

        public string HomeScore { get; set; }

        public string AwayScore { get; set; }

    }
}