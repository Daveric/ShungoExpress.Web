using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShungoExpress.Web.Data.Entities;
using ShungoExpress.Web.Data.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShungoExpress.Web.Controllers
{
  public class OrdersController : Controller
  {
    private readonly IGenericRepository<Order> _orderRepository;

    public OrdersController(IGenericRepository<Order> orderRepository)
    {
      _orderRepository = orderRepository;
    }

    // GET: Orders
    public IActionResult Index()
    {
      return View( _orderRepository.GetAll());
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
      return View();
    }

    // POST: Orders/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Order order)
    {
      if (ModelState.IsValid)
      {
        await _orderRepository.CreateAsync(order);
        return RedirectToAction(nameof(Index));
      }
      return View(order);
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
