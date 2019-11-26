using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RealEstate.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        
        public virtual ICollection<Ad> Ads { get; set; }
        public virtual ICollection<Property> Properties { get; set; }

        [InverseProperty("Payer")]
        public virtual ICollection<Agreement> PayerEnd { get; set; }
        [InverseProperty("Payee")]
        public virtual ICollection<Agreement> PayeeEnd { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("RealEstate", throwIfV1Schema: false)
        {
        }
        public virtual DbSet<Ad> Ads { get; set; }
        public virtual DbSet<Agreement> Agreements { get; set; }
        public virtual DbSet<Plot> Plots { get; set; }
        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<Residential> Residentials { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Property>()
                .HasMany(e => e.Ads)
                .WithOptional(e => e.Property)
                .HasForeignKey(e => e.Property_Id);

            modelBuilder.Entity<Property>()
                .HasOptional(e => e.Plot)
                .WithRequired(e => e.Property);

            modelBuilder.Entity<Property>()
                .HasOptional(e => e.Residential)
                .WithRequired(e => e.Property);

        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}