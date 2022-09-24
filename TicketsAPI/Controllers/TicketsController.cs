using Microsoft.AspNetCore.Mvc;
using TicketsAPI.Filter;

namespace TicketsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Reading all the tickets.");
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok($"Reading ticket #{id}.");
        }

        [HttpPost]
        public IActionResult PostV1([FromBody] Ticket ticket)
        {
            return Ok(ticket);
        }

        [HttpPost]
        [Route("/api/v2/tickets")]
        [Ticket_ValidateDatesActionFilter]
        public IActionResult PostV2([FromBody] Ticket ticket)
        {
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
