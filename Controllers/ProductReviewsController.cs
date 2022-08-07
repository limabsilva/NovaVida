using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NovaVida.DataContext;
using NovaVida.Models;

namespace NovaVida.Controllers
{
    public class ProductReviewsController : Controller
    {
        private readonly CrawlerContext _context;

        public ProductReviewsController(CrawlerContext context)
        {
            _context = context;
        }

        // GET: ProductReviews
        public async Task<IActionResult> Index()
        {
              return _context.ProductReviews != null ? 
                          View(await _context.ProductReviews.ToListAsync()) :
                          Problem("Entity set 'CrawlerContext.ProductReviews'  is null.");
        }

        // GET: ProductReviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductReviews == null)
            {
                return NotFound();
            }

            var productReviews = await _context.ProductReviews
                .FirstOrDefaultAsync(m => m.IDProductReviews == id);
            if (productReviews == null)
            {
                return NotFound();
            }

            return View(productReviews);
        }

        // GET: ProductReviews/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductReviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IDProductReviews,IDProduct,AuthorName,Title,register,review")] ProductReviews productReviews)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productReviews);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productReviews);
        }

        // GET: ProductReviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductReviews == null)
            {
                return NotFound();
            }

            var productReviews = await _context.ProductReviews.FindAsync(id);
            if (productReviews == null)
            {
                return NotFound();
            }
            return View(productReviews);
        }

        // POST: ProductReviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IDProductReviews,IDProduct,AuthorName,Title,register,review")] ProductReviews productReviews)
        {
            if (id != productReviews.IDProductReviews)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productReviews);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductReviewsExists(productReviews.IDProductReviews))
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
            return View(productReviews);
        }

        // GET: ProductReviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductReviews == null)
            {
                return NotFound();
            }

            var productReviews = await _context.ProductReviews
                .FirstOrDefaultAsync(m => m.IDProductReviews == id);
            if (productReviews == null)
            {
                return NotFound();
            }

            return View(productReviews);
        }

        // POST: ProductReviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductReviews == null)
            {
                return Problem("Entity set 'CrawlerContext.ProductReviews'  is null.");
            }
            var productReviews = await _context.ProductReviews.FindAsync(id);
            if (productReviews != null)
            {
                _context.ProductReviews.Remove(productReviews);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductReviewsExists(int id)
        {
          return (_context.ProductReviews?.Any(e => e.IDProductReviews == id)).GetValueOrDefault();
        }
    }
}
