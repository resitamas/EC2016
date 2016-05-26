namespace Bll.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Guess")]
    public partial class Guess
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int MatchId { get; set; }

        public int HomeScore { get; set; }

        public int AwayScore { get; set; }

        public virtual Match Match { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
