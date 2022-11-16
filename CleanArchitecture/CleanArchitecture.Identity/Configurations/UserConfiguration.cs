using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
           var hasher = new PasswordHasher<ApplicationUser>();

            builder.HasData(new ApplicationUser
            {
                Id = "0224f5ad-48e3-40d3-8171-4e3cb0017317",
                Email = "admin@localhost.com",
                NormalizedEmail = "admin@localhost.com",
                Nombre = "Raúl",
                Apellidos = "Cano Corbera",
                UserName = "racanoco",
                NormalizedUserName = "racanoco",
                PasswordHash = hasher.HashPassword(null, "$aDMin$"),
                EmailConfirmed = true,
            },
            new ApplicationUser
            {                
                Id = "9d86069e-7836-4785-955d-9061d44f338b",
                Email = "standardUser@localhost.com",
                NormalizedEmail = "standardUser@localhost.com",
                Nombre = "StandardUser",
                Apellidos = "Standard User",
                UserName = "standardUser",
                NormalizedUserName = "standardUser",
                PasswordHash = hasher.HashPassword(null, "$aDMin$"),
                EmailConfirmed = true,
            }
            );
        }
    }
}
