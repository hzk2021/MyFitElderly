using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EDP_Project.Models
{
    public class Ticket
    {
        public string Ticket_Reference_Id { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerName { get; set; }
        public string CustomerContact { get; set; }
        public string IssueType { get; set; }
        public string Problem { get; set; }

        [DataType(DataType.Date)]

        public DateTime DateCreated { get; set; }
        public string Status { get; set; }
    }
}
