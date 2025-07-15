using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClubMaster3.Data;
using ClubMaster3.Models;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace ClubMaster3.Controllers
{
    [AllowAnonymous]
    public class MatchesController : Controller
    {
        private readonly ClubMaster3Context _context;

        public MatchesController(ClubMaster3Context context)
        {
            _context = context;
        }

        // GET: Matches
        public async Task<IActionResult> Index()
        {
            var matches = _context.Matches
                .Include(m => m.TeamA)
                .Include(m => m.TeamB);
            return View(await matches.ToListAsync());
        }

        // GET: Matches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var match = await _context.Matches
                .Include(m => m.TeamA)
                .Include(m => m.TeamB)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (match == null) return NotFound();

            return View(match);
        }

        // GET: Matches/Create
        public IActionResult Create()
        {
            ViewData["TeamAId"] = new SelectList(_context.Team.ToList(), "Id", "Name");
            ViewData["TeamBId"] = new SelectList(_context.Team.ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeamAId,TeamBId,MatchDateTime,Location,ScoreA,ScoreB")] Match match)
        {
            if (match.MatchDateTime > DateTime.Now)
            {
                // Reset the score if the match is in the future
                match.ScoreA = null;
                match.ScoreB = null;
            }

            if (match.TeamAId == match.TeamBId)
            {
                ModelState.AddModelError("", "Team A and Team B cannot be the same.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(match);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // 👇 Re-populate dropdowns if there's a validation error
            ViewData["TeamAId"] = new SelectList(_context.Team, "Id", "Name", match.TeamAId);
            ViewData["TeamBId"] = new SelectList(_context.Team, "Id", "Name", match.TeamBId);
            return View(match);
        }



        // POST: Matches/Edit/5
        // GET: Matches/Edit/5 ✅ يعرض الفورم
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var match = await _context.Matches.FindAsync(id);
            if (match == null) return NotFound();

            ViewData["TeamAId"] = new SelectList(_context.Team, "Id", "Name", match.TeamAId);
            ViewData["TeamBId"] = new SelectList(_context.Team, "Id", "Name", match.TeamBId);

            return View(match);
        }

        // POST: Matches/Edit/5 ✅ يحفظ التعديلات
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TeamAId,TeamBId,MatchDateTime,Location,ScoreA,ScoreB")] Match match)
        {
            if (id != match.Id) return NotFound();

            if (match.TeamAId == match.TeamBId)
            {
                ModelState.AddModelError("", "Team A and Team B cannot be the same.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(match);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchExists(match.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["TeamAId"] = new SelectList(_context.Team, "Id", "Name", match.TeamAId);
            ViewData["TeamBId"] = new SelectList(_context.Team, "Id", "Name", match.TeamBId);
            return View(match);
        }


        // GET: Matches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var match = await _context.Matches
                .Include(m => m.TeamA)
                .Include(m => m.TeamB)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (match == null) return NotFound();

            return View(match);
        }

        // POST: Matches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var match = await _context.Matches.FindAsync(id);
            if (match != null)
            {
                _context.Matches.Remove(match);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool MatchExists(int id)
        {
            return _context.Matches.Any(e => e.Id == id);
        }
    }
}
