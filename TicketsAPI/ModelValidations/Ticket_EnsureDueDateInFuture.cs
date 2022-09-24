using System.ComponentModel.DataAnnotations;
using TicketsAPI.Models;

namespace TicketsAPI.ModelValidations
{
    public class Ticket_EnsureDueDateInFuture : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var ticket = validationContext.ObjectInstance as Ticket;

            // When creating ticket, ticket due date has to be in the future
            if (ticket != null && ticket.TicketId == null)
            {
                if (ticket.DueDate.HasValue && ticket.DueDate.Value < DateTime.Now)
                {
                    return new ValidationResult("Due date has to be in the future.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
