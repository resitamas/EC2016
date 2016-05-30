using Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EC2016.Models
{
    public class PlayerStatModel
    {

        public int TT { get; set; }

        public int GK { get; set; }

        public int MK { get; set; }

        public int CSG { get; set; }

        public int OG { get; set; }

        public string UserName { get; set; }


        public int Points
        {
            get
            {
                return TT * GuessPoint.TTPoint + GK * GuessPoint.GKPoint + MK * GuessPoint.MKPoint + CSG * GuessPoint.CSGPoint + OG * GuessPoint.OGPoint;
            }
        }

    }
}