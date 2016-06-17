using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EC2016.Models.Admin
{
    public class ModifyMatchModel
    {

        public List<TeamModel> Teams { get; set; }

        public List<MatchModel> Matches { get; set; }

        //public IEnumerable<SelectListItem> TeamNames
        //{
        //    get
        //    {
        //        List<SelectListItem> items = new List<SelectListItem>();
        //        foreach (var team in Teams)
        //        {
        //            items.Add(new SelectListItem
        //            {
        //                Value = team.Id.ToString(),
        //                Text = team.Name
        //            });
        //        }
        //        return items;
        //    }
        //}

    }
}