﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace BugTracker.Models
{
    public class HistoryHelper
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public void RecordHistoricalChanges(Ticket oldTicket, Ticket newTicket)
        {
            if (oldTicket.Title != newTicket.Title)
            {
                var newHistory = new TicketHistory {
                    Property = "Title",
                    TicketId = newTicket.Id,
                    NewValue = newTicket.Title,
                    OldValue = oldTicket.Title,
                    Changed = (DateTime)newTicket.Updated,
                    UserId = HttpContext.Current.User.Identity.GetUserId(),
                };
            }
            if (oldTicket.Description != newTicket.Description)
            {
                var newHistory = new TicketHistory
                {
                    Property = "Description",
                    TicketId = newTicket.Id,
                    NewValue = newTicket.Description,
                    OldValue = oldTicket.Description,
                    Changed = (DateTime)newTicket.Updated,
                    UserId = HttpContext.Current.User.Identity.GetUserId(),
                };
            }
            if (oldTicket.AssignedToUserId != newTicket.AssignedToUserId)
            {
                var newHistory = new TicketHistory
                {
                    Property = "Description",
                    TicketId = newTicket.Id,
                    NewValue = newTicket.AssignedToUser == null ? "Unassigned" : newTicket.AssignedToUser.FullName,
                    OldValue = oldTicket.AssignedToUser == null ? "Unassigned" : oldTicket.AssignedToUser.FullName,
                    Changed = (DateTime)newTicket.Updated,
                    UserId = HttpContext.Current.User.Identity.GetUserId(),
                };
                db.Histories.Add(newHistory);
            }
            if (oldTicket.TicketTypeId != newTicket.TicketTypeId)
            {
                var newHistory = new TicketHistory
                {
                    Property = "Ticket Type",
                    TicketId = newTicket.Id,
                    NewValue = newTicket.TicketType.Name,
                    OldValue = oldTicket.TicketType.Name,
                    Changed = (DateTime)newTicket.Updated,
                    UserId = HttpContext.Current.User.Identity.GetUserId(),
                };
            }
        }
            

    }
}