using Core.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace TicketsAPI.Test
{
    public class ProjectsControllerTest
    {
        private readonly HttpClient _httpClient;

        public ProjectsControllerTest()
        {
            var webAppFactory = new WebApplicationFactory<Program>();

             _httpClient = webAppFactory.CreateDefaultClient();
        }

        [Fact]
        public async void GetAllProjects()
        {

            var response = await _httpClient.GetFromJsonAsync<List<Project>>("/api/projects");

            Assert.NotNull(response);
            var project = response?.FirstOrDefault();
            Assert.Equal(1, project?.Id);
        }

        [Fact]
        public async void GetProjectById()
        {
            var project = await _httpClient.GetFromJsonAsync<Project>("/api/projects/1");

            Assert.True(project != null);
            Assert.Equal(1, project?.Id);
            Assert.Contains("Project 1", project?.Name, StringComparison.OrdinalIgnoreCase);
        }


    }
}