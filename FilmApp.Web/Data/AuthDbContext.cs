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

            // superAdmin rolü  2

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
                .HashPassword(superAdminUser, "SuperAdminYsf123");

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










            // superAdmin rolü  2

            var superAdmin2Id = "f3401107-afa2-47ea-8474-8d11cfe5de2a";
            var superAdmin2User = new IdentityUser
            {
                UserName = "superadmin@filmapp.com",
                NormalizedUserName = "superadmin@filmapp.com".ToUpper(),
                Email = "superadmin@filmapp.com",
                NormalizedEmail = "superadmin@filmapp.com".ToUpper(),
                Id = superAdmin2Id
            };


            // Şifreyi haslemeden veritabanına göndermek güvenlik açısından önerilmez. Ancak, 
            // şifreyi düz metin olarak kaydetmek için aşağıdaki satırı kullanabilirsiniz. 
            // Bu, yalnızca eğitim amaçlıdır ve gerçek uygulamalarda kullanılmamalıdır.

            superAdmin2User.PasswordHash = "SuperAdmin@123"; // Düz metin olarak kaydedildi

            builder.Entity<IdentityUser>().HasData(superAdmin2User);

            // tüm rolleri superAdmin rolüne atayalım
            var superAdmin2Roles = new List<IdentityUserRole<string>>
                {
                    new IdentityUserRole<string>
                    {
                        RoleId = adminRoleId,
                        UserId = superAdmin2Id
                    },
                    new IdentityUserRole<string>
                    {
                        RoleId = superAdminRoleId,
                        UserId = superAdmin2Id
                    },
                    new IdentityUserRole<string>
                    {
                        RoleId = userRoleId,
                        UserId = superAdmin2Id
                    }
                };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdmin2Roles);






































            



        }
    }
}

