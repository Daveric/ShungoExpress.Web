using Microsoft.EntityFrameworkCore;
using ShungoExpress.Web.Data.Entities;

namespace ShungoExpress.Web.Data
{
  public class DataContext : DbContext
  {
    public DbSet<Motorized> Motorizeds { get; set; }
    public DbSet<Order> Orders { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }
  }
}