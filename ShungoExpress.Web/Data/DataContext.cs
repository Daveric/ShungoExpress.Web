using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShungoExpress.Web.Data.Entities;

namespace ShungoExpress.Web.Data
{
  public class DataContext : IdentityDbContext<User>
  {
    public DbSet<Motorized> Motorizeds { get; set; }
    public DbSet<Order> Orders { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Order>()
        .Property(p => p.Cost)
        .HasColumnType("decimal(10,2)");

      var cascadeFKs = modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetForeignKeys())
        .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
      foreach (var fk in cascadeFKs)
      {
        fk.DeleteBehavior = DeleteBehavior.Restrict;
      }

      base.OnModelCreating(modelBuilder);
    }
  }
}