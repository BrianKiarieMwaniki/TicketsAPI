using Core.Models;

namespace TicketsAccess.Repository
{
    public interface IProjectRepository
    {
        Task<Project> AddProjectAsync(string uri, Project project);
        Task DeleteProject(int id);
        Task<IEnumerable<Project>> GetAllAsync();
        Task<Project> GetByIdAsync(int id);
        Task UpdateProjectAsync(int id, Project project);
    }
}