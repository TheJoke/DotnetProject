using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServiceApresVente.Models;
using ServiceApresVenteApp.Models;
using ServiceApresVenteApp.ViewModels;

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
        public async Task<IActionResult> Create([Bind("Id,ReclamationId,Technicien,DateIntervention,EstSousGarantie,CoutMainOeuvre")] Intervention intervention)
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
            var pieces = _context.Pieces.ToList();
            ViewData["Pieces"] = pieces;
            ViewData["ReclamationId"] = new SelectList(_context.Reclamations, "Id", "Id", intervention.ReclamationId);
            return View(intervention);
        }

        // POST: Interventions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
    int id,
    [Bind("Id,ReclamationId,Technicien,DateIntervention,EstSousGarantie,CoutMainOeuvre")] Intervention intervention,
    List<PieceViewModel> pieces1)
        {
            if (id != intervention.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingIntervention = await _context.Interventions
                        .Include(i => i.PiecesUtilisees)
                        .FirstOrDefaultAsync(i => i.Id == id);

                    if (existingIntervention == null)
                    {
                        return NotFound();
                    }

                    // Update basic intervention properties
                    _context.Entry(existingIntervention).CurrentValues.SetValues(intervention);

                    // Remove existing intervention pieces
                    var existingInterventionPieces = await _context.Set<IntervensionPiece>()
                        .Where(ip => ip.InterventionId == id)
                        .ToListAsync();
                    _context.Set<IntervensionPiece>().RemoveRange(existingInterventionPieces);

                    // Add new intervention pieces with quantities
                    if (pieces1 != null && pieces1.Any())
                    {
                        foreach (var piece in pieces1)
                        {
                            var interventionPiece = new IntervensionPiece
                            {
                                InterventionId = id,
                                PieceDeRechangeId = piece.Id,
                                Quantite = piece.Quantite
                            };
                            _context.Set<IntervensionPiece>().Add(interventionPiece);
                        }
                    }

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
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
            }

            ViewData["ReclamationId"] = new SelectList(_context.Reclamations, "Id", "Id", intervention.ReclamationId);
            ViewData["Pieces"] = new SelectList(_context.Pieces, "Id", "Nom");
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
