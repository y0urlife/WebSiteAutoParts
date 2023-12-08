using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebSiteAutoParts.Models;
using WebSiteAutoParts.Models.Data;
using WebSiteAutoParts.ViewModels.Spares;

namespace WebSiteAutoParts.Controllers
{
    public class SparesController : Controller
    {
        private readonly AppCtx _context;

        public SparesController(AppCtx context)
        {
            _context = context;
        }

        // GET: Spares
        public async Task<IActionResult> Index()
        {
            var appCtx = _context.Spares
                .OrderBy(f => f.Name);

            return _context.Spares != null ? 
                          View(await appCtx.ToListAsync()) :
                          Problem("Entity set 'AppCtx.Spares'  is null.");
        }

        // GET: Spares/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null || _context.Spares == null)
            {
                return NotFound();
            }

            var spare = await _context.Spares
                .FirstOrDefaultAsync(m => m.Id == id);
            if (spare == null)
            {
                return NotFound();
            }

            return View(spare);
        }

        // GET: Spares/Create
        public IActionResult Create()
        {
            ViewData["IdCategory"] = new SelectList(
                _context.Categories.OrderBy(o => o.CategoryName),
                "Id", "CategoryName");

            return View();
        }

        // POST: Spares/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSpareViewModel model)
        {

            if (_context.Spares
                .Include(i => i.Category)
                .Where(f => f.IdCategory == model.IdCategory &&
                    f.Name == model.Name &&
                    f.VendorCode == model.VendorCode)
                .FirstOrDefault() != null)
            {
                ModelState.AddModelError("", "Введеная запчасть уже существует");
            }

            if (ModelState.IsValid)
            {
                Spare spare = new()
                {
                    Name = model.Name,
                    Description = model.Description,
                    StockAvability = model.StockAvability,
                    VendorCode = model.VendorCode,
                    IdCategory = model.IdCategory
                };

                _context.Add(spare);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdCategory"] = new SelectList(
                _context.Categories.OrderBy(o=>o.CategoryName),
                "Id", "CategoryName", model.IdCategory);
            return View(model);
        }


        // GET: Spares/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spare = await _context.Spares.FindAsync(id);
            if (spare == null)
            {
                return NotFound();
            }
            EditSpareViewModel model = new()
            {
                Id = spare.Id,
                Name = spare.Name,
                Description = spare.Description,
                StockAvability = spare.StockAvability,
                VendorCode = spare.VendorCode,
                IdCategory = spare.IdCategory
            };

            ViewData["IdCategory"] = new SelectList(
                _context.Categories.OrderBy(o => o.CategoryName),
                "Id", "CategoryName", spare.IdCategory);
            return View(model);
        }
        // POST: Spares/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, EditSpareViewModel model)
        {
            Spare spare = await _context.Spares.FindAsync(id);

            if (_context.Spares
                .Where(f => f.IdCategory == model.IdCategory &&
                    f.Name == model.Name &&
                    f.VendorCode == model.VendorCode)
                .FirstOrDefault() != null)
            {
                ModelState.AddModelError("", "Эта запчасть уже имеется в наличии");
            }
            if (id != spare.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    spare.Name = model.Name;
                    spare.Description = model.Description;
                    spare.StockAvability = model.StockAvability;
                    spare.VendorCode = model.VendorCode;
                    spare.IdCategory = model.IdCategory;
                    _context.Update(spare);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpareExists(spare.Id))
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

            ViewData["IdCategory"] = new SelectList(
                _context.Categories,
                "Id", "CategoryName", spare.IdCategory);
            return View(model);
        }

        // GET: Spares/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null || _context.Spares == null)
            {
                return NotFound();
            }

            var spare = await _context.Spares
                .FirstOrDefaultAsync(m => m.Id == id);
            if (spare == null)
            {
                return NotFound();
            }

            return View(spare);
        }

        // POST: Spares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            if (_context.Spares == null)
            {
                return Problem("Entity set 'AppCtx.Spares'  is null.");
            }
            var spare = await _context.Spares.FindAsync(id);
            if (spare != null)
            {
                _context.Spares.Remove(spare);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpareExists(short id)
        {
          return (_context.Spares?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
