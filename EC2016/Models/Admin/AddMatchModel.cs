using Bll;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EC2016.Models
{
    public class AddMatchModel
    {
        [Display(Name = "Bajnokság")]
        public int TournamentSelectedId { get; set; }

        [Display(Name = "Hazai csapat")]
        public int HomeTeamSelectedId { get; set; }

        [Display(Name = "Vendég csapat")]
        public int AwayTeamSelectedId { get; set; }

        [Display(Name = "Dátum")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Display(Name = "Típus")]
        public string Type { get; set; }

        public List<TeamModel> Teams { get; set; }

        public List<TournamentModel> Tournaments { get; set; }

        public IEnumerable<SelectListItem> HomeTeamList
        {
            get { return new SelectList(Teams, "Id", "Name"); }
        }

        public IEnumerable<SelectListItem> AwayTeamList
        {
            get { return new SelectList(Teams, "Id", "Name"); }
        }

        public IEnumerable<SelectListItem> TournamentList
        {
            get { return new SelectList(Tournaments, "Id", "Name"); }
        }

        public IEnumerable<SelectListItem> TypesList
        {
            get
            {
                List<object> types = new List<object>();
                types.Add(new { Name = MatchType.FirstRound });
                types.Add(new { Name = MatchType.SecondRound });
                types.Add(new { Name = MatchType.ThirdRound });
                types.Add(new { Name = MatchType.SemiQuaerterFinal });
                types.Add(new { Name = MatchType.QuarterFinal });
                types.Add(new { Name = MatchType.SemiFinal });
                types.Add(new { Name = MatchType.Final });
                return new SelectList(types, "Name", "Name");
            }
        }
    }
}