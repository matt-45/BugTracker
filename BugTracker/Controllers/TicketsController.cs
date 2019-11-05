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
            viewModel.ticket = ticket;
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
            if (projectId != null)
            {
                viewModel.Project = db.Projects.Find(projectId);
            }
            viewModel.User = db.Users.Find(User.Identity.GetUserId());
            viewModel.Projects = db.Projects.ToList();

            return View(viewModel);
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string ticketTypeName, string ticketPriorityName, string ticketTitle, string ticketDescription, int projectId, string userId)
        {
            
            var type = db.Types.FirstOrDefault(t => t.Name == ticketTypeName);
            var priority = db.Priorities.FirstOrDefault(p => p.Name == ticketPriorityName);
            var status = db.Statuses.FirstOrDefault(s => s.Name == "Unassigned");
            var owner = db.Users.Find(userId);
            var project = db.Projects.Find(projectId);
            if (ModelState.IsValid)
            {
                Ticket ticket = new Ticket();
                ticket.TicketType = type;
                ticket.TicketPriority = priority;
                ticket.TicketStatus = status;
                ticket.Title = ticketTitle;
                ticket.Description = ticketDescription;
                ticket.Created = DateTime.Now;
                ticket.OwnerUser = owner;
                ticket.Project = project;

                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Details", "Tickets", new { id = ticket.Id});
            }

            return View();
        }

        [Authorize(Roles = "Admin, Manager")]
        public ActionResult EditStatus(int id, int statusId)
        {
            var ticket = db.Tickets.FirstOrDefault(t => t.Id == id);
            var status = db.Statuses.FirstOrDefault(p => p.Id == statusId);

            ticket.TicketStatus = status;
            db.SaveChanges();

            return RedirectToAction("Details", "Tickets", new { id });
        }
        public ActionResult EditPriority(int id, int priorityId)
        {
            var ticket = db.Tickets.FirstOrDefault(t => t.Id == id);
            var priority = db.Priorities.FirstOrDefault(p => p.Id == priorityId);

            ticket.TicketPriority = priority;
            db.SaveChanges();

            return RedirectToAction("Details", "Tickets", new { id });
        }
        public ActionResult EditType(int id, int typeId)
        {
            var ticket = db.Tickets.FirstOrDefault(t => t.Id == id);
            var type = db.Types.FirstOrDefault(p => p.Id == typeId);

            ticket.TicketType = type;
            db.SaveChanges();

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
