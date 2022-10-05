using DataStore.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web.Http.OData;
using TicketsAPI.Filter;
using TicketsAPI.Filter.V2.TicketFilter;
using TicketsAPI.QueryFilters.TicketFilters;

namespace TicketsAPI.Controllers.v2
{
    [ApiVersion("2.0")]
    //[Route("api/v{v:apiVersion}/tickets")] version with route
    [Route("api/tickets")]
    [ApiController]
    //[APIKeyAuthFilter]
    [CustomTokenAuthFilter]
    public class TicketsV2Controller : ControllerBase
    {
        private readonly BugsContext _context;

        public TicketsV2Controller(BugsContext context)
        {
            _context = context;
        }

        [EnableQuery]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]TicketQueryFilter ticketQueryFilter)
        {
            IQueryable<Ticket> tickets = _context.Tickets;

            if(ticketQueryFilter != null)
            {
                if(ticketQueryFilter.Id.HasValue)
                {
                    tickets = tickets.Where(t => t.Id == ticketQueryFilter.Id);
                }
                if(!string.IsNullOrWhiteSpace(ticketQueryFilter.Title))
                {
                    tickets = tickets.Where(t => t.Title.Contains(ticketQueryFilter.Title, StringComparison.OrdinalIgnoreCase));
                }
                if (!string.IsNullOrWhiteSpace(ticketQueryFilter.Description))
                {
                    tickets = tickets.Where(t => t.Description.Contains(ticketQueryFilter.Description, StringComparison.OrdinalIgnoreCase));
                }
            }

            return Ok(await tickets.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ticket = await _context.Tickets.SingleOrDefaultAsync(p => p.Id == id);

            if (ticket is null) return NotFound();

            return Ok(ticket);
        }

        [HttpPost]
        [EnsureDescriptionPresentActionFilter]
        public async Task<IActionResult> Post([FromBody]Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = ticket.Id }, ticket);
        }

        [HttpPut("{id}")]
        [EnsureDescriptionPresentActionFilter]
        public async Task<IActionResult> Put(int id, [FromBody] Ticket ticket)
        {
            if (id != ticket.Id) return BadRequest();

            _context.Entry(ticket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                if (await _context.Tickets.SingleOrDefaultAsync(t => t.Id == id) == null)
                {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }
    }
}
