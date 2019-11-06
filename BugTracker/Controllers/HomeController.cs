using BugTracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRoleHelper RoleHelper = new UserRoleHelper();
        private ProjectsHelper ProjectHelper = new ProjectsHelper();

        // GET: Projects
        [Authorize]
        public ActionResult Index(DashboardViewModel viewModel)
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            viewModel.User = user;
            viewModel.ApplicationUsers = db.Users.ToList();
            viewModel.Managers = RoleHelper.UsersInRole("Manager").ToList();
            viewModel.Developers = RoleHelper.UsersInRole("Developer").ToList();
            viewModel.Submitters = RoleHelper.UsersInRole("Submitter").ToList();

            if (RoleHelper.ListUserRoles(user.Id).FirstOrDefault() == "Admin")
            {
                viewModel.Projects = db.Projects.ToList();
                viewModel.Tickets = db.Tickets.ToList();
            }
            else if (RoleHelper.ListUserRoles(user.Id).FirstOrDefault() == "Manager")
            {
                viewModel.Projects = ProjectHelper.ListUserProjects(user.Id);
                viewModel.Tickets = ProjectHelper.ListUserProjects(user.Id).SelectMany(p => p.Tickets).ToList();
            }
            else if (RoleHelper.ListUserRoles(user.Id).FirstOrDefault() == "Developer")
            {
                viewModel.Projects = ProjectHelper.ListUserProjects(user.Id);
                viewModel.Tickets = ProjectHelper.ListUserProjects(user.Id).SelectMany(p => p.Tickets.Where(t => t.AssignedToUser.Id == user.Id)).ToList();
            }
            else if (RoleHelper.ListUserRoles(user.Id).FirstOrDefault() == "Submitter")
            {
                viewModel.Projects = ProjectHelper.ListUserProjects(user.Id);
                viewModel.Tickets = ProjectHelper.ListUserProjects(user.Id).SelectMany(p => p.Tickets).ToList();
            }
            else
            {
                viewModel.Projects = ProjectHelper.ListUserProjects(user.Id);
                viewModel.Tickets = ProjectHelper.ListUserProjects(user.Id).SelectMany(p => p.Tickets).ToList();
            }

            // load the dashboard viewmodel
            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}