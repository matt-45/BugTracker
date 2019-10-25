namespace BugTracker.Migrations
{
    using BugTracker.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BugTracker.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BugTracker.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }
            if (!context.Roles.Any(r => r.Name == "Manager"))
            {
                roleManager.Create(new IdentityRole { Name = "Manager" });
            }
            if (!context.Roles.Any(r => r.Name == "Developer"))
            {
                roleManager.Create(new IdentityRole { Name = "Developer" });
            }
            if (!context.Roles.Any(r => r.Name == "Submitter"))
            {
                roleManager.Create(new IdentityRole { Name = "Submitter" });
            }

            // user creation

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.Email == "mattpark102@outlook.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "mattpark102@outlook.com",
                    Email = "mattpark102@outlook.com",
                    FirstName = "Matthew",
                    LastName = "Park",
                    DisplayName = "Matt",
                }, "LargeEggs123!");
            }

            if (!context.Users.Any(u => u.Email == "jasontwichell@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "jasontwichell@mailinator.com",
                    Email = "jasontwichell@mailinator.com",
                    FirstName = "Jason",
                    LastName = "Twichell",
                    DisplayName = "Jason",
                }, "Abc&123");
            }

            if (!context.Users.Any(u => u.Email == "DemoAdmin@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoAdmin@mailinator.com",
                    Email = "DemoAdmin@mailinator.com",
                    FirstName = "Demo",
                    LastName = "Admin",
                    DisplayName = "Demo Admin",
                }, "Abc&123");
            }
            if (!context.Users.Any(u => u.Email == "DemoManager@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoManager@mailinator.com",
                    Email = "DemoManager@mailinator.com",
                    FirstName = "Demo",
                    LastName = "Manager",
                    DisplayName = "Demo Manager",
                }, "Abc&123");
            }
            if (!context.Users.Any(u => u.Email == "DemoDeveloper@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoDeveloper@mailinator.com",
                    Email = "DemoDeveloper@mailinator.com",
                    FirstName = "Demo",
                    LastName = "Developer",
                    DisplayName = "Demo Developer",
                }, "Abc&123");
            }
            if (!context.Users.Any(u => u.Email == "DemoSubmitter@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoSubmitter@mailinator.com",
                    Email = "DemoSubmitter@mailinator.com",
                    FirstName = "Demo",
                    LastName = "Submitter",
                    DisplayName = "Demo Submitter",
                }, "Abc&123");
            }

            // assign admin role

            var adminId = userManager.FindByEmail("mattpark102@outlook.com").Id;
            userManager.AddToRole(adminId, "Admin");

            var modId = userManager.FindByEmail("jasontwichell@mailinator.com").Id;
            userManager.AddToRole(modId, "Admin");

            var demoAdminId = userManager.FindByEmail("DemoAdmin@mailinator.com").Id;
            userManager.AddToRole(demoAdminId, "Admin");
            var demoManagerId = userManager.FindByEmail("DemoManager@mailinator.com").Id;
            userManager.AddToRole(demoManagerId, "Manager");
            var demoDeveloperId = userManager.FindByEmail("DemoDeveloper@mailinator.com").Id;
            userManager.AddToRole(demoDeveloperId, "Developer");
            var demoSubmitterId = userManager.FindByEmail("DemoSubmitter@mailinator.com").Id;
            userManager.AddToRole(demoSubmitterId, "Submitter");

            /*context.Statuses.AddOrUpdate(
                t => t.Name,
                    new TicketStatus { Name = "Open", Description = ""} // assigned, in progressn resolved, archived.
            ); // the same can be done for ticket priority, projects, and tickets.


            var blogId = context.Projects.FirstOrDefault(p => p.Name == "Test").Id;
            var projects = context.Projects;
            context.Projects.AddOrUpdate(
                p => p.Name,
                new Project { Name = "Test", Description = "Description", Id = blogId}
            );

            context.Tickets.AddOrUpdate(
                t => t.Title,
                new Ticket { Title = "Ticket title", Description = "Description"} // set parrents.
            );*/

            context.SaveChanges();
            // if I want to seed a ticket, I need the fk of at least the project.

        }
    }
}
