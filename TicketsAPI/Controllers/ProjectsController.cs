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
        public IActionResult Get()
        {
            return Ok(_context.Projects.AsNoTracking().ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);
            if (project == null) return NotFound();

            return Ok(project);
        }

        [HttpGet]
        [Route("api/projects/{id}/tickets")]
        public IActionResult GetProjectTickets(int projectId)
        {
            var tickets = _context.Tickets.Where(t => t.ProjectId == projectId).AsNoTracking().ToList();

            if (!tickets.Any()) return NotFound();

            return Ok(tickets);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Project project)
        {
            _context.Projects.Add(project);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = project.Id }, project);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Project project)
        {
            if (id != project.Id) return BadRequest();

            _context.Entry(project).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch 
            {
                if(_context.Projects.SingleOrDefault(p =>p.Id == id) == null)
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
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);
            if (project is null) return NotFound();

            _context.Projects.Remove(project);
            _context.SaveChanges();

            return Ok(project);
        }
    }
}
