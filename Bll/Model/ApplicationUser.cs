using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Model
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Guesses = new HashSet<Guess>();
            GuessGames = new HashSet<GuessGame>();
        }


        public virtual ICollection<GuessGame> GuessGames { get; set; }

        public virtual ICollection<Guess> Guesses { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
