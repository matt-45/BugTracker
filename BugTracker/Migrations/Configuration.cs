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
            if (!context.Users.Any(u => u.Email == "DemoManager2@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoManager2@mailinator.com",
                    Email = "DemoManager2@mailinator.com",
                    FirstName = "Demo2",
                    LastName = "Manager2",
                    DisplayName = "Demo Manager2",
                }, "Abc&123");
            }
            if (!context.Users.Any(u => u.Email == "DemoManager3@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoManager3@mailinator.com",
                    Email = "DemoManager3@mailinator.com",
                    FirstName = "Demo3",
                    LastName = "Manager3",
                    DisplayName = "Demo Manager3",
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
            if (!context.Users.Any(u => u.Email == "DemoDeveloper2@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoDeveloper2@mailinator.com",
                    Email = "DemoDeveloper2@mailinator.com",
                    FirstName = "Demo2",
                    LastName = "Developer2",
                    DisplayName = "Demo Developer2",
                }, "Abc&123");
            }
            if (!context.Users.Any(u => u.Email == "DemoDeveloper3@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoDeveloper3@mailinator.com",
                    Email = "DemoDeveloper3@mailinator.com",
                    FirstName = "Demo3",
                    LastName = "Developer3",
                    DisplayName = "Demo Developer3",
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
            if (!context.Users.Any(u => u.Email == "DemoSubmitter2@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoSubmitter2@mailinator.com",
                    Email = "DemoSubmitter2@mailinator.com",
                    FirstName = "Demo2",
                    LastName = "Submitter2",
                    DisplayName = "Demo Submitter2",
                }, "Abc&123");
            }
            if (!context.Users.Any(u => u.Email == "DemoSubmitter3@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoSubmitter3@mailinator.com",
                    Email = "DemoSubmitter3@mailinator.com",
                    FirstName = "Demo3",
                    LastName = "Submitter3",
                    DisplayName = "Demo Submitter3",
                }, "Abc&123");
            }

            // assign admin role

            var adminId = userManager.FindByEmail("mattpark102@outlook.com").Id;
            userManager.AddToRole(adminId, "Admin");

            var modId = userManager.FindByEmail("jasontwichell@mailinator.com").Id;
            userManager.AddToRole(modId, "Admin");

            var userId = userManager.FindByEmail("DemoAdmin@mailinator.com").Id;
            userManager.AddToRole(userId, "Admin");
            userId = userManager.FindByEmail("DemoManager@mailinator.com").Id;
            userManager.AddToRole(userId, "Manager");
            userId = userManager.FindByEmail("DemoDeveloper@mailinator.com").Id;
            userManager.AddToRole(userId, "Developer");
            userId = userManager.FindByEmail("DemoSubmitter@mailinator.com").Id;
            userManager.AddToRole(userId, "Submitter");

            userId = userManager.FindByEmail("DemoManager2@mailinator.com").Id;
            userManager.AddToRole(userId, "Manager");
            userId = userManager.FindByEmail("DemoDeveloper2@mailinator.com").Id;
            userManager.AddToRole(userId, "Developer");
            userId = userManager.FindByEmail("DemoSubmitter2@mailinator.com").Id;
            userManager.AddToRole(userId, "Submitter");

            userId = userManager.FindByEmail("DemoManager3@mailinator.com").Id;
            userManager.AddToRole(userId, "Manager");
            userId = userManager.FindByEmail("DemoDeveloper3@mailinator.com").Id;
            userManager.AddToRole(userId, "Developer");
            userId = userManager.FindByEmail("DemoSubmitter3@mailinator.com").Id;
            userManager.AddToRole(userId, "Submitter");

            context.Statuses.AddOrUpdate(
                t => t.Name,
                    new TicketStatus { Name = "Assigned", Description = "This ticket has been assigned to a developer." },
                    new TicketStatus { Name = "Unassigned", Description = "This ticket has not been assigned yet."},
                    new TicketStatus { Name = "Resolved", Description = "This ticket has been resolved."},
                    new TicketStatus { Name = "Archived", Description = "This ticket has been archived."}
            ); // the same can be done for ticket priority, projects, and tickets.

            context.Types.AddOrUpdate(
                s => s.Name,
                    new TicketType { Name = "UI", Description = "There is a problem with the UI."},
                    new TicketType { Name = "Server", Description = "There is a server issue."},
                    new TicketType { Name = "Feature Request", Description = "There is a feature request."},
                    new TicketType { Name = "Defect", Description = "There is an important defect in the software."}
            );

            context.Priorities.AddOrUpdate(
                p => p.Name,
                    new TicketPriority { Name = "Low", Description = "This ticket has a low priority." },
                    new TicketPriority { Name = "Medium", Description = "This ticket has a medium priority." },
                    new TicketPriority { Name = "High", Description = "This ticket has a high priority." }
            );

            context.Projects.AddOrUpdate(
                p => p.Name,
                new Project { Name = "Project 1", Description = "This is a short description of project 1"}
            );

            context.SaveChanges();

            var projects = context.Projects;
            var statuses = context.Statuses;
            var priorities = context.Priorities;
            var types = context.Types;
            var users = context.Users;
            var roles = context.Roles;

            context.Tickets.AddOrUpdate(
                t => t.Title,
                new Ticket
                {
                    ProjectId = projects.FirstOrDefault(p => p.Name.Contains("Project 1")).Id,
                    TicketTypeId = types.FirstOrDefault(t => t.Name.Contains("UI")).Id,
                    TicketPriorityId = priorities.FirstOrDefault(p => p.Name.Contains("High")).Id,
                    TicketStatusId = statuses.FirstOrDefault(s => s.Name.Contains("Assigned")).Id,
                    OwnerUserId = users.FirstOrDefault(u => u.Email.Contains("DemoSubmitter@mailinator.com")).Id,
                    AssignedToUserId = users.FirstOrDefault(u => u.Email.Contains("DemoDeveloper@mailinator.com")).Id,
                    Title = "Demo Ticket 1",
                    Description = "A demo ticket that beings assigned to a developer at high priority",
                    Created = DateTime.Now
                });

            context.SaveChanges();
            // if I want to seed a ticket, I need the fk of at least the project.

        }
    }
}
