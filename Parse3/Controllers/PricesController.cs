using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Parse3.Data;
using Parse3.Models;

namespace Parse3.Controllers
{
    public class PricesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PricesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Prices
        public IActionResult Index()
        {
            var moveDBs = _context.MoveDBs.ToList();
            var price = _context.Prices.ToList();
            var model = new ViewModel { Prices = price, MoveDBs = moveDBs };
            return View(model);
        }

        // GET: Prices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _context.Prices
                .FirstOrDefaultAsync(m => m.id == id);
            if (price == null)
            {
                return NotFound();
            }

            return View(price);
        }

        // GET: Prices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Prices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,coderab,name,cost")] Price price)
        {
            if (ModelState.IsValid)
            {
                _context.Add(price);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(price);
        }

        // GET: Prices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _context.Prices.FindAsync(id);
            if (price == null)
            {
                return NotFound();
            }
            return View(price);
        }

        // POST: Prices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,coderab,name,cost")] Price price)
        {
            if (id != price.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(price);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PriceExists(price.id))
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
            return View(price);
        }

        // GET: Prices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _context.Prices
                .FirstOrDefaultAsync(m => m.id == id);
            if (price == null)
            {
                return NotFound();
            }

            return View(price);
        }

        // POST: Prices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var price = await _context.Prices.FindAsync(id);
            _context.Prices.Remove(price);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PriceExists(int id)
        {
            return _context.Prices.Any(e => e.id == id);
        }
        // GET: MoveDBs/Delete/5
        public async Task<IActionResult> Delete1(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moveDB = await _context.MoveDBs
                .FirstOrDefaultAsync(m => m.id == id);
            if (moveDB == null)
            {
                return NotFound();
            }

            return View(moveDB);
        }

        // POST: MoveDBs/Delete/5
        [HttpPost, ActionName("Delete1")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed1(int id)
        {
            var moveDB = await _context.MoveDBs.FindAsync(id);
            _context.MoveDBs.Remove(moveDB);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MoveDBExists(int id)
        {
            return _context.MoveDBs.Any(e => e.id == id);
        }
        public async Task<IActionResult> Edit1(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moveDB = await _context.MoveDBs.FindAsync(id);
            if (moveDB == null)
            {
                return NotFound();
            }
            return View(moveDB);
        }

        // POST: MoveDBs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit1(int id, [Bind("id,coderem,coderab,name")] MoveDB moveDB)
        {
            if (id != moveDB.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(moveDB);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoveDBExists(moveDB.name))
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
            return View(moveDB);
        }

        // GET: MoveDBs/Create
        public IActionResult Create1()
        {
            return View();
        }

        // POST: MoveDBs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create1([Bind("id,coderem,coderab,name")] MoveDB moveDB)
        {
            if (ModelState.IsValid)
            {
                _context.Add(moveDB);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(moveDB);
        }
    }
}


