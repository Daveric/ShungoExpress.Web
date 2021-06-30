using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShungoExpress.Web.Data.Entities;
using ShungoExpress.Web.Data.Repositories;
using ShungoExpress.Web.Helper;
using ShungoExpress.Web.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ShungoExpress.Web.Controllers
{
  [Authorize]
  public class OrdersController : Controller
  {
    private readonly IMotorizedRepository _motorizedRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IUserHelper _userHelper;

    public OrdersController(IMotorizedRepository motorizedRepository, IOrderRepository orderRepository, IUserHelper userHelper)
    {
      _motorizedRepository = motorizedRepository;
      _orderRepository = orderRepository;
      _userHelper = userHelper;
    }

    //TODO: filtrar pedidos por motorizados y por cliente
    //agregar boton the filtrado por dia,semana y mes
    //letras mas grandes
    //agregar notas en pantalla home, para cargar siempre,
    //agregar boton de resumen de ventas del dia anterior en home

    // GET: Orders
    public IActionResult Index()
    {
      return View(_orderRepository.GetOrders());
    }

    // GET: Orders/Details/5
    public IActionResult Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var order = _orderRepository.GetOrderByIdAsync((int)id);
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
        var client = await _userHelper.GetUserByIdAsync(model.ClientId);
        var order = new Order()
        {
          OrderDate = DateTime.Now,
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
    public IActionResult Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var order = _orderRepository.GetOrderByIdAsync((int)id);
      if (order == null)
      {
        return NotFound();
      }

      var model = new OrderViewModel
      {
        OrderDate = order.OrderDate,
        ClientId = order.Client.Id,
        Clients = _userHelper.GetClients(),
        MotorizedId = order.Motorized.Id,
        Motorizeds = _motorizedRepository.GetMotorizeds(),
        Description = order.Description,
        DeliveryDate = order.DeliveryDate,
        Cost = order.Cost
      };
      return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, OrderViewModel model)
    {
      if (ModelState.IsValid)
      {
        var order = _orderRepository.GetOrderByIdAsync(id);
        var motorized = await _motorizedRepository.FindAsync(model.MotorizedId);
        var client = await _userHelper.GetUserByIdAsync(model.ClientId);
        order.OrderDate = model.OrderDate;
        order.DeliveryDate = model.DeliveryDate;
        order.Description = model.Description;
        order.Cost = model.Cost;
        order.Client = client;
        order.Motorized = motorized;
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
      return View(model);
    }

    public async Task<IActionResult> Delivered(int? id)
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

      order.DeliveryDate = DateTime.Now;
      await _orderRepository.UpdateAsync(order);
      return RedirectToAction("Index");
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var order = _orderRepository.GetOrderByIdAsync((int)id);
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

    [Authorize(Roles = "Admin")]
    public IActionResult ShowChart(bool perWeek)
    {
      var orders = new List<decimal>();
      var labels = new List<string>();
      string title;
      var listOrder = _orderRepository.GetOrders().OrderBy(o => o.OrderDate).ToList();
      if (perWeek)
      {
        title = "Ultima semana";
        labels.Add("Lunes");
        labels.Add("Martes");
        labels.Add("Miércoles");
        labels.Add("Jueves");
        labels.Add("Viernes");
        labels.Add("Sábado");
        labels.Add("Domingo");
        var currentDay = DateTime.Now.DayOfWeek;
        var daysTillCurrentDay = currentDay - DayOfWeek.Monday;
        var lastWeekStartDate = DateTime.Now.AddDays(-daysTillCurrentDay - 7);
        for (var i = 0; i < 7; i++)
        {
          orders.Add(listOrder.Where(o=>o.OrderDate.Date == lastWeekStartDate.Date).Sum(o=>o.Cost));
          lastWeekStartDate = lastWeekStartDate.AddDays(1);
        }
      }
      else
      {
        title = "Año corriente";
        labels.Add("Enero");
        labels.Add("Febrero");
        labels.Add("Marzo");
        labels.Add("Abril");
        labels.Add("Mayo");
        labels.Add("Junio");
        labels.Add("Julio");
        labels.Add("Agosto");
        labels.Add("Septiembre");
        labels.Add("Octubre");
        labels.Add("Noviembre");
        labels.Add("Diciembre");
        var currentYearOrders = listOrder.Where(o=>o.OrderDate.Year == DateTime.Now.Year).ToList();
        for (var i = 1; i < 13; i++)
        {
          orders.Add(currentYearOrders.Where(o => o.OrderDate.Month == i).Sum(o=>o.Cost));
        }
      }


      ViewBag.Labels = labels.ToList();
      ViewBag.Orders = orders.ToList();
      return View();
    }

  }
}
