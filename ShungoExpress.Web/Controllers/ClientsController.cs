using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShungoExpress.Web.Data.Entities;
using ShungoExpress.Web.Helper;
using ShungoExpress.Web.Models;
using System.Threading.Tasks;

namespace ShungoExpress.Web.Controllers
{
  [Authorize]
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

    public IActionResult Create(string redirectUrl)
    {
      var model = new ClientViewModel
      {
        RedirectUrl = redirectUrl
      };
      return View(model);
    }

    // POST: Clients/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ClientViewModel model)
    {
      if (ModelState.IsValid)
      {
        var client = new User
        {
          FirstName = model.FirstName,
          LastName = model.LastName,
          AddressUrl = model.AddressUrl,
          Address = model.Address,
          PhoneNumber = model.PhoneNumber,
          Role = "Client",
          UserName = model.FirstName + model.LastName
        };
        await _clientHelper.AddUserAsync(client);
        await _clientHelper.AddUserToRoleAsync(client, "Client");
        if (!string.IsNullOrEmpty(model.RedirectUrl))
        {
          var redirect = model.RedirectUrl.Split('/');
          return RedirectToAction(redirect[1], redirect[0]);
        }
        else
          return RedirectToAction(nameof(Index));
      }

      return View(model);
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
    public async Task<IActionResult> Edit(string id, ClientViewModel model)
    {
      if (ModelState.IsValid)
      {
        var client = await _clientHelper.GetUserByIdAsync(id);
        if (client != null)
        {
          client.FirstName = model.FirstName;
          client.LastName = model.LastName;
          client.AddressUrl = model.AddressUrl;
          client.PhoneNumber = model.PhoneNumber;
          client.Address = model.Address;
          client.UserName = model.FirstName + model.LastName;
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

      try
      {
        await _clientHelper.DeleteUserAsync(client);
      }
      catch
      {
        //ignore
      }

      return RedirectToAction(nameof(Index));
    }
  }
}
