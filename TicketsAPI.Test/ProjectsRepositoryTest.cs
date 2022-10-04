using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsAccess.Repository;
using TicketsAccess.Repository.APIClient;

namespace TicketsAPI.Test
{
    public class ProjectsRepositoryTest
    {

        private readonly WebApplicationFactory<Program> app;

        public ProjectsRepositoryTest()
        {
            app = new WebApplicationFactory<Program>()
               .WithWebHostBuilder(builder =>
                   {
                       builder.ConfigureServices(services =>
                       {
                           services.AddSingleton<IWebAPIExecuter>(sp =>
                                   new WebAPIExecuter(
                                       "http://localhost:5073/api",
                                       new HttpClient()));

                           services.AddTransient<IProjectRepository, ProjectRepository>();

                        
                       });
                   });


        }

        [Fact]
        public async void GetAllProjectsTest()
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var projectRepo = services.GetRequiredService<IProjectRepository>();

                var projects = await projectRepo.GetAllAsync();

                Assert.NotNull(projects);
            }
        }
    }
}
