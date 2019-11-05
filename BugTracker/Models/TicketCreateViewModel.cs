using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BugTracker.Models
{
    public class TicketCreateViewModel
    {
        public ApplicationUser User { get; set; }
        public Project? Project { get; set; }
        public ICollection<Project> Projects { get; set; }


        public TicketCreateViewModel()
        {
            Projects = new HashSet<Project>();
        }

    }

}