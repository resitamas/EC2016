using Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EC2016.Models
{
    public class MatchModel
    {
        public int Id { get; set; }
        public int TournamentId { get; set; }

        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public Nullable<int> HomeScore { get; set; }
        public Nullable<int> AwayScore { get; set; }

        public bool IsConfirmed { get; set; }

        public string Round
        {
            get
            {
                switch (Type)
                {
                    case MatchType.FirstRound:
                        return "Első csoportmeccs";
                    case MatchType.SecondRound:
                        return "Második csoportmeccs";
                    case MatchType.ThirdRound:
                        return "Harmadik csoportmeccs";
                    case MatchType.SemiQuaerterFinal:
                        return "Nyolcaddöntő";
                    case MatchType.QuarterFinal:
                        return "Negyeddöntő";
                    case MatchType.SemiFinal:
                        return "Elődöntő";
                    case MatchType.Final:
                        return "Döntő";
                    default:
                        return "Ismeretlen";
                }
            }
        }

    }
}