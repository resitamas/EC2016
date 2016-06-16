using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EC2016.Models
{
    public class PlayerMatchesWithGuesses
    {

        public PlayerMatchesWithGuesses()
        {
            TTGuessesByMatch = new Dictionary<int, int>();
            MKCSGGuessesByMatch = new Dictionary<int, int>();
            MKGKDRAWGuessesByMatch = new Dictionary<int, int>();
            MKGKGuessesByMatch = new Dictionary<int, int>();
            MKOGGuessesByMatch = new Dictionary<int, int>();
            MKGuessesByMatch = new Dictionary<int, int>();
            CSGGuessesByMatch = new Dictionary<int, int>();
            OGGuessesByMatch = new Dictionary<int, int>();
            NONEGuessesByMatch = new Dictionary<int, int>();
        }

        public Dictionary<int,int> TTGuessesByMatch { get; set; }
        public Dictionary<int,int> MKGKGuessesByMatch { get; set; }
        public Dictionary<int,int> MKGKDRAWGuessesByMatch { get; set; }
        public Dictionary<int,int> MKCSGGuessesByMatch { get; set; }
        public Dictionary<int,int> MKOGGuessesByMatch { get; set; }
        public Dictionary<int,int> MKGuessesByMatch { get; set; }
        public Dictionary<int,int> CSGGuessesByMatch { get; set; }
        public Dictionary<int,int> OGGuessesByMatch { get; set; }
        public Dictionary<int,int> NONEGuessesByMatch { get; set; }

    }
}