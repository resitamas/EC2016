namespace Bll.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    public partial class EC2016CodeFirst : IdentityDbContext<ApplicationUser>
    {
        public EC2016CodeFirst()
            : base("name=EC2016CodeFirst")
        {
        }

        public static EC2016CodeFirst Create()
        {
            return new EC2016CodeFirst();
        }


        // Identity and Authorization
        public DbSet<IdentityUserLogin> UserLogins { get; set; }
        public DbSet<IdentityUserClaim> UserClaims { get; set; }
        public DbSet<IdentityUserRole> UserRoles { get; set; }


        public virtual DbSet<Guess> Guesses { get; set; }
        public virtual DbSet<GuessGame> GuessGames { get; set; }
        public virtual DbSet<Match> Matches { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<Tournament> Tournaments { get; set; }
        
        //public virtual DbSet<User> Users1 { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GuessGame>()
                .Property(e => e.PasswordHash)
                .IsFixedLength();

            modelBuilder.Entity<GuessGame>()
                .HasMany(e => e.Users)
                .WithMany(e => e.GuessGames)
                .Map(m => m.ToTable("UserGuessGame").MapLeftKey("GuessGameId").MapRightKey("UserId"));

            modelBuilder.Entity<Match>()
                .HasMany(e => e.Guesses)
                .WithRequired(e => e.Match)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Team>()
                .HasMany(e => e.Matches)
                .WithRequired(e => e.Team)
                .HasForeignKey(e => e.AwayTeamId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Team>()
                .HasMany(e => e.Matches1)
                .WithRequired(e => e.Team1)
                .HasForeignKey(e => e.HomeTeamId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tournament>()
                .HasMany(e => e.GuessGames)
                .WithRequired(e => e.Tournament)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tournament>()
                .HasMany(e => e.Matches)
                .WithRequired(e => e.Tournament)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tournament>()
                .HasMany(e => e.Teams)
                .WithRequired(e => e.Tournament)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<User>()
            //    .Property(e => e.PasswordHash)
            //    .IsFixedLength();

            //modelBuilder.Entity<User>()
            //    .HasMany(e => e.Guesses)
            //    .WithRequired(e => e.User)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });

            // Configure Asp Net Identity Tables
            //modelBuilder.Entity<IdentityUser>().ToTable("User");
            modelBuilder.Entity<IdentityUser>().Property(u => u.PasswordHash).HasMaxLength(500);
            modelBuilder.Entity<IdentityUser>().Property(u => u.SecurityStamp).HasMaxLength(500);
            modelBuilder.Entity<IdentityUser>().Property(u => u.PhoneNumber).HasMaxLength(50);

            modelBuilder.Entity<IdentityRole>().ToTable("Role");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRole");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogin");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaim");
            modelBuilder.Entity<IdentityUserClaim>().Property(u => u.ClaimType).HasMaxLength(150);
            modelBuilder.Entity<IdentityUserClaim>().Property(u => u.ClaimValue).HasMaxLength(500);
        }
    }
}
