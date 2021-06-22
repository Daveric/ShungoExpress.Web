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

      var model = new ClientViewModel
      {
        FirstName = client.FirstName,
        LastName = client.LastName,
        AddressUrl = client.AddressUrl,
        PhoneNumber = client.PhoneNumber,
        Address = client.Address,
        UserName = client.FirstName + client.LastName
      };
      return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ClientViewModel model)
    {
      if (ModelState.IsValid)
      {
        var userName = model.FirstName + model.LastName;
        var client = await _clientHelper.GetUserByNameAsync(userName);
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

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(string id)
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
      await _clientHelper.DeleteUserAsync(client);
      return RedirectToAction(nameof(Index));
    }
  }
}
