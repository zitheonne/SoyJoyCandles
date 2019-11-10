using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoyJoyCandles.Models;

namespace SoyJoyCandles.Views.Candles
{
    public class SoyJoyCandlesShopsController : Controller
    {
        private readonly SoyJoyCandlesContext _context;

        public SoyJoyCandlesShopsController(SoyJoyCandlesContext context)
        {
            _context = context;
        }

        // GET: SoyJoyCandlesShops
        public async Task<IActionResult> Index()
        {
            return View(await _context.SoyJoyCandlesShop.ToListAsync());
        }

        // GET: SoyJoyCandlesShops/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soyJoyCandlesShop = await _context.SoyJoyCandlesShop
                .FirstOrDefaultAsync(m => m.ID == id);
            if (soyJoyCandlesShop == null)
            {
                return NotFound();
            }

            return View(soyJoyCandlesShop);
        }

        // GET: SoyJoyCandlesShops/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SoyJoyCandlesShops/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FlowerName,FlowerProducedDate,Type,price")] SoyJoyCandlesShop soyJoyCandlesShop)
        {
            if (ModelState.IsValid)
            {
                _context.Add(soyJoyCandlesShop);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(soyJoyCandlesShop);
        }

        // GET: SoyJoyCandlesShops/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soyJoyCandlesShop = await _context.SoyJoyCandlesShop.FindAsync(id);
            if (soyJoyCandlesShop == null)
            {
                return NotFound();
            }
            return View(soyJoyCandlesShop);
        }

        // POST: SoyJoyCandlesShops/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FlowerName,FlowerProducedDate,Type,price")] SoyJoyCandlesShop soyJoyCandlesShop)
        {
            if (id != soyJoyCandlesShop.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(soyJoyCandlesShop);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SoyJoyCandlesShopExists(soyJoyCandlesShop.ID))
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
            return View(soyJoyCandlesShop);
        }

        // GET: SoyJoyCandlesShops/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soyJoyCandlesShop = await _context.SoyJoyCandlesShop
                .FirstOrDefaultAsync(m => m.ID == id);
            if (soyJoyCandlesShop == null)
            {
                return NotFound();
            }

            return View(soyJoyCandlesShop);
        }

        // POST: SoyJoyCandlesShops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var soyJoyCandlesShop = await _context.SoyJoyCandlesShop.FindAsync(id);
            _context.SoyJoyCandlesShop.Remove(soyJoyCandlesShop);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SoyJoyCandlesShopExists(int id)
        {
            return _context.SoyJoyCandlesShop.Any(e => e.ID == id);
        }
    }
}
