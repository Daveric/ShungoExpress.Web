using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ShungoExpress.Web.Data;

namespace ShungoExpress.Web
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var host = CreateWebHostBuilder(args).Build();
      RunSeedDb(host);
      host.Run();
    }

    private static void RunSeedDb(IWebHost host)
    {
      var scopeFactory = host.Services.GetService<IServiceScopeFactory>();
      using var scope = scopeFactory.CreateScope();
      var seeder = scope.ServiceProvider.GetService<SeedDb>();
      seeder.SeedAsync().Wait();
    }


    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
      WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
  }
}
