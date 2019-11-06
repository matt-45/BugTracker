using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BugTracker.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Exchange.WebServices.Data;

namespace BugTracker.Controllers
{
    public class ProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private UserRoleHelper RoleHelper = new UserRoleHelper();
        private ProjectsHelper ProjectHelper = new ProjectsHelper();

        // GET: Projects
        public ActionResult Index()
        {
            return View(db.Projects.ToList());
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            ProjectDetailsViewModel viewModel = new ProjectDetailsViewModel();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userRole = RoleHelper.ListUserRoles(User.Identity.GetUserId()).FirstOrDefault();
            Project project = db.Projects.Find(id);
            viewModel.Project = project;
            viewModel.Tickets = project.Tickets.ToList();
            viewModel.Managers = project.GetManagersInProject();
            viewModel.Developers = project.GetDevelopersInProject();
            viewModel.Submitters = project.GetSubmittersInProject();
            viewModel.Users = ProjectHelper.UsersOnProject((int)id);

            if (userRole == "Manager")
            {
                var users = RoleHelper.UsersInRole("Developer").Concat(RoleHelper.UsersInRole("Submitter"));
                viewModel.AllUsers = users.ToList();
            }
            else
            {
                var users = RoleHelper.UsersInRole("Manager").Concat(RoleHelper.UsersInRole("Developer")).Concat(RoleHelper.UsersInRole("Submitter"));
                viewModel.AllUsers = users.ToList();
            }


            if (project == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult AddUserToProject(string[] userIds, int projectId)
        {
            var project = db.Projects.FirstOrDefault(p => p.Id == projectId);

            var allUsers = project.Users.ToList();

            if (userIds == null)
            {
                foreach (var user in allUsers)
                {
                    ProjectHelper.RemoveUserFromProject(user.Id, project.Id);
                }
            }
            else 
            {
                foreach (var user in allUsers)
                {
                    ProjectHelper.RemoveUserFromProject(user.Id, project.Id);
                }
                foreach (var userId in userIds)
                {
                    ProjectHelper.AddUserToProject(userId, project.Id);
                }
            }
            return RedirectToAction("Details", "Projects", new { id = projectId });
        }


        // GET: Projects/Create
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string ProjectTitle, string ProjectDescription)
        {
            if (ModelState.IsValid)
            {
                Project project = new Project();
                project.Name = ProjectTitle;
                project.Description = ProjectDescription;
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Details", "Projects", new { id = project.Id});
            }

            return RedirectToAction("Index", "Home");
        }

        // GET: Projects/Edit/5
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
