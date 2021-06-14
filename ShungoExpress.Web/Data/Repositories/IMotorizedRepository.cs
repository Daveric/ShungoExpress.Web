using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShungoExpress.Web.Data.Entities;

namespace ShungoExpress.Web.Data.Repositories
{
  public interface IMotorizedRepository: IGenericRepository<Motorized>
  {
    IEnumerable<SelectListItem> GetMotorizeds();

    Task<Motorized> FindAsync(int id);
  }
}
