using BugTracker.Models;
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

        // GET: Projects
        public ActionResult Index(DashboardViewModel viewModel)
        {
            viewModel.Projects = db.Projects.ToList();
            viewModel.ApplicationUsers = db.Users.ToList();
            viewModel.Tickets = db.Tickets.ToList();
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