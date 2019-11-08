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

namespace BugTracker.Controllers
{
    public class TicketsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRoleHelper roleHelper = new UserRoleHelper();
        private HistoryHelper historyHelper = new HistoryHelper();
        private ProjectsHelper projectHelper = new ProjectsHelper();

        // GET: Tickets
        [Authorize]
        public ActionResult Index()
        {
            var tickets = db.Tickets.Include(t => t.AssignedToUser).Include(t => t.OwnerUser).Include(t => t.Project).Include(t => t.TicketPriority).Include(t => t.TicketStatus).Include(t => t.TicketType);
            return View(tickets.ToList());
        }

        // GET: Tickets/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            TicketDetailsViewModel viewModel = new TicketDetailsViewModel();
            viewModel.Ticket = ticket;
            viewModel.User = db.Users.Find(User.Identity.GetUserId());
            viewModel.UserRole = roleHelper.ListUserRoles(User.Identity.GetUserId()).FirstOrDefault();
            viewModel.TicketPriorities = db.Priorities.ToList();
            viewModel.TicketStatuses = db.Statuses.ToList();
            viewModel.TicketTypes = db.Types.ToList();

            return View(viewModel);
        }

        // GET: Tickets/Create
        [Authorize(Roles = "Submitter")]
        public ActionResult Create(int? projectId)
        {
            TicketCreateViewModel viewModel = new TicketCreateViewModel();
            Ticket ticket = new Ticket();
            if (projectId != null)
            {
                viewModel.Project = db.Projects.Find(projectId);
            }
            viewModel.User = db.Users.Find(User.Identity.GetUserId());
            viewModel.Projects = projectHelper.ListUserProjects(viewModel.User.Id);
            viewModel.Ticket = ticket;
            viewModel.Ticket.OwnerUser = viewModel.User;
            return View(viewModel);
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ticket ticket, Project project, string typeName, string priorityName, string userId)
        {
            var owner = db.Users.Find(userId);
            var defaultStatus = db.Statuses.FirstOrDefault(s => s.Name == "Unassigned");
            var type = db.Types.FirstOrDefault(t => t.Name == typeName);
            var priority = db.Priorities.FirstOrDefault(p => p.Name == priorityName);
            ticket.OwnerUser = owner;
            ticket.TicketStatus = defaultStatus;
            ticket.Created = DateTime.Now;
            ticket.ProjectId = project.Id;
            ticket.TicketPriority = priority;
            ticket.TicketType = type;
            db.Tickets.Add(ticket);
            db.SaveChanges();
            return RedirectToAction("Details", "Tickets", new { id = ticket.Id});

           // return View();
        }

        [Authorize(Roles = "Admin, Manager")]
        public ActionResult EditStatus(int id, int statusId)
        {
            var oldTicket = db.Tickets.AsNoTracking().FirstOrDefault(t => t.Id == id);
            var ticket = db.Tickets.Find(id);
            var status = db.Statuses.Find(statusId);
            ticket.Updated = DateTime.Now;
            ticket.TicketStatus = status;
            db.SaveChanges();
            var newTicket = db.Tickets.Find(id);

            historyHelper.RecordHistoricalChanges(oldTicket, newTicket);

            return RedirectToAction("Details", "Tickets", new { id });
        }
        public ActionResult EditPriority(int id, int priorityId)
        {
            var oldTicket = db.Tickets.AsNoTracking().FirstOrDefault(t => t.Id == id);
            var ticket = db.Tickets.Find(id);
            var priority = db.Priorities.Find(priorityId);
            ticket.Updated = DateTime.Now;
            ticket.TicketPriority = priority;
            db.SaveChanges();
            var newTicket = db.Tickets.Find(ticket.Id);

            historyHelper.RecordHistoricalChanges(oldTicket, newTicket);

            return RedirectToAction("Details", "Tickets", new { id });
        }
        public ActionResult EditType(int id, int typeId)
        {
            var oldTicket = db.Tickets.AsNoTracking().FirstOrDefault(t => t.Id == id);
            var ticket = db.Tickets.Find(id);
            var type = db.Types.Find(typeId);
            ticket.Updated = DateTime.Now;
            ticket.TicketType = type;
            db.SaveChanges();
            var newTicket = db.Tickets.Find(ticket.Id);

            historyHelper.RecordHistoricalChanges(oldTicket, newTicket);

            return RedirectToAction("Details", "Tickets", new { id });
        }


        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssignedToUserId = new SelectList(db.Users, "Id", "FirstName", ticket.AssignedToUserId);
            ViewBag.OwnerUserId = new SelectList(db.Users, "Id", "FirstName", ticket.OwnerUserId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketPriorityId = new SelectList(db.Priorities, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.Statuses, "Id", "Name", ticket.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.Types, "Id", "Name", ticket.TicketTypeId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProjectId,TicketTypeId,TicketPriorityId,TicketStatusId,OwnerUserId,AssignedToUserId,Title,Description,Created,Updated")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssignedToUserId = new SelectList(db.Users, "Id", "FirstName", ticket.AssignedToUserId);
            ViewBag.OwnerUserId = new SelectList(db.Users, "Id", "FirstName", ticket.OwnerUserId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketPriorityId = new SelectList(db.Priorities, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.Statuses, "Id", "Name", ticket.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.Types, "Id", "Name", ticket.TicketTypeId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
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
