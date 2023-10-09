using Knowledge.Api.Database;
using Knowledge.Api.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;


namespace Knowledge.Api.Services
{
    public class UrlStatusBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly TimeSpan _interval = TimeSpan.FromSeconds(30); // Adjust the interval as needed


        public UrlStatusBackgroundService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    var links = await dbContext.Links.ToListAsync();
                    foreach (var link in links)
                    {
                        if (stoppingToken.IsCancellationRequested)
                            break;
                        try
                        {
                            var response = await new HttpClient().GetAsync(link.URL);
                            link.Status = (int)response.StatusCode;
                        }
                        catch (HttpRequestException)
                        {
                            // Handle HTTP request errors here
                            link.Status = 999; // You can use a custom code for errors
                        }

                        // Update the status in the database
                        dbContext.Update(link);
                        await dbContext.SaveChangesAsync();
                    }
                }
                // Wait for the specified interval before processing the next batch of URLs
                await Task.Delay(_interval, stoppingToken);
            }
            
        }
    }
}
