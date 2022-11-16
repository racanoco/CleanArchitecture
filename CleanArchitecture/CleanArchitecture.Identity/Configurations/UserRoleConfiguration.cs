using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations
{
    /// <summary>
    /// Clase que relaciona el usuario con su rol
    /// </summary>
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "cabd1dee-b0b4-48c2-a572-2d197390c289",
                    UserId = "0224f5ad-48e3-40d3-8171-4e3cb0017317"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "a677e5b8-bca7-48cb-900a-1834257ea88e",
                    UserId = "9d86069e-7836-4785-955d-9061d44f338b"
                }
                );
        }
    }
}
