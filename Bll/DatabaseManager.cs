using Bll.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
    public class DatabaseManager
    {

        #region GET
        public static List<Team> GetAllTeam()
        {
            using (var db = new EC2016CodeFirst())
            {
                return db.Teams.ToList();
            }
        }

        public static List<Tournament> GetAllTournament()
        {
            using (var db = new EC2016CodeFirst())
            {
                return db.Tournaments.ToList();
            }
        }

        public static List<Match> GetAllMatches()
        {
            using (var db = new EC2016CodeFirst())
            {
                return db.Matches.Include("Team").Include("Team1").ToList();
            }
        }

        public static List<Match> GetMatchesByTeamId(int teamId)
        {
            using (var db = new EC2016CodeFirst())
            {
                return db.Matches.Where(m => m.HomeTeamId == teamId || m.AwayTeamId == teamId).ToList();
            }
        }

        //public static List<Guess> GetGuessesByUser(string userId)
        //{
        //    using (var db = new EC2016CodeFirst())
        //    {
        //        return 
        //    }
        //}

        public static ApplicationUser GetUserById(string id)
        {
            using (var db = new EC2016CodeFirst())
            {
                return db.Users.Include("Guesses").Where(u => u.Id == id).First();
            }
        }

        public static List<ApplicationUser> GetUsersByGuessGame(int guessGameId)
        {
            using (var db = new EC2016CodeFirst())
            {
                return db.Users.Include("Guesses").Where(u => u.GuessGames.Select(g => g.Id).Contains(guessGameId)).ToList();
            }
        }

        public static Match GetMatchById(int matchId, bool withTeams = false)
        {
            using (var db = new EC2016CodeFirst())
            {
                if (withTeams)
                {
                    return db.Matches.Include("Team").Include("Team1").Where(m => m.Id == matchId).First();
                }
                else
                {
                    return db.Matches.Where(m => m.Id == matchId).First();
                }
            }
        }
        
        public static List<Guess> GetGuessesByMatchByGuessGame(int matchId, int guessGameId)
        {
            using (var db = new EC2016CodeFirst())
            {
                List<Guess> guesses = new List<Guess>();

                var guessGameUsers = db.GuessGames.Include("Users.Guesses").Where(g => g.Id == guessGameId).Select(g => g.Users).First();
                foreach (var user in guessGameUsers)
                {
                    if (user.UserName != "Developer")
                    {
                        guesses.AddRange(user.Guesses.Where(g => g.MatchId == matchId));
                    }
                }

                return guesses;

                //return db.Guesses.Include("User").Where(g => g.MatchId == matchId).ToList();

            }
        }

        #endregion

        #region POST

        public static int AddTournament(string name, string place, DateTime startDate, DateTime endDate)
        {
            using (var db = new EC2016CodeFirst())
            {
                Tournament t = new Tournament();
                t.Name = name;
                t.Place = place;
                t.StartDate = startDate;
                t.EndDate = endDate;

                db.Tournaments.Add(t);
                db.SaveChanges();

                return t.Id;
            }
        }

        public static int AddTeam(string name, string group, int tournamentId)
        {
            using (var db = new EC2016CodeFirst())
            {
                Team t = new Team();
                t.Name = name;
                t.TournamentId = tournamentId;
                t.Group = group;
                

                db.Teams.Add(t);
                db.SaveChanges();
                return t.Id;
            }
        }

        public static int AddMatch(int homeTeamId, int awayTeamId, int tournamentId, DateTime date, string type)
        {
            using (var db = new EC2016CodeFirst())
            {
                Match m = new Match();
                m.HomeTeamId = homeTeamId;
                m.AwayTeamId = awayTeamId;
                m.TournamentId = tournamentId;
                m.HomeScore = null;
                m.AwayScore = null;
                m.Date = date;
                m.Type = type;

                db.Matches.Add(m);
                db.SaveChanges();

                return m.Id;
            }
        }

        //public static int AddUser(string name, byte[] password)
        //{
        //    using (var db = new EC2016CodeFirst())
        //    {
        //        User u = new User();
        //        u.Name = name;
        //        u.PasswordHash = password;

        //        db.Users1.Add(u);
        //        db.SaveChanges();

        //        return u.Id;
        //    }
        //}

        public static int AddGuessGame(string name, byte[] password, int tournamentId)
        {
            using (var db = new EC2016CodeFirst())
            {
                GuessGame g = new GuessGame();
                g.Name = name;
                g.PasswordHash = password;
                g.TournamentId = tournamentId;

                db.GuessGames.Add(g);
                db.SaveChanges();

                return g.Id;
            }
        }

        public static int AddGuess(int matchId, string userId, int homeScore, int awayScore)
        {
            using (var db = new EC2016CodeFirst())
            {
                Guess g = new Guess();
                g.MatchId = matchId;
                g.UserId = userId;
                g.HomeScore = homeScore;
                g.AwayScore = awayScore;

                db.Guesses.Add(g);
                db.SaveChanges();

                return g.Id;
            }
        }

        public static int AddResultForMatch(int matchId, int homeScore, int awayScore)
        {
            using (var db = new EC2016CodeFirst())
            {
                Match match = db.Matches.Where(m => m.Id == matchId).First();
                match.HomeScore = homeScore;
                match.AwayScore = awayScore;

                db.Matches.Attach(match);
                var entry = db.Entry(match);
                entry.Property(e => e.HomeScore).IsModified = true;
                entry.Property(e => e.AwayScore).IsModified = true;

                return match.Id;
            }
        }

        #endregion


        #region PUT

        public static void AddUserToGuessGame(string userId, int guessGameId)
        {
            using (var db = new EC2016CodeFirst())
            {

                ApplicationUser user =  db.Users.Where(u => u.Id == userId).First();
                GuessGame game = db.GuessGames.Where(g => g.Id == guessGameId).First();

                user.GuessGames.Add(game);

                db.SaveChanges();


            }
        }

        public static void ModifyGuess(string userId, int matchId, int homeScore, int awayScore)
        {
            using (var db = new EC2016CodeFirst())
            {
                Guess guess = db.Guesses.Where(g => g.UserId == userId && g.MatchId == matchId).First();
                guess.HomeScore = homeScore;
                guess.AwayScore = awayScore;

                db.Guesses.Attach(guess);
                var entry = db.Entry(guess);
                entry.Property(e => e.HomeScore).IsModified = true;
                entry.Property(e => e.AwayScore).IsModified = true;

                db.SaveChanges();

            }
        }

        #endregion

    }
}
