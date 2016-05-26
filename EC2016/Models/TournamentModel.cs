using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EC2016.Models
{
    public class TournamentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }

    }
}