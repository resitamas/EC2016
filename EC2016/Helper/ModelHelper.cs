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

            public PlayerStatModel PlayerStatModelBefore { get; set; }

            public PlayerMatchesWithGuesses PlayerMatchesWithGuesses { get; set; }
        }


        public static Wrapper GetPlayerStatsFromGuesses(IEnumerable<GuessModel> guesses, List<MatchModel> matches)
        {
            Wrapper wrapper = new Wrapper();

            PlayerStatModel playerStatModel = new PlayerStatModel();
            playerStatModel.Count = guesses.Count();

            PlayerStatModel playerStatModelBefore = new PlayerStatModel();
            int lastId = matches.Where(m => m.HomeScore.HasValue).OrderBy(m => m.Date).Last().Id;

            PlayerMatchesWithGuesses playerMatchesWithGuesses = new PlayerMatchesWithGuesses();

            foreach (var guess in guesses)
            {
                MatchModel match = matches.Where(m => m.Id == guess.MatchId).First();

                if (match.HomeScore.HasValue) //game has been playing
                {
                    if (match.HomeScore.Value == guess.HomeScore && match.AwayScore.Value == guess.AwayScore)
                    {
                        playerStatModel.TT++;
                        playerStatModel.TTPoint += GuessPoint.TTPoint;
                        if (match.Id != lastId)
                        {
                            playerStatModelBefore.TT++;
                            playerStatModelBefore.TTPoint += GuessPoint.TTPoint;
                        }
                        playerMatchesWithGuesses.TTGuessesByMatch.Add(guess.Id, match.Id);
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
                            playerStatModel.MKCSGPoint += GuessPoint.MKCSGPoint;
                            playerStatModel.MKCSG++;
                            if (match.Id != lastId)
                            {
                                playerStatModelBefore.MKCSG++;
                                playerStatModelBefore.MKCSGPoint += GuessPoint.MKCSGPoint;
                            }
                            playerMatchesWithGuesses.MKCSGGuessesByMatch.Add(guess.Id, match.Id);

                        }
                        else
                        {
                            if (isMK && isGK)
                            {
                                if (match.HomeScore.Value == match.AwayScore.Value)
                                {
                                    playerStatModel.MKGKPoint += GuessPoint.DrawMKPoint;
                                    playerStatModel.MKGKDraw++;
                                    if (match.Id != lastId)
                                    {
                                        playerStatModelBefore.MKGKPoint += GuessPoint.DrawMKPoint;
                                        playerStatModelBefore.MKGKDraw++;
                                    }
                                    playerMatchesWithGuesses.MKGKDRAWGuessesByMatch.Add(guess.Id, match.Id);

                                }
                                else
                                {
                                    playerStatModel.MKGKPoint += GuessPoint.MKGKPoint;
                                    playerStatModel.MKGK++;
                                    if (match.Id != lastId)
                                    {
                                        playerStatModelBefore.MKGKPoint += GuessPoint.MKGKPoint;
                                        playerStatModelBefore.MKGK++;
                                    }
                                    playerMatchesWithGuesses.MKGKGuessesByMatch.Add(guess.Id, match.Id);
                                }
                            }
                            else
                            {
                                if (isMK && isOG)
                                {
                                    playerStatModel.MKOGPoint += GuessPoint.MKOGPoint;
                                    playerStatModel.MKOG++;
                                    if (match.Id != lastId)
                                    {
                                        playerStatModelBefore.MKOGPoint += GuessPoint.MKOGPoint;
                                        playerStatModelBefore.MKOG++;
                                    }
                                    playerMatchesWithGuesses.MKOGGuessesByMatch.Add(guess.Id, match.Id);
                                }
                                else
                                {
                                    if (isMK)
                                    {
                                        playerStatModel.MKPoint += GuessPoint.MKPoint;
                                        playerStatModel.MK++;
                                        if (match.Id != lastId)
                                        {
                                            playerStatModelBefore.MKPoint += GuessPoint.MKPoint;
                                            playerStatModelBefore.MK++;
                                        }
                                        playerMatchesWithGuesses.MKGuessesByMatch.Add(guess.Id, match.Id);
                                    }
                                    else
                                    {
                                        if (isCSG)
                                        {
                                            playerStatModel.CSGPoint += GuessPoint.CSGPoint;
                                            playerStatModel.CSG++;
                                            if (match.Id != lastId)
                                            {
                                                playerStatModelBefore.CSGPoint += GuessPoint.CSGPoint;
                                                playerStatModelBefore.CSG++;
                                            }
                                            playerMatchesWithGuesses.CSGGuessesByMatch.Add(guess.Id, match.Id);

                                        }
                                        else
                                        {
                                            if (isOG)
                                            {
                                                playerStatModel.OGPoint += GuessPoint.OGPoint;
                                                playerStatModel.OG++;
                                                if (match.Id != lastId)
                                                {
                                                    playerStatModelBefore.OGPoint += GuessPoint.OGPoint;
                                                    playerStatModelBefore.OG++;
                                                }
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

            wrapper.PlayerStatModel = playerStatModel;
            wrapper.PlayerStatModelBefore = playerStatModelBefore;
            wrapper.PlayerMatchesWithGuesses = playerMatchesWithGuesses;

            return wrapper;
            //return playerStats;

        }

        public enum GuessType
        {
            TT, MKCSG, MKGK, MKGKD, MKOG, MK, CSG, OG
        }

        public static GuessType GetGuessType(Guess guess, Match match)
        {
            if (match.HomeScore.Value == guess.HomeScore && match.AwayScore.Value == guess.AwayScore)
            {
                return GuessType.TT;
            }
            else
            {
                bool isGK = false;
                if (Math.Abs(match.HomeScore.Value - match.AwayScore.Value) == Math.Abs(guess.HomeScore - guess.AwayScore) &&
                    ((match.HomeScore.Value > match.AwayScore.Value && guess.HomeScore > guess.AwayScore)
                    || (match.HomeScore.Value <= match.AwayScore.Value && guess.HomeScore <= guess.AwayScore)))
                {
                    isGK = true;
                }

                bool isMK = false;
                if ((match.HomeScore.Value > match.AwayScore.Value && guess.HomeScore > guess.AwayScore) ||
                    (match.HomeScore.Value < match.AwayScore.Value && guess.HomeScore < guess.AwayScore) ||
                    (match.HomeScore.Value == match.AwayScore.Value && guess.HomeScore == guess.AwayScore))
                {
                    isMK = true;
                }

                bool isCSG = false;
                if (match.HomeScore.Value == guess.HomeScore || match.AwayScore.Value == guess.AwayScore)
                {
                    isCSG = true;
                }

                bool isOG = false;
                if (match.HomeScore.Value + match.AwayScore.Value == guess.HomeScore + guess.AwayScore)
                {
                    isOG = true;
                }

                if (isMK && isCSG)
                {
                    return GuessType.MKCSG;
                }
                else
                {
                    if (isMK && isGK)
                    {
                        if (match.HomeScore.Value == match.AwayScore.Value)
                        {
                            return GuessType.MKGKD;
                        }
                        else
                        {
                            return GuessType.MKGK;
                        }
                    }
                    else
                    {
                        if (isMK && isOG)
                        {
                            return GuessType.MKOG;
                        }
                        else
                        {
                            if (isMK)
                            {
                                return GuessType.MK;
                            }
                            else
                            {
                                if (isCSG)
                                {
                                    return GuessType.CSG;
                                }
                                else
                                {
                                    if (isOG)
                                    {
                                        return GuessType.OG;
                                    }
                                    else
                                    {
                                        return GuessType.OG;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public static string GetShortUserName(string name)
        {
            string[] splittedName = name.Split(' ');

            return splittedName.Count() == 1 ? splittedName.ElementAt(0) : splittedName.ElementAt(0) + " " + (splittedName.ElementAt(1).Count() == 0 ? "" : splittedName.ElementAt(1).ElementAt(0) + ".");
        }

        public static MatchGuessModel CreateMatchGuessModel(List<Guess> guesses, Match match)
        {
            MatchGuessModel model = new MatchGuessModel();

            model.HomeTeamName = match.Team1.Name;
            model.AwayTeamName = match.Team.Name;
            model.HomeScore = match.HomeScore.HasValue ? match.HomeScore.Value.ToString() : "";
            model.AwayScore = match.AwayScore.HasValue ? match.AwayScore.Value.ToString() : "";

            foreach (var guess in guesses)
            {
                if (match.HomeScore.HasValue)
                {
                    switch (GetGuessType(guess, match))
                    {
                        case GuessType.TT:
                            model.GuessList.ElementAt(0).Add(GetShortUserName(guess.User.UserName) + " (" + guess.HomeScore + "-" + guess.AwayScore + ")");
                            break;
                        case GuessType.MKCSG:
                            model.GuessList.ElementAt(1).Add(GetShortUserName(guess.User.UserName) + " (" + guess.HomeScore + "-" + guess.AwayScore + ")");
                            break;
                        case GuessType.MKGK:
                            model.GuessList.ElementAt(2).Add(GetShortUserName(guess.User.UserName) + " (" + guess.HomeScore + "-" + guess.AwayScore + ")");
                            break;
                        case GuessType.MKGKD:
                            model.GuessList.ElementAt(3).Add(GetShortUserName(guess.User.UserName) + " (" + guess.HomeScore + "-" + guess.AwayScore + ")");
                            break;
                        case GuessType.MKOG:
                            model.GuessList.ElementAt(4).Add(GetShortUserName(guess.User.UserName) + " (" + guess.HomeScore + "-" + guess.AwayScore + ")");
                            break;
                        case GuessType.MK:
                            model.GuessList.ElementAt(5).Add(GetShortUserName(guess.User.UserName) + " (" + guess.HomeScore + "-" + guess.AwayScore + ")");
                            break;
                        case GuessType.CSG:
                            model.GuessList.ElementAt(6).Add(GetShortUserName(guess.User.UserName) + " (" + guess.HomeScore + "-" + guess.AwayScore + ")");
                            break;
                        case GuessType.OG:
                            model.GuessList.ElementAt(7).Add(GetShortUserName(guess.User.UserName) + " (" + guess.HomeScore + "-" + guess.AwayScore + ")");
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    model.GuessList.ElementAt(0).Add(GetShortUserName(guess.User.UserName) + " (" + guess.HomeScore + "-" + guess.AwayScore + ")");
                }


            }

            return model;
        } 

        public static List<GuessStatistic.StatHelper> GetStatHelpersFromPlayerMatchesWithGuesses(List<Guess> guesses, List<Match> matches, Dictionary<int,int> model)
        {

            List<GuessStatistic.StatHelper> helper = new List<GuessStatistic.StatHelper>();

            foreach (var m in model)
            {
                GuessStatistic.StatHelper h = new GuessStatistic.StatHelper();

                Match match = matches.Where(ma => ma.Id == m.Value).First();
                h.homeScore = match.HomeScore.HasValue ? match.HomeScore.Value.ToString() : "-";
                h.awayScore = match.AwayScore.HasValue ? match.AwayScore.Value.ToString() : "-";
                h.homeTeam = match.Team1.Name;
                h.awayTeam = match.Team.Name;

                Guess g = guesses.Where(gu => gu.Id == m.Key).First();
                h.homeGuess = g.HomeScore.ToString();
                h.awayGuess = g.AwayScore.ToString();

                helper.Add(h);
            }

            return helper;

        }

    }
}