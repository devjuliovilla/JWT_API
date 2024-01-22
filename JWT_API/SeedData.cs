using JWT_API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JWT_API
{
    public class SeedData
    {
        public static async Task InsertUserAndRolesAsync(AppDbContext dbContext, IPasswordHasher<User> passwordHasher)
        {
            try
            {
                if (!dbContext.Roles.Any())
                {
                    var roles = new List<Role>()
                    {
                     new Role{ Name="ADMIN"},
                     new Role{ Name="USER"},
                    };

                    dbContext.Roles.AddRange(roles);
                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.Users.Any())
                {
                    var adminUser = new User
                    {
                        Username = "admin"
                    };
                    adminUser.Password = passwordHasher.HashPassword(adminUser, "admin123");

                    Role adminRole = await dbContext.Roles
                        .FirstOrDefaultAsync(x => x.Name.ToUpper() == "ADMIN");

                    adminUser.RoleId = adminRole.Id;

                    dbContext.Users.Add(adminUser);

                    var simpleUser = new User
                    {
                        Username = "user"
                    };
                    simpleUser.Password = passwordHasher.HashPassword(adminUser, "user123");

                    Role userRole = await dbContext.Roles
                        .FirstOrDefaultAsync(x => x.Name.ToUpper() == "USER");

                    simpleUser.RoleId = adminRole.Id;

                    dbContext.Users.Add(simpleUser);

                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
