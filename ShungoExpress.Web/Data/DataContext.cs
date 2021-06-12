using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShungoExpress.Web.Data.Entities;

namespace ShungoExpress.Web.Data
{
  public class DataContext : IdentityDbContext<User>
  {
    public DbSet<Motorized> Motorizeds { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Client> Clients { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }
  }
}