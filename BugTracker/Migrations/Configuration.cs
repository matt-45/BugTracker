namespace BugTracker.Migrations
{
    using BugTracker.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        private ProjectsHelper projectHelper = new ProjectsHelper();
        private Random random = new Random();
        private UserRoleHelper roleHelper = new UserRoleHelper();
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
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
            if (!context.Roles.Any(r => r.Name == "NewUser"))
            {
                roleManager.Create(new IdentityRole { Name = "NewUser" });
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
                    AvatarPath = "/Uploads/default-user.jpg",
                    IsDemoUser = false
                }, "LargeEggs123!");
            }

            if (!context.Users.Any(u => u.Email == "RealDeveloper@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "RealDeveloper@mailinator.com",
                    Email = "RealDeveloper@mailinator.com",
                    FirstName = "Charmaine",
                    LastName = "Prentice",
                    DisplayName = "Charmaine",
                    AvatarPath = "/Uploads/default-user.jpg",
                    IsDemoUser = false
                }, "Abc&123");
            }
            if (!context.Users.Any(u => u.Email == "RealManager@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "RealManager@mailinator.com",
                    Email = "RealManager@mailinator.com",
                    FirstName = "Arun",
                    LastName = "Calhoun",
                    DisplayName = "Arun",
                    AvatarPath = "/Uploads/default-user.jpg",
                    IsDemoUser = false
                }, "Abc&123");
            }
            if (!context.Users.Any(u => u.Email == "RealSubmitter@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "RealSubmitter@mailinator.com",
                    Email = "RealSubmitter@mailinator.com",
                    FirstName = "Renzo",
                    LastName = "Tanner",
                    DisplayName = "Renzo",
                    AvatarPath = "/Uploads/default-user.jpg",
                    IsDemoUser = false
                }, "Abc&123");
            }

            if (!context.Users.Any(u => u.Email == "DemoAdmin@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoAdmin@mailinator.com",
                    Email = "DemoAdmin@mailinator.com",
                    FirstName = "Fabio",
                    LastName = "Nobel",
                    DisplayName = "Demo",
                    AvatarPath = "/Uploads/default-user.jpg",
                    IsDemoUser = true
                }, "Abc&123");
            }
            if (!context.Users.Any(u => u.Email == "DemoManager@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoManager@mailinator.com",
                    Email = "DemoManager@mailinator.com",
                    FirstName = "Reema",
                    LastName = "Chung",
                    DisplayName = "Demo",
                    AvatarPath = "/Uploads/default-user.jpg",
                    IsDemoUser = true
                }, "Abc&123");
            }
            if (!context.Users.Any(u => u.Email == "DemoManager2@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoManager2@mailinator.com",
                    Email = "DemoManager2@mailinator.com",
                    FirstName = "Anis",
                    LastName = "Woods",
                    DisplayName = "Demo",
                    AvatarPath = "/Uploads/default-user.jpg",
                    IsDemoUser = true
                }, "Abc&123");
            }
            if (!context.Users.Any(u => u.Email == "DemoManager3@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoManager3@mailinator.com",
                    Email = "DemoManager3@mailinator.com",
                    FirstName = "Roger",
                    LastName = "Farley",
                    DisplayName = "Demo",
                    AvatarPath = "/Uploads/default-user.jpg",
                    IsDemoUser = true
                }, "Abc&123");
            }
            if (!context.Users.Any(u => u.Email == "DemoDeveloper@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoDeveloper@mailinator.com",
                    Email = "DemoDeveloper@mailinator.com",
                    FirstName = "Romany",
                    LastName = "Paine",
                    DisplayName = "Demo",
                    AvatarPath = "/Uploads/default-user.jpg",
                    IsDemoUser = true
                }, "Abc&123");
            }
            if (!context.Users.Any(u => u.Email == "DemoDeveloper2@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoDeveloper2@mailinator.com",
                    Email = "DemoDeveloper2@mailinator.com",
                    FirstName = "Cole",
                    LastName = "Serrano",
                    DisplayName = "Demo",
                    AvatarPath = "/Uploads/default-user.jpg",
                    IsDemoUser = true
                }, "Abc&123");
            }
            if (!context.Users.Any(u => u.Email == "DemoDeveloper3@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoDeveloper3@mailinator.com",
                    Email = "DemoDeveloper3@mailinator.com",
                    FirstName = "Tiffany",
                    LastName = "Joyce",
                    DisplayName = "Demo",
                    AvatarPath = "/Uploads/default-user.jpg",
                    IsDemoUser = true
                }, "Abc&123");
            }
            if (!context.Users.Any(u => u.Email == "DemoSubmitter@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoSubmitter@mailinator.com",
                    Email = "DemoSubmitter@mailinator.com",
                    FirstName = "Lisa",
                    LastName = "Chadwick",
                    DisplayName = "Demo",
                    AvatarPath = "/Uploads/default-user.jpg",
                    IsDemoUser = true
                }, "Abc&123");
            }
            if (!context.Users.Any(u => u.Email == "DemoSubmitter2@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoSubmitter2@mailinator.com",
                    Email = "DemoSubmitter2@mailinator.com",
                    FirstName = "Antonio",
                    LastName = "Weaver",
                    DisplayName = "Demo",
                    AvatarPath = "/Uploads/default-user.jpg",
                    IsDemoUser = true
                }, "Abc&123");
            }
            if (!context.Users.Any(u => u.Email == "DemoSubmitter3@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoSubmitter3@mailinator.com",
                    Email = "DemoSubmitter3@mailinator.com",
                    FirstName = "Kara",
                    LastName = "Ramsey",
                    DisplayName = "Demo",
                    AvatarPath = "/Uploads/default-user.jpg",
                    IsDemoUser = true
                }, "Abc&123");
            }

            // assign admin role

            var adminId = userManager.FindByEmail("mattpark102@outlook.com").Id;
            userManager.AddToRole(adminId, "Admin");


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

            userId = userManager.FindByEmail("RealSubmitter@mailinator.com").Id;
            userManager.AddToRole(userId, "Submitter");
            userId = userManager.FindByEmail("RealDeveloper@mailinator.com").Id;
            userManager.AddToRole(userId, "Developer");
            userId = userManager.FindByEmail("RealManager@mailinator.com").Id;
            userManager.AddToRole(userId, "Manager");

            context.Projects.AddOrUpdate(
                p => p.Name,
                new Project { Name = "First Demo", Description = "The first demo project seed" },
                new Project { Name = "Second Demo", Description = "The second demo project seed" });

            context.Statuses.AddOrUpdate(
                t => t.Name,
                    new TicketStatus { Name = "Assigned", Description = "This ticket has been assigned to a developer." },
                    new TicketStatus { Name = "Unassigned", Description = "This ticket has not been assigned yet."},
                    new TicketStatus { Name = "Resolved", Description = "This ticket has been resolved."},
                    new TicketStatus { Name = "Archived", Description = "This ticket has been archived."}
            );

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

            context.SaveChanges();

            var projects = context.Projects.ToList();
            var statuses = context.Statuses.ToList();
            var priorities = context.Priorities.ToList();
            var types = context.Types.ToList();
            var users = context.Users.ToList();
            var roles = context.Roles.ToList();

            /*context.Tickets.AddOrUpdate(
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
                });*/
            var maxSubmitters = roleHelper.UsersInRole("Submitter").Count();
            var maxDevelopers = roleHelper.UsersInRole("Developer").Count();
            var submitters = roleHelper.UsersInRole("Submitter").ToList();
            var developers = roleHelper.UsersInRole("Developer").ToList();
            foreach (var project in projects)
            {
                var count = 1;
                foreach (var status in statuses)
                {
                    foreach (var type in types)
                    {
                        foreach (var priority in priorities)
                        {
                            var randomSubmitter = submitters[random.Next(0, maxSubmitters)];
                            var randomDeveloper = developers[random.Next(0, maxDevelopers)];
                            var randomDay = random.Next(0, 61);
                            context.Tickets.AddOrUpdate(
                                t => t.Title,
                                new Ticket
                                {
                                    ProjectId = project.Id,
                                    TicketTypeId = type.Id,
                                    TicketPriorityId = priority.Id,
                                    TicketStatusId = status.Id,
                                    OwnerUserId= randomSubmitter.Id,
                                    AssignedToUserId = randomDeveloper.Id,
                                    Title = project.Name + " Demo Ticket " + count,
                                    Description = "A demo ticket of priority '" + priority.Name + "', type '" + type.Name + "', status '" + status.Name + "'",
                                    Created = DateTime.Now.AddDays(-randomDay)
                                });
                            count++;
                            context.SaveChanges();
                        }
                    }
                }
                foreach (var user in roleHelper.UsersInRole("Manager").Concat(roleHelper.UsersInRole("Developer")).Concat(roleHelper.UsersInRole("Submitter")))
                {
                    var randomNumber = random.Next(0, 2);
                    if (randomNumber == 0)
                    {
                        projectHelper.AddUserToProject(user.Id, project.Id);
                        context.SaveChanges();
                    }
                    
                }
                
            }
            /*foreach (var project in projects)
            {
                project.Users.Add(users.FirstOrDefault(u => u.Email.Contains("demosubmitter@mailinator.com")));
            }*/

            context.SaveChanges();
            // if I want to seed a ticket, I need the fk of at least the project.

        }
    }
}
