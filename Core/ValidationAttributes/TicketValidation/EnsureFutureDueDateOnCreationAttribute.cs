using Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Core.ValidationAttributes.TicketValidation
{
    public class EnsureFutureDueDateOnCreationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var ticket = validationContext.ObjectInstance as Ticket;

            if (!ticket.ValidateFutureDueDate())
                return new ValidationResult("Due date has to be in the future");

            return ValidationResult.Success;
        }
    }
}
