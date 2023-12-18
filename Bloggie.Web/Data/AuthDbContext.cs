using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Seed rolls
            var adminRoleId = "bc80aa7e-94cc-4570-8978-e697a3bdfd4c";
            var superAdminRoleId = "1f9de671-b7c6-4f99-a3a7-a890ddecdbe0";
            var userRoleId = "3aadf660-ac10-43ab-a2a0-56c3125e5b46";
            var roles = new List<IdentityRole> 
            {  
                new IdentityRole
            {
                Name="Admin",
                NormalizedName="Admin",
                Id=adminRoleId,
                ConcurrencyStamp=adminRoleId
            },
            new IdentityRole
            {
                Name="SuperAdmin",
                NormalizedName="SuperAdmin",
                Id=superAdminRoleId,
                ConcurrencyStamp=superAdminRoleId
            },
            new IdentityRole
            {
                Name="User",
                NormalizedName="User",
                Id=userRoleId,
                ConcurrencyStamp=userRoleId
            }
                };

            builder.Entity<IdentityRole>().HasData(roles);
            //seed superadmin
            var superAdminId = "267b78ed-74d1-407d-b99d-f6c520edf881";
            var superAdminUser = new IdentityUser
            {
                UserName = "superadmin@bloggie.com",
                Email = "superadmin@bloggie.com",
                NormalizedEmail = "superadmin@bloggie.com".ToUpper(),
                NormalizedUserName = "superadmin@bloggie.com".ToUpper(),
                Id=superAdminId
            };
            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().
                HashPassword(superAdminUser, "Superadmin@123");
            builder.Entity<IdentityUser>().HasData(superAdminUser);
            //add al to superadmin
            var superAdminRoles = new List<IdentityUserRole<string>>
            { 
                new IdentityUserRole<string>
            { 
                    RoleId=adminRoleId ,
                    UserId=superAdminId
            },
            new IdentityUserRole<string> 
             {
                    RoleId=superAdminRoleId, 
                    UserId=superAdminId
              },
            new IdentityUserRole<string>
                {
                RoleId=userRoleId,
                UserId=superAdminId
                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }
    }
}
