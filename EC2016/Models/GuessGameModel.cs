using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EC2016.Models
{
    public class GuessGameModel
    {

        public int Id { get; set; }
        public int TournamentId { get; set; }
        public string Name { get; set; }
        public byte[] PasswordHash { get; set; }

    }
}