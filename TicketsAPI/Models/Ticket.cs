using System.ComponentModel.DataAnnotations;
using TicketsAPI.ModelValidations;

namespace TicketsAPI.Models
{
    public class Ticket
    {
        public int? TicketId { get; set; }

        [Required]
        public int? ProjectId { get; set; }

        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }

        [Ticket_EnsureDueDateForTicketOwner]
        [Ticket_EnsureDueDateInFuture]
        public DateTime? DueDate { get; set; }

        public DateTime? EnteredDate { get; set; }
    }
}
