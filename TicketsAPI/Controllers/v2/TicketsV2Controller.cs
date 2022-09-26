using DataStore.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketsAPI.Filter.V2.TicketFilter;

namespace TicketsAPI.Controllers.v2
{
    [ApiVersion("2.0")]
    [Route("api/tickets")]
    [ApiController]
    public class TicketsV2Controller : ControllerBase
    {
        private readonly BugsContext _context;

        public TicketsV2Controller(BugsContext context)
        {
            _context = context;
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
