using Bll;
using Bll.Model;
using EC2016.Helper;
using EC2016.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EC2016.Controllers
{
    
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index(string menu = "guesses")
        {

            if (!System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return Redirect("~/Account/Login");
            }

            //ApplicationUser user = DatabaseManager.GetUserById(User.Identity.GetUserId());
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            ViewBag.User = user;
            ViewBag.menu = menu;

            //DatabaseManager.AddUserToGuessGame(user.Id,1);

            IndexModel model = new IndexModel();
            List<TeamModel> teams;
            List<MatchModel> matches;

            matches = DatabaseManager.GetAllMatches().Select(m => ModelHelper.CreateMatchModel(m)).ToList();
            model.Matches = matches;

            switch (menu)
            {
                case "guesses":

                    model.Guesses = user.Guesses.ToList().Select(g => ModelHelper.CreateGuessModel(g)).ToList();

                    break;

                case "teams":


                    break;

                case "games":

                    teams = new List<TeamModel>();
                    IEnumerable<ApplicationUser> users = DatabaseManager.GetUsersByGuessGame(1);

                    model.UsersWithGuessesAndStats = new List<IndexModel.UserGuesses>();
                    foreach (var u in users)
                    {
                        IndexModel.UserGuesses ug = new IndexModel.UserGuesses();
                        ug.User = ModelHelper.CreateUserModel(u);
                        ug.Guesses = u.Guesses.Select(g => ModelHelper.CreateGuessModel(g));
                        ModelHelper.Wrapper wrapper = ModelHelper.GetPlayerStatsFromGuesses(ug.Guesses, model.Matches);
                        ug.PlayerStats = wrapper.PlayerStatModel;
                        ug.PlayerStatsBefore = wrapper.PlayerStatModelBefore;
                        //ug.PlayerMatchesWithGuesses = wrapper.PlayerMatchesWithGuesses;

                        model.UsersWithGuessesAndStats.Add(ug);
                    }



                    break;


                default:
                    teams = new List<TeamModel>();
                    break;
            }

            teams = DatabaseManager.GetAllTeam().Select(t => ModelHelper.CreateTeamModel(t)).ToList();

            model.ATeams = ModelHelper.CreateGorupTable(teams.Where(t => t.Group == "A").ToList(), matches);
            model.BTeams = ModelHelper.CreateGorupTable(teams.Where(t => t.Group == "B").ToList(), matches);
            model.CTeams = ModelHelper.CreateGorupTable(teams.Where(t => t.Group == "C").ToList(), matches);
            model.DTeams = ModelHelper.CreateGorupTable(teams.Where(t => t.Group == "D").ToList(), matches);
            model.ETeams = ModelHelper.CreateGorupTable(teams.Where(t => t.Group == "E").ToList(), matches);
            model.FTeams = ModelHelper.CreateGorupTable(teams.Where(t => t.Group == "F").ToList(), matches);

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Guess(int matchId, int? homeScore, int? awayScore)
        {

            if (DatabaseManager.GetMatchById(matchId).Date.AddMinutes(-5).CompareTo(DateTime.Now) < 0)
            {
                //return RedirectToAction("Index", "Home");
                //return Json(new { error = true});
                return new JsonResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new { id = matchId ,error = true }
                };
            }


            int hScore = homeScore.HasValue ? homeScore.Value : 0;
            int aScore = awayScore.HasValue ? awayScore.Value : 0;

            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            if (user.Guesses.Select(g => g.MatchId).Contains(matchId))
            {
                DatabaseManager.ModifyGuess(user.Id, matchId, hScore, aScore);
                //return RedirectToAction("Index", "Home");
                //return Json(new { homeGuess = hScore, awayGuess = awayScore});
                return new JsonResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new { id = matchId, error = true , homeGuess = hScore, awayGuess = aScore}
                };
            }

            DatabaseManager.AddGuess(matchId, user.Id, hScore, aScore);

            //return Json(new { homeGuess = hScore, awayGuess = awayScore });
            //return RedirectToAction("Index","Home");
            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new { id = matchId, error = true, homeGuess = hScore, awayGuess = aScore }
            };
        }


        [HttpGet]
        public ActionResult Guesses()
        {
            return RedirectToAction("Index","Home", new { menu = "guesses"});
        }

        [HttpGet]
        public ActionResult Teams()
        {
            return RedirectToAction("Index", "Home", new { menu = "teams" });
        }


        [HttpGet]
        public ActionResult Game()
        {
            return RedirectToAction("Index", "Home", new { menu = "games" });
        }

        [HttpGet]
        public ActionResult GuessStatistic(string userId, string userName, string type)
        {
            GuessStatistic model = new GuessStatistic();
            model.UserName = userName;

            List<Guess> guesses = DatabaseManager.GetUserById(userId).Guesses.ToList();
            List<Match> matches = DatabaseManager.GetAllMatches();

            List<GuessModel> guessesModel = guesses.Select(g => ModelHelper.CreateGuessModel(g)).ToList();
            List<MatchModel> matchesModel = matches.Select(m => ModelHelper.CreateMatchModel(m)).ToList();

            ModelHelper.Wrapper wrapper = ModelHelper.GetPlayerStatsFromGuesses(guessesModel, matchesModel);

            switch (type)
            {
                case "TT":

                    model.Stats = ModelHelper.GetStatHelpersFromPlayerMatchesWithGuesses(guesses, matches, wrapper.PlayerMatchesWithGuesses.TTGuessesByMatch);
                    model.Type = "TT";

                    break;

                case "MKCSG":

                    model.Stats = ModelHelper.GetStatHelpersFromPlayerMatchesWithGuesses(guesses, matches, wrapper.PlayerMatchesWithGuesses.MKCSGGuessesByMatch);
                    model.Type = "MK + CSG";
                    break;

                case "MKGK":

                    model.Stats = ModelHelper.GetStatHelpersFromPlayerMatchesWithGuesses(guesses, matches, wrapper.PlayerMatchesWithGuesses.MKGKGuessesByMatch);
                    model.Type = "MK + GK";

                    break;

                case "MKGKDRAW":

                    model.Stats = ModelHelper.GetStatHelpersFromPlayerMatchesWithGuesses(guesses, matches, wrapper.PlayerMatchesWithGuesses.MKGKDRAWGuessesByMatch);
                    model.Type = "MK + GK (D)";

                    break;

                case "MKOG":

                    model.Stats = ModelHelper.GetStatHelpersFromPlayerMatchesWithGuesses(guesses, matches, wrapper.PlayerMatchesWithGuesses.MKOGGuessesByMatch);
                    model.Type = "MK + OG";

                    break;

                case "MK":

                    model.Stats = ModelHelper.GetStatHelpersFromPlayerMatchesWithGuesses(guesses, matches, wrapper.PlayerMatchesWithGuesses.MKGuessesByMatch);
                    model.Type = "MK";

                    break;

                case "CSG":

                    model.Stats = ModelHelper.GetStatHelpersFromPlayerMatchesWithGuesses(guesses, matches, wrapper.PlayerMatchesWithGuesses.CSGGuessesByMatch);
                    model.Type = "CSG";

                    break;

                case "OG":

                    model.Stats = ModelHelper.GetStatHelpersFromPlayerMatchesWithGuesses(guesses, matches, wrapper.PlayerMatchesWithGuesses.OGGuessesByMatch);
                    model.Type = "OG";

                    break;

                default:
                    break;
            }

            return PartialView("_GuessStatistic", model);
        }

        [HttpGet]
        public ActionResult MatchGuessStatistic(int matchId)
        {

            List<Guess> guesses = DatabaseManager.GetGuessesByMatch(matchId);
            Match match = DatabaseManager.GetMatchById(matchId,true);

            MatchGuessModel model = ModelHelper.CreateMatchGuessModel(guesses, match);

            return PartialView("_MatchGuessStatistic", model);
        }

    }
}