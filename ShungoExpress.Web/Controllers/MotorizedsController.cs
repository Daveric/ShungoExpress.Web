using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShungoExpress.Web.Data;
using ShungoExpress.Web.Data.Entities;

namespace ShungoExpress.Web.Controllers
{
    public class MotorizedsController : Controller
    {
        private readonly DataContext _context;

        public MotorizedsController(DataContext context)
        {
            _context = context;
        }

        // GET: Motorizeds
        public async Task<IActionResult> Index()
        {
            return View(await _context.Motorizeds.ToListAsync());
        }

        // GET: Motorizeds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motorized = await _context.Motorizeds
                .FirstOrDefaultAsync(m => m.Id == id);
            if (motorized == null)
            {
                return NotFound();
            }

            return View(motorized);
        }

        // GET: Motorizeds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Motorizeds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Available")] Motorized motorized)
        {
            if (ModelState.IsValid)
            {
                _context.Add(motorized);
                await _context.SaveChangesAsync();
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

            var motorized = await _context.Motorizeds.FindAsync(id);
            if (motorized == null)
            {
                return NotFound();
            }
            return View(motorized);
        }

        // POST: Motorizeds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Available")] Motorized motorized)
        {
            if (id != motorized.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(motorized);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MotorizedExists(motorized.Id))
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

            var motorized = await _context.Motorizeds
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var motorized = await _context.Motorizeds.FindAsync(id);
            _context.Motorizeds.Remove(motorized);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MotorizedExists(int id)
        {
            return _context.Motorizeds.Any(e => e.Id == id);
        }
    }
}
