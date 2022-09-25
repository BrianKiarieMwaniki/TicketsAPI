using DataStore.EF;

namespace TicketsAPI.Utils
{
    public static class Extensions
    {
        public static void CreateDb(this IHost host)
        {
            {
                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var context = services.GetRequiredService<BugsContext>();
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();
                }
            }

        }
    }
}
