using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SymphonicSeats2.Models;

namespace SymphonicSeats2.Controllers
{
    public class CollectionItemController : Controller
    {
        private readonly CollectionContext _context;
        private readonly IWebHostEnvironment Environment;

        public CollectionItemController(CollectionContext context, IWebHostEnvironment environment)
        {
            _context = context;
            this.Environment = environment;
        }

        // GET: CollectionItem
        // Returns collection items into a view
        public async Task<IActionResult> Index()
        {
            return Redirect("/");
        }

        // GET: CollectionItem/Details/5
        // Looks for object based on id
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CollectionItems == null)
            {
                return NotFound();
            }

            var collectionItem = await _context.CollectionItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (collectionItem == null)
            {
                return NotFound();
            }

            return View(collectionItem);
        }

        // GET: CollectionItem/Create
        //
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: CollectionItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // Recieves data when forms are entered, checks if data is valid, and then adds teh new CollectionItem to the database and
        // returns to index page. 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(
            [Bind("Name,Description,ConcertTime,Location")]
            CollectionItem collectionItem,
            List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                // if we didnt get a file, it will be null
                if ((files.FirstOrDefault()?.Length ?? 0) > 0)
                {
                    // specifying the path of the file being directed to the collectionIages folder
                    var filePath = Path.Combine(this.Environment.WebRootPath, "collectionImages", collectionItem.Name + ".jpg");

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await files.First().CopyToAsync(stream);
                    }

                    collectionItem.ImageURL = $"/collectionImages/{collectionItem.Name}.jpg";
                }
                _context.Add(collectionItem);
                await _context.SaveChangesAsync();
                // turns Index into "Index" to return you to the index page after for is submitted
                return RedirectToAction(nameof(Index));
            }
            return View(collectionItem);
        }

        // GET: CollectionItem/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CollectionItems == null)
            {
                return NotFound();
            }

            var collectionItem = await _context.CollectionItems.FindAsync(id);
            if (collectionItem == null)
            {
                return NotFound();
            }
            return View(collectionItem);
        }

        // POST: CollectionItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,ConcertTime,ImageURL,Location,Votes,Price")] CollectionItem collectionItem)
        {
            if (id != collectionItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(collectionItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollectionItemExists(collectionItem.Id))
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
            return View(collectionItem);
        }

        // GET: CollectionItem/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CollectionItems == null)
            {
                return NotFound();
            }

            var collectionItem = await _context.CollectionItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (collectionItem == null)
            {
                return NotFound();
            }

            return View(collectionItem);
        }

        // POST: CollectionItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CollectionItems == null)
            {
                return Problem("Entity set 'CollectionContext.CollectionItems'  is null.");
            }
            var collectionItem = await _context.CollectionItems.FindAsync(id);
            if (collectionItem != null)
            {
                _context.CollectionItems.Remove(collectionItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CollectionItemExists(int id)
        {
            return (_context.CollectionItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
