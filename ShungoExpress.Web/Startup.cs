using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShungoExpress.Web.Data;
using ShungoExpress.Web.Data.Entities;
using ShungoExpress.Web.Data.Repositories;
using ShungoExpress.Web.Helper;

namespace ShungoExpress.Web
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllersWithViews();
      services.AddIdentity<User, IdentityRole>(cfg =>
        {
          //cfg.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider;
          cfg.SignIn.RequireConfirmedEmail = false;
          cfg.User.RequireUniqueEmail = false;
          cfg.Password.RequireDigit = true;
          cfg.Password.RequiredUniqueChars = 0;
          cfg.Password.RequireLowercase = true;
          cfg.Password.RequireNonAlphanumeric = false;
          cfg.Password.RequireUppercase = true;
          cfg.Password.RequiredLength = 6;
        })
        //.AddDefaultTokenProviders()
        .AddEntityFrameworkStores<DataContext>();

      services.AddDbContext<DataContext>(options =>
      {
        options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection"));
      });

      services.AddTransient<SeedDb>();
      services.AddScoped<IGenericRepository<Motorized>, GenericRepository<Motorized>>();
      services.AddScoped<IGenericRepository<Order>, GenericRepository<Order>>();
      services.AddScoped<IUserHelper, UserHelper>();

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
      }
      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseRouting();
      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}
