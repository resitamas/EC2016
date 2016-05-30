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

                    model.UsersWithGuesses = new List<IndexModel.UserGuesses>();
                    foreach (var u in users)
                    {
                        IndexModel.UserGuesses ug = new IndexModel.UserGuesses();
                        ug.User = ModelHelper.CreateUserModel(u);
                        ug.Guesses = u.Guesses.Select(g => ModelHelper.CreateGuessModel(g));

                        model.UsersWithGuesses.Add(ug);
                    }

                    break;


                default:
                    teams = new List<TeamModel>();
                    break;
            }

            matches = DatabaseManager.GetAllMatches().Select(m => ModelHelper.CreateMatchModel(m)).ToList();
            teams = DatabaseManager.GetAllTeam().Select(t => ModelHelper.CreateTeamModel(t)).ToList();

            model.ATeams = ModelHelper.CreateGorupTable(teams.Where(t => t.Group == "A").ToList(), matches);
            model.BTeams = ModelHelper.CreateGorupTable(teams.Where(t => t.Group == "B").ToList(), matches);
            model.CTeams = ModelHelper.CreateGorupTable(teams.Where(t => t.Group == "C").ToList(), matches);
            model.DTeams = ModelHelper.CreateGorupTable(teams.Where(t => t.Group == "D").ToList(), matches);
            model.ETeams = ModelHelper.CreateGorupTable(teams.Where(t => t.Group == "E").ToList(), matches);
            model.FTeams = ModelHelper.CreateGorupTable(teams.Where(t => t.Group == "F").ToList(), matches);

            model.Matches = matches;

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
            int hScore = homeScore.HasValue ? homeScore.Value : 0;
            int aScore = awayScore.HasValue ? awayScore.Value : 0;

            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            if (user.Guesses.Select(g => g.MatchId).Contains(matchId))
            {
                DatabaseManager.ModifyGuess(user.Id, matchId, hScore, aScore);
                return RedirectToAction("Index", "Home");
            }

            DatabaseManager.AddGuess(matchId, user.Id, hScore, aScore);


            return RedirectToAction("Index","Home");
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

    }
}