using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class UserViewModel
    {
        public ApplicationUser User { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public UserViewModel()
        {
            User = new ApplicationUser();
            Projects = new HashSet<Project>();
            Tickets = new HashSet<Ticket>();
        }
    }
}