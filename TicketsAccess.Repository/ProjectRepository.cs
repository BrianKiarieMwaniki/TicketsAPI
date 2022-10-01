using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsAccess.Repository.APIClient;

namespace TicketsAccess.Repository
{
    public class ProjectRepository
    {
        private readonly IWebAPIExecuter _executer;

        public ProjectRepository(IWebAPIExecuter executer)
        {
            _executer = executer;
        }

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await _executer.InvokeGet<IEnumerable<Project>>("/projects");
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            return await _executer.InvokeGet<Project>($"/projects/{id}");
        }

        public async Task<Project> AddProjectAsync(string uri, Project project)
        {
            return await _executer.InvokePost<Project>("/projects", project);
        }

        public async Task UpdateProjectAsync(int id, Project project)
        {
            await _executer.InvokePut<Project>($"/projects/{id}", project);
        }

        public async Task DeleteProject(int id)
        {
            await _executer.InvokeDelete<Project>($"/projects/{id}");
        }
    }
}
