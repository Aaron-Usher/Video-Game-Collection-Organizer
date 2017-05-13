namespace VideoGameMVCLayer.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Security.Claims;
    using VideoGameDataObjects;
    using VideoGameMVCLayer.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<VideoGameMVCLayer.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "VideoGameMVCLayer.Models.ApplicationDbContext";
        }

        protected override void Seed(VideoGameMVCLayer.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            if (!context.Users.Any(u => u.UserName == "zhugelianftw@gmail.com"))
            {
                var user = new ApplicationUser()
                {
                    UserName = "zhugelianftw@gmail.com",
                    Email = "zhugelianftw@gmail.com",
                    //FirstName = "System",
                    //LastName  = "Admin"

                };
                /*Note that I would use a more secure password here, but I need to be able to remember this
                 * and I'm unwilling to use any of my normal passwords.*/
                IdentityResult result = userManager.Create(user, "password");

                if (result.Succeeded)
                {
                    userManager.AddClaim(user.Id, new Claim(ClaimTypes.GivenName, "Aaron"));
                    userManager.AddClaim(user.Id, new Claim(ClaimTypes.Surname, "Usher"));

                    context.Roles.AddOrUpdate(r => r.Name, new IdentityRole { Name = "Administrator" });
                    context.SaveChanges();

                    userManager.AddToRole(user.Id, "Administrator");
                    context.SaveChanges();

                    VideoGameLogicLayer.UserManager myUserManager = new VideoGameLogicLayer.UserManager();
                    myUserManager.RegisterUser(new User()
                    {
                        Roles = { "Administrator" },
                        Username = "zhugelianftw@gmail.com"
                    });
                }
            }
        }
    }
}
