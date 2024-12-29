using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FilmApp.Web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) 
        {
            
        }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // rolleri yapılandıralım ( user,admin,superadmin)

            var adminRoleId = "f899a9c2-029a-4dae-b97c-d2a2517eddb5";
            var superAdminRoleId = "6a23b361-58b7-4e0a-97a9-dabb2f6da0fb";
            var userRoleId = "7bbe911a-4306-465f-9fcd-dd4d91216aee";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                    Id = superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            // superAdmin rolü

            var superAdminId = "9105bf38-b997-4908-8032-f77330415505";
            var superAdminUser = new IdentityUser
            {
                UserName = "superadmin@perdearkasi.com",
                NormalizedUserName = "superadmin@perdearkasi.com".ToUpper(),
                Email = "superadmin@perdearkasi.com",
                NormalizedEmail = "superadmin@perdearkasi.com".ToUpper(),
                Id = superAdminId
            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(superAdminUser, "SuperAdmin@123");

            builder.Entity<IdentityUser>().HasData(superAdminUser);


            // tüm rolleri superAdmin rolüne atayalım
            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = superAdminId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);





        }
    }
}

