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

        public string UserId { get; set; }

        public int Count { get; set; }

        public bool Max { get; set; }

        public int Bonus { get; set; }

        public int TTPoint { get; set; }

        public int MKGKPoint { get; set; }

        public int MKCSGPoint { get; set; }

        public int MKOGPoint { get; set; }

        public int MKPoint { get; set; }

        public int CSGPoint { get; set; }

        public int OGPoint { get; set; }

        public int Points
        {
            get
            {
                return TTPoint + MKGKPoint + MKCSGPoint + MKOGPoint + MKPoint + CSGPoint + OGPoint;
                //return (Max ? TT * GuessPoint.TTPoint : TT * (GuessPoint.TTPoint + 4)) + (GK - TT) * GuessPoint.GKPoint + (MK - TT) * GuessPoint.MKPoint + (CSG - TT) * GuessPoint.CSGPoint + (OG - TT) * GuessPoint.OGPoint + Bonus;
                //return (Max ? TT * GuessPoint.TTPoint : 12) + GK * GuessPoint.GKPoint + MK * GuessPoint.MKPoint + CSG * GuessPoint.CSGPoint + OG * GuessPoint.OGPoint;
            }
        }

    }
}