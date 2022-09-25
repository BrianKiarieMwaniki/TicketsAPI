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


            return Ok(ticket);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Ticket ticket)
        {
            return Ok(ticket);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok($"Deleting ticket #{id}.");
        }
    }
}
