namespace Bll.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Match")]
    public partial class Match
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Match()
        {
            Guesses = new HashSet<Guess>();
        }

        public int Id { get; set; }

        public int TournamentId { get; set; }

        public int HomeTeamId { get; set; }

        public int AwayTeamId { get; set; }

        public DateTime Date { get; set; }

        [Required]
        [StringLength(20)]
        public string Type { get; set; }

        public int? HomeScore { get; set; }

        public int? AwayScore { get; set; }

        public bool isConfirmed { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Guess> Guesses { get; set; }

        public virtual Team Team { get; set; }

        public virtual Team Team1 { get; set; }

        public virtual Tournament Tournament { get; set; }
    }
}
