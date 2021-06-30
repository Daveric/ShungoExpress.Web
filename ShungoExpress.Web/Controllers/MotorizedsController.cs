using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShungoExpress.Web.Data.Entities;
using ShungoExpress.Web.Data.Repositories;
using System.Threading.Tasks;
using ShungoExpress.Web.Models;

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
    public IActionResult Create(string redirectUrl)
    {
      var model = new MotorizedViewModel
      {
        RedirectUrl = redirectUrl
      };
      return View(model);
    }

    // POST: Motorizeds/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(MotorizedViewModel model)
    {
      if (ModelState.IsValid)
      {
        var motorized = new Motorized
        {
          FirstName = model.FirstName,
          LastName = model.LastName,
          Plate = model.Plate,
          Description = model.Description,
          IdNumber = model.IdNumber,
          Available = model.Available,
          DisplayName = model.DisplayName
        };
        await _motorizedRepository.CreateAsync(motorized);
        if (string.IsNullOrEmpty(model.RedirectUrl)) return RedirectToAction(nameof(Index));
        var redirect = model.RedirectUrl.Split('/');
        return RedirectToAction(redirect[1], redirect[0]);
      }
      return View(model);
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
