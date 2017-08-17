using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace avalonbuild.com.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Navigation property for the roles this user belongs to.
        /// </summary>
        //public virtual ICollection<TUserRole> Roles { get; } = new List<TUserRole>();

        /// <summary>
        /// Navigation property for the claims this user possesses.
        /// </summary>
        //public virtual ICollection<TUserClaim> Claims { get; } = new List<TUserClaim>();

        /// <summary>
        /// Navigation property for this users login accounts.
        /// </summary>
        //public virtual ICollection<TUserLogin> Logins { get; } = new List<TUserLogin>();
    }
}
