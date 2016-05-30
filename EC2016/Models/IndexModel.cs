using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EC2016.Models
{
    public class IndexModel
    {

        public List<TeamModel> ATeams { get; set; }
        public List<TeamModel> BTeams { get; set; }
        public List<TeamModel> CTeams { get; set; }
        public List<TeamModel> DTeams { get; set; }
        public List<TeamModel> ETeams { get; set; }
        public List<TeamModel> FTeams { get; set; }

        public List<MatchModel> Matches { get; set; }

        public List<GuessModel> Guesses { get; set; }

        public List<TeamModel> Teams
        {
            get
            {
                List<TeamModel> teams = new List<TeamModel>();
                teams.AddRange(ATeams);
                teams.AddRange(BTeams);
                teams.AddRange(CTeams);
                teams.AddRange(DTeams);
                teams.AddRange(ETeams);
                teams.AddRange(FTeams);
                return teams;
            }
        }

        public List<UserGuesses> UsersWithGuesses { get; set; }

        public class UserGuesses
        {
            public UserModel User { get; set; }

            public IEnumerable<GuessModel> Guesses { get; set; }


        }

    }
}