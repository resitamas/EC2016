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
            model.IsConfirmed = m.isConfirmed;

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

        public static UserModel CreateUserModel(ApplicationUser u)
        {
            UserModel model = new UserModel();
            model.Id = u.Id;
            model.Name = u.UserName;
            model.PasswordHash = u.PasswordHash;

            return model;
        }

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
                                team.Points += 3;
                                team.Win++;
                            }
                        }
                        else
                        {
                            team.Scored += match.AwayScore.Value;
                            team.Get += match.HomeScore.Value;
                            if (match.AwayScore > match.HomeScore)
                            {
                                team.Points += 3;
                                team.Win++;
                            }
                        }
                        if (match.HomeScore == match.AwayScore)
                        {
                            team.Points += 1;
                            team.Draw++;
                        }
                        team.Count++;
                    }
                }
            }

            teams = teams.OrderByDescending(t => t.Points).ThenByDescending(t => t.Scored - t.Get).ThenByDescending(t => t.Scored).ToList();

            int i = 1;
            foreach (var t in teams)
            {
                t.Loose = t.Count - (t.Draw + t.Win);
                t.Position = i++;
            }

            return teams;
        }

        public class Wrapper
        {
            public PlayerStatModel PlayerStatModel { get; set; }

            public PlayerMatchesWithGuesses PlayerMatchesWithGuesses { get; set; }
        }

        public static Wrapper GetPlayerStatsFromGuesses(IEnumerable<GuessModel> guesses, List<MatchModel> matches, bool isMax = false)
        {
            Wrapper wrapper = new Wrapper();

            PlayerStatModel playerStats = new PlayerStatModel();
            playerStats.Count = guesses.Count();
            playerStats.Max = isMax;

            PlayerMatchesWithGuesses playerMatchesWithGuesses = new PlayerMatchesWithGuesses();

            foreach (var guess in guesses)
            {
                MatchModel match = matches.Where(m => m.Id == guess.MatchId).First();

                if (match.HomeScore.HasValue) //game has been playing
                {
                    if (match.HomeScore.Value == guess.HomeScore && match.AwayScore.Value == guess.AwayScore)
                    {
                        playerStats.TT++;
                        //playerStats.GK++;
                        //playerStats.MK++;
                        //playerStats.CSG++;
                        //playerStats.OG++;
                        playerStats.TTPoint += GuessPoint.TTPoint;
                        playerMatchesWithGuesses.TTGuessesByMatch.Add(match.Id, guess.Id);
                    }
                    else
                    {
                        bool isGK = false;
                        if (Math.Abs(match.HomeScore.Value - match.AwayScore.Value) == Math.Abs(guess.HomeScore - guess.AwayScore) &&
                            ((match.HomeScore.Value > match.AwayScore.Value && guess.HomeScore > guess.AwayScore)
                            || (match.HomeScore.Value <= match.AwayScore.Value && guess.HomeScore <= guess.AwayScore)))
                        {
                            //playerStats.GK++;
                            isGK = true;
                        }

                        bool isMK = false;
                        if ((match.HomeScore.Value > match.AwayScore.Value && guess.HomeScore > guess.AwayScore) ||
                            (match.HomeScore.Value < match.AwayScore.Value && guess.HomeScore < guess.AwayScore) ||
                            (match.HomeScore.Value == match.AwayScore.Value && guess.HomeScore == guess.AwayScore))
                        {
                            //playerStats.MK++;
                            isMK = true;
                        }

                        bool isCSG = false;
                        if (match.HomeScore.Value == guess.HomeScore || match.AwayScore.Value == guess.AwayScore)
                        {
                            //playerStats.CSG++;
                            isCSG = true;
                            //if (isMK)
                            //{
                            //    playerStats.Bonus += 2;
                            //}
                        }

                        bool isOG = false;
                        if (match.HomeScore.Value + match.AwayScore.Value == guess.HomeScore + guess.AwayScore)
                        {
                            //playerStats.OG++;
                            isOG = true;
                        }

                        if (isMK && isCSG)
                        {
                            playerStats.MKCSGPoint += GuessPoint.MKCSGPoint;
                            playerStats.MKCSG++;
                            playerMatchesWithGuesses.MKCSGGuessesByMatch.Add(guess.Id, match.Id);

                        }
                        else
                        {
                            if (isMK && isGK)
                            {
                                if (match.HomeScore.Value == match.AwayScore.Value)
                                {
                                    playerStats.MKGKPoint += GuessPoint.DrawMKPoint;
                                    playerStats.MKGKDraw++;
                                    playerMatchesWithGuesses.MKGKDRAWGuessesByMatch.Add(guess.Id, match.Id);

                                }
                                else
                                {
                                    playerStats.MKGKPoint += GuessPoint.MKGKPoint;
                                    playerStats.MKGK++;
                                    playerMatchesWithGuesses.MKGKGuessesByMatch.Add(guess.Id, match.Id);
                                }
                            }
                            else
                            {
                                if (isMK && isOG)
                                {
                                    playerStats.MKOGPoint += GuessPoint.MKOGPoint;
                                    playerStats.MKOG++;
                                    playerMatchesWithGuesses.MKOGGuessesByMatch.Add(guess.Id, match.Id);
                                }
                                else
                                {
                                    if (isMK)
                                    {
                                        playerStats.MKPoint += GuessPoint.MKPoint;
                                        playerStats.MK++;
                                        playerMatchesWithGuesses.MKGuessesByMatch.Add(guess.Id, match.Id);
                                    }
                                    else
                                    {
                                        if (isCSG)
                                        {
                                            playerStats.CSGPoint += GuessPoint.CSGPoint;
                                            playerStats.CSG++;
                                            playerMatchesWithGuesses.CSGGuessesByMatch.Add(guess.Id, match.Id);

                                        }
                                        else
                                        {
                                            if (isOG)
                                            {
                                                playerStats.OGPoint += GuessPoint.OGPoint;
                                                playerStats.OG++;
                                                playerMatchesWithGuesses.OGGuessesByMatch.Add(guess.Id, match.Id);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }


            }

            wrapper.PlayerStatModel = playerStats;
            wrapper.PlayerMatchesWithGuesses = playerMatchesWithGuesses;

            return wrapper;
            //return playerStats;

        }

    }
}