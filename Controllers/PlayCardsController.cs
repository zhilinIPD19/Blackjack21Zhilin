using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Blackjack21Zhilin.Data;
using Blackjack21Zhilin.Models;

namespace Blackjack21Zhilin.Controllers
{
    public class PlayCardsController : Controller
    {
        private readonly Blackjack21ZhilinContext _context;

        public PlayCardsController(Blackjack21ZhilinContext context)
        {
            _context = context;
        }

        // GET: PlayCards
        public async Task<IActionResult> Index()
        {
            return View(await _context.PlayCard.ToListAsync());
        }

        // GET: PlayCards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playCard = await _context.PlayCard
                .FirstOrDefaultAsync(m => m.PlayCardId == id);
            if (playCard == null)
            {
                return NotFound();
            }

            return View(playCard);
        }

        // GET: PlayCards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PlayCards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlayCardId,Name,Value,PhotoPath,IsDistributed")] PlayCard playCard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(playCard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(playCard);
        }

        // GET: PlayCards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playCard = await _context.PlayCard.FindAsync(id);
            if (playCard == null)
            {
                return NotFound();
            }
            return View(playCard);
        }

        // POST: PlayCards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlayCardId,Name,Value,PhotoPath,IsDistributed")] PlayCard playCard)
        {
            if (id != playCard.PlayCardId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(playCard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayCardExists(playCard.PlayCardId))
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
            return View(playCard);
        }

        // GET: PlayCards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playCard = await _context.PlayCard
                .FirstOrDefaultAsync(m => m.PlayCardId == id);
            if (playCard == null)
            {
                return NotFound();
            }

            return View(playCard);
        }

        // POST: PlayCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var playCard = await _context.PlayCard.FindAsync(id);
            _context.PlayCard.Remove(playCard);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayCardExists(int id)
        {
            return _context.PlayCard.Any(e => e.PlayCardId == id);
        }
    }
}
