namespace Bll.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    //[Table("User")]
    //public partial class User
    //{
    //    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    //    public User()
    //    {
    //        Guesses = new HashSet<Guess>();
    //        GuessGames = new HashSet<GuessGame>();
    //    }

    //    public int Id { get; set; }

    //    [Required]
    //    [StringLength(30)]
    //    public string Name { get; set; }

    //    [Required]
    //    [MaxLength(64)]
    //    public byte[] PasswordHash { get; set; }

    //    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    //    public virtual ICollection<Guess> Guesses { get; set; }

    //    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    //    public virtual ICollection<GuessGame> GuessGames { get; set; }
    //}
}
