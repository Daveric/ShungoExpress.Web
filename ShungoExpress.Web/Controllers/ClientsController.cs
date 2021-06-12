using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShungoExpress.Web.Helper;
using System.Threading.Tasks;
using ShungoExpress.Web.Data.Entities;

namespace ShungoExpress.Web.Controllers
{
  public class ClientsController : Controller
  {
    private readonly IUserHelper _clientHelper;

    public ClientsController(IUserHelper clientHelper)
    {
      _clientHelper = clientHelper;
    }

    public async Task<IActionResult> Index()
    {
      var clients = await _clientHelper.GetAllClientsAsync();

      return View(clients);
    }

    public IActionResult Create()
    {
      return View();
    }

    // POST: Motorizeds/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(User client)
    {
      if (ModelState.IsValid)
      {
        client.Role = "Client";
        await _clientHelper.AddUserAsync(client);
        await _clientHelper.AddUserToRoleAsync(client, "Client");
        return RedirectToAction(nameof(Index));
      }
      return View(client);
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteClient(string id)
    {
      if (string.IsNullOrEmpty(id))
      {
        return NotFound();
      }

      var client = await _clientHelper.GetUserByIdAsync(id);
      if (client == null)
      {
        return NotFound();
      }

      await _clientHelper.RemoveUserFromRoleAsync(client, "Client");
      await _clientHelper.DeleteUserAsync(client);
      return RedirectToAction(nameof(Index));
    }
  }
}
