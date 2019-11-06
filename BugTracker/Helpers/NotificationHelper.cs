using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Helpers
{
    public class NotificationHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void SendTicketNotification(Ticket oldTicket, Ticket newTicket)
        {
            var ticketHasBeenAssigned = oldTicket.AssignedToUserId == null && newTicket.AssignedToUserId != null;
            var ticketHasBeenUnassigned = oldTicket.AssignedToUserId != null && newTicket.AssignedToUser == null;
            var ticketHasBeenReassigned = oldTicket.AssignedToUserId != null && newTicket.AssignedToUserId != null && oldTicket.AssignedToUserId != newTicket.AssignedToUserId;

            if (ticketHasBeenAssigned)
            {
                var notificationMessage = $"You have been assigned to a new ticket named {newTicket.Title}, on project named {newTicket.Project.Name}. It has a {newTicket.TicketPriority.Name} priority.";
                SendNotificationTo(newTicket.AssignedToUserId, notificationMessage, newTicket.Id);
            }
            else if (ticketHasBeenUnassigned)
            {
                var notificationMessage = $"You have been unassigned to a new ticket named {newTicket.Title}, on project named {newTicket.Project.Name}.";
                SendNotificationTo(oldTicket.AssignedToUserId, notificationMessage, newTicket.Id);
            }
            else if (ticketHasBeenReassigned)
            {
                var notificationMessage1 = $"You have been assigned to a new ticket named {newTicket.Title}, on project named {newTicket.Project.Name}. It has a {newTicket.TicketPriority.Name} priority.";
                SendNotificationTo(newTicket.AssignedToUserId, notificationMessage1, newTicket.Id);
                var notificationMessage2 = $"You have been unassigned to a new ticket named {newTicket.Title}, on project named {newTicket.Project.Name}.";
                SendNotificationTo(oldTicket.AssignedToUserId, notificationMessage2, newTicket.Id);
            }

        }

        public void SendNotificationTo(string userId, string message, int? ticketId)
        {
            var notification = new TicketNotification
            {
                NotificationBody = message,
                UserId = userId,
                IsRead = false,
                TicketId = ticketId
            };
            db.TicketNotifications.Add(notification);
            db.SaveChanges();
        }
    }
}