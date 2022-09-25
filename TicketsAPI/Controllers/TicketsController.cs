using DataStore.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TicketsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly BugsContext _context;

        public TicketsController(BugsContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Tickets.AsNoTracking().ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var ticket = _context.Tickets.SingleOrDefault(p => p.Id == id);

            if (ticket is null) return NotFound();

            return Ok(ticket);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new {id = ticket.Id}, ticket);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id,[FromBody] Ticket ticket)
        {
            if (id == ticket.Id) return BadRequest();

            _context.Entry(ticket).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch
            {
                if(_context.Tickets.SingleOrDefault(t => t.Id == id) == null)
                {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var ticket = _context.Tickets.SingleOrDefault(t => t.Id == id);
            if(ticket is null) return NotFound();

            _context.Tickets.Remove(ticket);
            _context.SaveChanges();

            return Ok(ticket);
        }
    }
}
