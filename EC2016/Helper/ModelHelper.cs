using Bll;
using Bll.Model;
using EC2016.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EC2016.Helper
{
    public class ModelHelper
    {

        public static TeamModel CreateTeamModel(Team t)
        {
            TeamModel model = new TeamModel();
            model.Id = t.Id;
            model.Name = t.Name;
            model.TournamentId = t.TournamentId;
            model.Group = t.Group;

            return model;
        }

        public static MatchModel CreateMatchModel(Match m)
        {
            MatchModel model = new MatchModel();
            model.Id = m.Id;
            model.Type = m.Type;
            model.TournamentId = m.TournamentId;
            model.HomeTeamId = m.HomeTeamId;
            model.HomeScore = m.HomeScore;
            model.AwayTeamId = m.AwayTeamId;
            model.AwayScore = m.AwayScore;
            model.Date = m.Date;

            return model;
        }

        public static TournamentModel CreateTournamentModel(Tournament t)
        {
            TournamentModel model = new TournamentModel();
            model.Id = t.Id;
            model.Name = t.Name;
            model.Place = t.Place;
            model.StartDate = t.StartDate;
            model.EndDate = t.EndDate;

            return model;
        }

        //public static UserModel CreateUserModel(User u)
        //{
        //    UserModel model = new UserModel();
        //    model.Id = u.Id;
        //    model.Name = u.Name;
        //    model.PasswordHash = u.PasswordHash;

        //    return model;
        //}

        public static GuessGameModel CreateGuessGameModel(GuessGame g)
        {
            GuessGameModel model = new GuessGameModel();
            model.Id = g.Id;
            model.Name = g.Name;
            model.PasswordHash = g.PasswordHash;
            model.TournamentId = g.TournamentId;

            return model;
        }

        public static GuessModel CreateGuessModel(Guess g)
        {
            GuessModel model = new GuessModel();
            model.Id = g.Id;
            model.MatchId = g.MatchId;
            model.UserId = g.UserId;
            model.HomeScore = g.HomeScore;
            model.AwayScore = g.AwayScore;
            
            return model;
        }

        public static List<TeamModel> CreateGorupTable(List<TeamModel> teams, List<MatchModel> matches)
        {
            foreach (var team in teams)
            {
                List<MatchModel> matchesForTeam = matches.Where(m => m.HomeTeamId == team.Id || m.AwayTeamId == team.Id).ToList();

                foreach (var match in matchesForTeam)
                {
                    if (match.HomeScore.HasValue)
                    {
                        if (match.HomeTeamId == team.Id)
                        {
                            team.Scored += match.HomeScore.Value;
                            team.Get += match.AwayScore.Value;
                            if (match.HomeScore > match.AwayScore)
                            {
                                team.Poins += 3;
                            }
                        }
                        else
                        {
                            team.Scored += match.AwayScore.Value;
                            team.Get += match.HomeScore.Value;
                            if (match.AwayScore > match.HomeScore)
                            {
                                team.Poins += 3;
                            }
                        }
                        if (match.HomeScore == match.AwayScore)
                        {
                            team.Poins += 1;
                        }
                        team.Count++;
                    }
                }
            }

            teams = teams.OrderByDescending(t => t.Poins).ThenByDescending(t => t.Scored - t.Get).ThenByDescending(t => t.Scored).ToList();

            int i = 1;
            foreach (var t in teams)
            {
                t.Position = i++;
            }

            return teams;
        }

    }
}