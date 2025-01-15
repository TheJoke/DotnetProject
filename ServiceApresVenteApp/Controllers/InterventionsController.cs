using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceApresVente.Models;

namespace ServiceApresVenteApp.Controllers
{
    public class InterventionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InterventionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Interventions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Interventions.Include(i => i.Reclamation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Interventions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intervention = await _context.Interventions
                .Include(i => i.Reclamation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (intervention == null)
            {
                return NotFound();
            }

            return View(intervention);
        }

        // GET: Interventions/Create
        public IActionResult Create()
        {
            ViewData["ReclamationId"] = new SelectList(_context.Reclamations, "Id", "Id");
            return View();
        }

        // POST: Interventions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ReclamationId,Technicien,DateIntervention,EstSousGarantie,CoutMainOeuvre,CoutTotal")] Intervention intervention)
        {
            if (ModelState.IsValid)
            {
                _context.Add(intervention);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReclamationId"] = new SelectList(_context.Reclamations, "Id", "Id", intervention.ReclamationId);
            return View(intervention);
        }

        // GET: Interventions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intervention = await _context.Interventions.FindAsync(id);
            if (intervention == null)
            {
                return NotFound();
            }
            ViewData["ReclamationId"] = new SelectList(_context.Reclamations, "Id", "Id", intervention.ReclamationId);
            return View(intervention);
        }

        // POST: Interventions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ReclamationId,Technicien,DateIntervention,EstSousGarantie,CoutMainOeuvre,CoutTotal")] Intervention intervention)
        {
            if (id != intervention.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(intervention);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InterventionExists(intervention.Id))
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
            ViewData["ReclamationId"] = new SelectList(_context.Reclamations, "Id", "Id", intervention.ReclamationId);
            return View(intervention);
        }

        // GET: Interventions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intervention = await _context.Interventions
                .Include(i => i.Reclamation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (intervention == null)
            {
                return NotFound();
            }

            return View(intervention);
        }

        // POST: Interventions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var intervention = await _context.Interventions.FindAsync(id);
            if (intervention != null)
            {
                _context.Interventions.Remove(intervention);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InterventionExists(int id)
        {
            return _context.Interventions.Any(e => e.Id == id);
        }
    }
}
