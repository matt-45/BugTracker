using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class TicketDetailsViewModel
    {
        public Ticket ticket { get; set; }
        public ICollection<TicketStatus> TicketStatuses { get; set; }
        public ICollection<TicketPriority> TicketPriorities { get; set; }
        public ICollection<TicketType> TicketTypes { get; set; }

        public TicketDetailsViewModel()
        {
            TicketStatuses = new HashSet<TicketStatus>();
            TicketPriorities = new HashSet<TicketPriority>();
            TicketTypes = new HashSet<TicketType>();
        }
    }
}