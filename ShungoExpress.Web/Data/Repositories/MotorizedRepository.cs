using Microsoft.AspNetCore.Mvc.Rendering;
using ShungoExpress.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShungoExpress.Web.Data.Repositories
{
  public class MotorizedRepository : GenericRepository<Motorized>, IMotorizedRepository
  {
    private readonly DataContext _context;

    public MotorizedRepository(DataContext context) : base(context)
    {
      _context = context;
    }

    public async Task<Motorized> FindAsync(int id)
    {
      return await _context.Motorizeds.FindAsync(id);
    }

    public IEnumerable<SelectListItem> GetMotorizeds()
    {
      var list = _context.Motorizeds.Select(m => new SelectListItem
      {
        Text = m.DisplayName,
        Value = m.Id.ToString()
      }).OrderBy(l => l.Text).ToList();

      list.Insert(0, new SelectListItem
      {
        Text = "(Selecciona un motorizado...)",
        Value = "0"
      });

      return list;
    }
  }
}
