using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShungoExpress.Web.Data.Entities;
using ShungoExpress.Web.Helper;
using ShungoExpress.Web.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Language.CodeGeneration;

namespace ShungoExpress.Web.Controllers
{
  public class ClientsController : Controller
  {
    private readonly IUserHelper _clientHelper;

    public ClientsController(IUserHelper clientHelper)
    {
      _clientHelper = clientHelper;
    }

    //borrar boton de delete para los usuarios
    //TODO: agregar nickname 
    //agregar campo de direccion en index

    public async Task<IActionResult> Index()
    {
      var clients = await _clientHelper.GetAllClientsAsync();

      return View(clients);
    }

    public IActionResult Create()
    {
      return View();
    }

    // GET: Clients/Details/5
    public async Task<IActionResult> Details(string id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var client = await _clientHelper.GetUserByIdAsync(id);
      if (client == null)
      {
        return NotFound();
      }

      return View(client);
    }

    // POST: Clients/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(User client)
    {
      if (ModelState.IsValid)
      {
        client.Role = "Client";
        client.UserName = client.FirstName;
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

    public async Task<IActionResult> Edit(string id)
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
      var model = new ClientViewModel();
      model.FirstName = client.FirstName;
      model.LastName = client.LastName;
      model.AddressUrl = client.AddressUrl;
      model.PhoneNumber = client.PhoneNumber;
      model.Address = client.Address;
      return View(model);
    }


    [HttpPost]
    public async Task<IActionResult> Edit(ClientViewModel model)
    {
      if (ModelState.IsValid)
      {
        var client = await _clientHelper.GetUserByNameAsync(model.FirstName);
        if (client != null)
        {
          client.FirstName = model.FirstName;
          client.LastName = model.LastName;
          client.AddressUrl = model.AddressUrl;
          client.PhoneNumber = model.PhoneNumber;
          client.Address = model.Address;
          var identityResult = await _clientHelper.UpdateUserAsync(client);
        }
      }

      return RedirectToAction(nameof(Index));
    }
  }
}
