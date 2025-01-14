using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceApresVente.Models;
using ServiceApresVenteApp.Repositories;

namespace ServiceApresVenteApp.Controllers
{
    public class ReclamationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository userRepository;


        public ReclamationsController(ApplicationDbContext context, IUserRepository userRepository)
        {
            _context = context;
            this.userRepository = userRepository;
        }

        // GET: Reclamations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reclamations.ToListAsync());
        }

        // GET: Reclamations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reclamation = await _context.Reclamations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reclamation == null)
            {
                return NotFound();
            }

            return View(reclamation);
        }

        // GET: Reclamations/Create
        public IActionResult Create()
        {
            ViewData["ArticleId"] = new SelectList(_context.Articles.ToList(), "Id", "Id");
            

            return View();
        }

        // POST: Reclamations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,DateReclamation,ArticleId")] Reclamation reclamation)
        {
            if (ModelState.IsValid)
            {
                reclamation.Statut = StatutReclamation.EnCours;
                _context.Add(reclamation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reclamation);
        }
        public IActionResult CreateWithArticleId(int id)
        {
            ViewData["ArticleId"] = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateWithArticleId([Bind("Id,Description,DateReclamation,ArticleId")] Reclamation reclamation)
        {
            if (ModelState.IsValid)
            {
                reclamation.Statut = StatutReclamation.EnCours;
                _context.Add(reclamation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reclamation);
        }

        // GET: Reclamations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["ArticleId"] = new SelectList(_context.Articles.ToList(), "Id", "Id");

            var reclamation = await _context.Reclamations.FindAsync(id);
            if (reclamation == null)
            {
                return NotFound();
            }
            return View(reclamation);
        }

        // POST: Reclamations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,DateReclamation,ArticleId")] Reclamation reclamation)
        {
            if (id != reclamation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reclamation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReclamationExists(reclamation.Id))
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
            return View(reclamation);
        }

        // GET: Reclamations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reclamation = await _context.Reclamations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reclamation == null)
            {
                return NotFound();
            }

            return View(reclamation);
        }

        // POST: Reclamations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reclamation = await _context.Reclamations.FindAsync(id);
            if (reclamation != null)
            {
                _context.Reclamations.Remove(reclamation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReclamationExists(int id)
        {
            return _context.Reclamations.Any(e => e.Id == id);
        }
        [Authorize(Roles ="Responsable")]
        // GET: 
        public async Task<IActionResult> EditForResponsable(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.StatusReclamation = Enum.GetValues(typeof(StatutReclamation))
                                    .Cast<StatutReclamation>()
                                    .Select(sr => new SelectListItem
                                    {
                                        Text = sr.ToString(), // Display text in the dropdown
                                        Value = ((int)sr).ToString() // Corresponding value for the option
                                    })
                                    .ToList();
            var reclamation = await _context.Reclamations.FindAsync(id);
            if (reclamation == null)
            {
                return NotFound();
            }
            return View(reclamation);
        }

        // POST: Reclamations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Responsable")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditForResponsable(int id, [Bind("Id,Description,Statut,DateReclamation,ArticleId")] Reclamation reclamation)
        {
            var existingTask = _context.Reclamations.Find(id);
            if (id != reclamation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    existingTask.Statut = reclamation.Statut; 

                    _context.Update(existingTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReclamationExists(reclamation.Id))
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
            return View(reclamation);
        }
    }
}
