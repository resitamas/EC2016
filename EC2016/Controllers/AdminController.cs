using Bll;
using Bll.Model;
using EC2016.Helper;
using EC2016.Models;
using EC2016.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace EC2016.Controllers
{
    [RequireHttps]
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {

        [HttpGet]
        public ActionResult AddMatch()
        {

            List<Team> teams = DatabaseManager.GetAllTeam();
            List<Tournament> tournaments = DatabaseManager.GetAllTournament();

            AddMatchModel model = new AddMatchModel();
            model.Teams = teams.Select(t => ModelHelper.CreateTeamModel(t)).ToList();
            model.Tournaments = tournaments.Select(t => ModelHelper.CreateTournamentModel(t)).ToList();
            model.Date = DateTime.Now.AddDays(27);
            model.Type = MatchType.ThirdRound;

            return View(model);
        }

        [HttpPost]
        public ActionResult AddMatch(AddMatchModel model)
        {
            DatabaseManager.AddMatch(model.HomeTeamSelectedId, model.AwayTeamSelectedId, model.TournamentSelectedId, model.Date, model.Type);

            return Redirect("/admin/addmatch");
        }

        public ActionResult ModifyMatch()
        {
            ModifyMatchModel model = new ModifyMatchModel();

            model.Matches = DatabaseManager.GetAllMatches().Select(m => ModelHelper.CreateMatchModel(m)).ToList();
            model.Teams = DatabaseManager.GetAllTeam().Select(t => ModelHelper.CreateTeamModel(t)).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult ModifyMatch(MatchModel model)
        {

            DatabaseManager.ModifyMatch(model.Id, model.TournamentId, model.HomeTeamId, model.AwayTeamId, model.Date, model.Type, model.HomeScore, model.AwayScore, model.IsConfirmed);

            return Redirect("/admin/modifymatch");
        }

    }
}
