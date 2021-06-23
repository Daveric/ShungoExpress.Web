using Microsoft.EntityFrameworkCore;
using ShungoExpress.Web.Data.Entities;
using ShungoExpress.Web.Helper;
using System.Linq;

namespace ShungoExpress.Web.Data.Repositories
{
  public class OrderRepository : GenericRepository<Order>, IOrderRepository
  {
    private readonly DataContext _context;
    private readonly IUserHelper _userHelper;

    public OrderRepository(DataContext context, IUserHelper userHelper) : base(context)
    {
      _context = context;
      _userHelper = userHelper;
    }

    public IQueryable<Order> GetOrders()
    {
      return _context.Orders
        .Include(o => o.Client)
        .Include(o => o.Motorized)
        .OrderByDescending(o => o.OrderDate);
    }

    public Order GetOrderByIdAsync(int id)
    {
      return _context.Orders
        .Include(o => o.Client)
        .Include(o => o.Motorized)
        .AsNoTracking().FirstOrDefault(o => o.Id == id);
    }

  }
}
