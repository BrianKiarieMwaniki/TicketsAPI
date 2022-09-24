using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TicketsAPI.Filter
{
    public class Ticket_ValidateDatesActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var ticket = context.ActionArguments["ticket"] as Ticket;
            if (ticket != null &&
                !string.IsNullOrWhiteSpace(ticket.Owner))
            {
                bool isValid = true;

                if (ticket.ReportDate.HasValue == false)
                {
                    context.ModelState.AddModelError("ReportDate", "ReportDate is required.");
                    isValid = false;
                }

                if (ticket.ReportDate.HasValue &&
                    ticket.DueDate.HasValue &&
                    ticket.ReportDate > ticket.DueDate)
                {
                    context.ModelState.AddModelError("DueDate", "DueDate has to be later than the ReportDate.");
                    isValid = false;
                }

                if (!isValid)
                    context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
