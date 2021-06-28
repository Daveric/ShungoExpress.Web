using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShungoExpress.Web.Data.Entities;
using ShungoExpress.Web.Data.Repositories;
using System.Threading.Tasks;

namespace ShungoExpress.Web.Controllers
{
  [Authorize]
  public class MotorizedsController : Controller
  {
    private readonly IMotorizedRepository _motorizedRepository;

    public MotorizedsController(IMotorizedRepository motorizedRepository)
    {
      _motorizedRepository = motorizedRepository;
    }

    // GET: Motorizeds
    public IActionResult Index()
    {
      return View(_motorizedRepository.GetAll());
    }

    // GET: Motorizeds/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var motorized = await _motorizedRepository.GetByIdAsync((int)id);
      if (motorized == null)
      {
        return NotFound();
      }

      return View(motorized);
    }

    // GET: Motorizeds/Create
    public IActionResult Create(bool fromOrder)
    {
      return View();
    }

    // POST: Motorizeds/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Motorized motorized)
    {
      if (ModelState.IsValid)
      {
        await _motorizedRepository.CreateAsync(motorized);
        return RedirectToAction(nameof(Index));
      }
      return View(motorized);
    }

    // GET: Motorizeds/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var motorized = await _motorizedRepository.GetByIdAsync((int)id);
      if (motorized == null)
      {
        return NotFound();
      }
      return View(motorized);
    }

    // POST: Motorizeds/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Motorized motorized)
    {
      if (id != motorized.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          await _motorizedRepository.UpdateAsync(motorized);
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!await _motorizedRepository.ExistAsync(motorized.Id))
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
      return View(motorized);
    }

    // GET: Motorizeds/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var motorized = await _motorizedRepository.GetByIdAsync((int)id);
      if (motorized == null)
      {
        return NotFound();
      }

      return View(motorized);
    }

    // POST: Motorizeds/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {

      var motorized = await _motorizedRepository.GetByIdAsync(id);
      try
      {
        await _motorizedRepository.DeleteAsync(motorized);
      }
      catch
      {
        // ignored
      }

      return RedirectToAction(nameof(Index));
    }
  }
}
