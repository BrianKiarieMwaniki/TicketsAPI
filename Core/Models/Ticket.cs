using Core.ValidationAttributes.TicketValidation;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class Ticket
    {
        public int? Id { get; set; }
        [Required]
        public int? ProjectId { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        public string Description { get; set; }
        [StringLength(50)]
        public string Owner { get; set; }
        [EnsureDueDatePresent]
        [EnsureFutureDueDateOnCreation]
        [EnsureDueDateAfterReportDate]
        public DateTime? DueDate { get; set; }
        [EnsureReportDatePresent]
        public DateTime? ReportDate { get; set; }
        public Project Project { get; set; }

        public bool ValidateDescription()
        {
            return !string.IsNullOrWhiteSpace(Description);
        }

        /// <summary>
        /// When creating a ticket, if due date is entered, it has to be in the future.
        /// </summary>        
        public bool ValidateFutureDueDate()
        {
            if (Id.HasValue) return true;

            if(!DueDate.HasValue) return true;

            return (DueDate.Value > DateTime.Now);
        }

        /// <summary>
        /// When owner is assigned, the report date has to be present 
        /// </summary>        
        public bool ValidateReportDatePresence()
        {
            if (string.IsNullOrWhiteSpace(Owner)) return true;

            return ReportDate.HasValue;
        }

        /// <summary>
        /// When owner is assigned, the due date has to be present
        /// </summary>  
        public bool ValidateDueDatePresence()
        {
            if (string.IsNullOrWhiteSpace(Owner)) return true;

            return DueDate.HasValue;
        }

        /// <summary>
        /// When due date and report date are present, due date has to be later or equal to
        /// </summary>
        public bool ValidateDueDateAfterReportDate()
        {
            if(!DueDate.HasValue || !ReportDate.HasValue ) return true;

            return DueDate.Value.Date >= ReportDate.Value.Date;
        }
    }
}
