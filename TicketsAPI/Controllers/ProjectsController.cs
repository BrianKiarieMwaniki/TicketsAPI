using DataStore.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TicketsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly BugsContext _context;

        public ProjectsController(BugsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _context.Projects.AsNoTracking().ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var project = await _context.Projects.SingleOrDefaultAsync(p => p.Id == id);
            if (project == null) return NotFound();

            return Ok(project);
        }

        [HttpGet]
        [Route("api/projects/{id}/tickets")]
        public async Task<IActionResult> GetProjectTickets(int projectId)
        {
            var tickets = await _context.Tickets.Where(t => t.ProjectId == projectId).ToListAsync();

            //if (!tickets.Any()) return NotFound();
            if (tickets is null && tickets.Count < 0) return NotFound();

            return Ok(tickets);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Project project)
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = project.Id }, project);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Project project)
        {
            if (id != project.Id) return BadRequest();

            _context.Entry(project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch 
            {
                if(await _context.Projects.SingleOrDefaultAsync(p =>p.Id == id) == null)
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var project = await _context.Projects.SingleOrDefaultAsync(p => p.Id == id);
            if (project is null) return NotFound();

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return Ok(project);
        }
    }
}
