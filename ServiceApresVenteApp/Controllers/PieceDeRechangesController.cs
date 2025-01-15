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
    public class PieceDeRechangesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PieceDeRechangesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PieceDeRechanges
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pieces.ToListAsync());
        }

        // GET: PieceDeRechanges/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pieceDeRechange = await _context.Pieces
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pieceDeRechange == null)
            {
                return NotFound();
            }

            return View(pieceDeRechange);
        }

        // GET: PieceDeRechanges/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PieceDeRechanges/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Reference,Prix,Stock")] PieceDeRechange pieceDeRechange)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pieceDeRechange);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pieceDeRechange);
        }

        // GET: PieceDeRechanges/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pieceDeRechange = await _context.Pieces.FindAsync(id);
            if (pieceDeRechange == null)
            {
                return NotFound();
            }
            return View(pieceDeRechange);
        }

        // POST: PieceDeRechanges/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Reference,Prix,Stock")] PieceDeRechange pieceDeRechange)
        {
            if (id != pieceDeRechange.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pieceDeRechange);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PieceDeRechangeExists(pieceDeRechange.Id))
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
            return View(pieceDeRechange);
        }

        // GET: PieceDeRechanges/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pieceDeRechange = await _context.Pieces
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pieceDeRechange == null)
            {
                return NotFound();
            }

            return View(pieceDeRechange);
        }

        // POST: PieceDeRechanges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pieceDeRechange = await _context.Pieces.FindAsync(id);
            if (pieceDeRechange != null)
            {
                _context.Pieces.Remove(pieceDeRechange);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PieceDeRechangeExists(int id)
        {
            return _context.Pieces.Any(e => e.Id == id);
        }
    }
}
