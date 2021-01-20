using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetMe.Data
{
    public static class ApplicationDbContextSeeder
    {
        // http://www.binaryintellect.net/articles/5e180dfa-4438-45d8-ac78-c7cc11735791.aspx
        public static async Task SeedRolesAndUsersAsync(
            RoleManager<IdentityRole> roleManager, 
            UserManager<ApplicationUser> userManager)
        {
            var roleName = "admin";
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            var userName = "admin@microsoft.com";
            if (!await userManager.Users.AnyAsync(x => x.UserName == userName))
            {
                var user = new ApplicationUser()
                {
                    UserName = userName,
                    Email = userName,
                    EmailConfirmed = true,
                    FirstName = "Admin",
                    LastName = "User"
                };
                await userManager.CreateAsync(user, "Password1.");
                await userManager.AddToRoleAsync(user, roleName);
            }
        }

        private static void SeedMeetings(ApplicationDbContext db)
        {
            if (!db.Meetings.Any())
            {
                db.Meetings.Add(new Meeting()
                {
                    Title = "English Speaking Club",
                    Description = "An English Club is a place for language learners to use English in a casual setting. Practising your skills in the classroom is important.",
                    Place = "Route Cafe, Ankara",
                    MeetingTime = DateTime.Now.AddDays(7),
                    PhotoPath = "meeting1.jpg"
                });
                db.Meetings.Add(new Meeting()
                {
                    Title = "Environmental Pollution",
                    Description = "This conference aims to bring together leading academic scientists, researchers and research scholars to exchange and share their experiences and research results on all aspects of Environmental Pollution, Public Health and Impacts. ",
                    Place = "Congresium",
                    MeetingTime = DateTime.Now.AddDays(30),
                    PhotoPath = "meeting2.jpg"
                });
                db.SaveChanges();
            }
        }

        public static async Task<IHost> SeedAsync(this IHost host)
        {
            // http://www.binaryintellect.net/articles/5e180dfa-4438-45d8-ac78-c7cc11735791.aspx
            // https://github.com/dotnet-architecture/eShopOnWeb/blob/master/src/Web/Startup.cs
            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var env = serviceProvider.GetRequiredService<IHostEnvironment>();
                var db = serviceProvider.GetRequiredService<ApplicationDbContext>();
                await SeedRolesAndUsersAsync(roleManager, userManager);

                if (env.IsDevelopment())
                {
                    SeedMeetings(db);
                }
            }
            return host;
        }

        
    }
}
