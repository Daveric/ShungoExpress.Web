using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShungoExpress.Web.Data.Entities;
using ShungoExpress.Web.Data.Repositories;
using ShungoExpress.Web.Helper;
using ShungoExpress.Web.Models;
using System.Threading.Tasks;

namespace ShungoExpress.Web.Controllers
{
  public class OrdersController : Controller
  {
    private readonly IMotorizedRepository _motorizedRepository;
    private readonly IGenericRepository<Order> _orderRepository;
    private readonly IUserHelper _userHelper;

    public OrdersController(IMotorizedRepository motorizedRepository, IGenericRepository<Order> orderRepository, IUserHelper userHelper)
    {
      _motorizedRepository = motorizedRepository;
      _orderRepository = orderRepository;
      _userHelper = userHelper;
    }

    // GET: Orders
    public IActionResult Index()
    {
      return View(_orderRepository.GetAll());
    }

    // GET: Orders/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var order = await _orderRepository.GetByIdAsync((int)id);
      if (order == null)
      {
        return NotFound();
      }

      return View(order);
    }

    // GET: Orders/Create
    public IActionResult Create()
    {
      var model = new OrderViewModel()
      {
        OrderDate = DateTime.UtcNow,
        Clients = _userHelper.GetClients(),
        Motorizeds = _motorizedRepository.GetMotorizeds()
      };
      return View(model);
    }

    // POST: Orders/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(OrderViewModel model)
    {
      if (ModelState.IsValid)
      {
        var motorized = await _motorizedRepository.FindAsync(model.MotorizedId);
        var client = await _userHelper.GetUserByIdAsync(model.ClientName);
        var order = new Order()
        {
          OrderDate = model.OrderDate,
          DeliveryDate = model.DeliveryDate,
          Description = model.Description,
          Cost = model.Cost,
          Client = client,
          Motorized = motorized
        };
        await _orderRepository.CreateAsync(order);
        return RedirectToAction(nameof(Index));
      }
      return View(model);
    }

    // GET: Orders/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var order = await _orderRepository.GetByIdAsync((int)id);
      if (order == null)
      {
        return NotFound();
      }
      return View(order);
    }

    // POST: Orders/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Order order)
    {
      if (id != order.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          await _orderRepository.UpdateAsync(order);
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!await _orderRepository.ExistAsync(order.Id))
          {
            return NotFound();
          }
          else
          {
            throw;
          }
        }
        return RedirectToAction(nameof(Index));
      }
      return View(order);
    }

    // GET: Orders/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var order = await _orderRepository.GetByIdAsync((int)id);
      if (order == null)
      {
        return NotFound();
      }

      return View(order);
    }

    // POST: Orders/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var order = await _orderRepository.GetByIdAsync(id);
      await _orderRepository.DeleteAsync(order);
      return RedirectToAction(nameof(Index));
    }
  }
}
