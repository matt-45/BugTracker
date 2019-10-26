using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BugTracker.Models
{
    public class DashboardViewModel
    {
        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<Ticket> Tickets{ get; set; }
        public DashboardViewModel()
        {
            ApplicationUsers = new HashSet<ApplicationUser>();
            Projects = new HashSet<Project>();
            Tickets = new HashSet<Ticket>();
        }
    }

}