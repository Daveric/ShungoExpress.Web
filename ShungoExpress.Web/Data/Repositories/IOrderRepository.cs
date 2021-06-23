using ShungoExpress.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace ShungoExpress.Web.Data.Repositories
{
  public interface IOrderRepository : IGenericRepository<Order>
  {
    IQueryable<Order> GetOrders();

    Order GetOrderByIdAsync(int id);
  }
}
