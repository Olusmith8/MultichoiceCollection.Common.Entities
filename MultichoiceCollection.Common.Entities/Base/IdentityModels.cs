using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MultichoiceCollection.Common.Entities.Enum;

namespace MultichoiceCollection.Common.Entities.Base
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string AgentName { get; set; }
        public string AccountNumber { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
           // Add custom user claims here
           /* userIdentity.AddClaims(new []
                            {
                                new Claim(CustomClaimTypes.FirstName, FirstName),
                                new Claim(CustomClaimTypes.LastName, LastName),
                                new Claim(CustomClaimTypes.FullName, FullName),
                                new Claim(ClaimTypes.Gender, Gender?.ToString()??""),
                                new Claim(ClaimTypes.Email, Email),
                                new Claim(ClaimTypes.MobilePhone, PhoneNumber),
                                new Claim(ClaimTypes.DateOfBirth,DateOfBirth?.ToString() ?? ""),
                                new Claim(ClaimTypes.StreetAddress, (string.IsNullOrEmpty(Address)?"":Address)),
                                new Claim(CustomClaimTypes.LastLoggedIn,LastLogin.ToString("F")),
                                new Claim(CustomClaimTypes.EmailConfirmed,EmailConfirmed.ToString()),
                                new Claim(CustomClaimTypes.PhoneNumberConfirmed,PhoneNumberConfirmed.ToString()),
                                new Claim(CustomClaimTypes.ProfilePicture,(string.IsNullOrEmpty(ProfilePicture)?"":ProfilePicture)),
                                 new Claim(CustomClaimTypes.IdentityCode,FormatIdentityCode(IdentityCode))
                                }
                            );*/

            return userIdentity;
        }

       


     
        //public virtual ICollection<TransactionHistory> TransactionHistories { get; set; } 
        private static string FormatIdentityCode(int indentityCode)
        {
            var transRef = indentityCode.ToString();
            if (transRef.Length < 4)
            {
                transRef = transRef.PadLeft(4, '0');
            }
            return transRef;
        }
    }

   


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base(ConnectionStrings.BaseConnectionString)
        {
        }
        public static ApplicationDbContext Create()
       {
            return new ApplicationDbContext();
        }
        
    }
}