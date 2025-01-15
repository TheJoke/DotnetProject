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
    [Bind("Id,ReclamationId,Technicien,DateIntervention,EstSousGarantie,CoutMainOeuvre, pieces")] Intervention intervention,
    [Bind("pieces1")] IList <PieceViewModel> pieces1) // Ensure the parameter name matches the form field prefix
        {
            // Print all POST parameters
            Debug.WriteLine("POST Parameters:");
            foreach (var key in HttpContext.Request.Form.Keys)
            {
                var value = HttpContext.Request.Form[key];
                Debug.WriteLine($"{key} = {value}");
            }
            // Log the incoming Pieces data
            if (pieces1 == null)
            {
                Debug.WriteLine("Pieces is null.");
            }
            else
            {
                Debug.WriteLine($"Pieces count: {pieces1.Count()}");
                foreach (var piece in pieces1)
                {
                    Debug.WriteLine($"Piece Id: {piece.Id}, Quantite: {piece.Quantite}");
                }
            }

            if (id != intervention.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Fetch the existing intervention from the database
                    var existingIntervention = await _context.Interventions
                        .Include(i => i.PiecesUtilisees) // Include the related pieces
                        .FirstOrDefaultAsync(i => i.Id == id);

                    if (existingIntervention == null)
                    {
                        return NotFound();
                    }

                    // Update the main intervention details
                    _context.Entry(existingIntervention).CurrentValues.SetValues(intervention);

                    // Handle pieces
                    if (pieces1 != null && pieces1.Any())
                    {
                        // Clear existing pieces
                        existingIntervention.PiecesUtilisees.Clear();

                        // Add new pieces from the submitted form
                        foreach (var piece in pieces1)
                        {
                            var existingPiece = await _context.Pieces.FindAsync(piece.Id);
                            if (existingPiece != null)
                            {
                                existingIntervention.PiecesUtilisees.Add(existingPiece);
                            }
                        }
                    }

                    // Save changes
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

            // Log validation errors (for debugging)
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    Debug.WriteLine(error.ErrorMessage);
                }
            }

            // Repopulate ViewData for dropdowns
            ViewData["ReclamationId"] = new SelectList(_context.Reclamations, "Id", "Id", intervention.ReclamationId);
            ViewData["Pieces"] = new SelectList(_context.Pieces, "Id", "Nom"); // Ensure this matches your Piece model

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
