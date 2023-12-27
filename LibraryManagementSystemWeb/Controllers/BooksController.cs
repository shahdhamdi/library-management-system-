using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManagementSystem.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BooksController(ApplicationDbContext context,IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Books
        public async Task<IActionResult> Index(int? id)
        {
       
            if(id!=null)
            {
                var query = _context.Books.Include(b => b.Category).Where(b => b.CategoryId == id);
                return View(await query.ToListAsync());
            }
            var query2 = _context.Books.Include(b => b.Category);
            return View( await query2.ToListAsync()); 


        }
      

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [Authorize(Roles ="Admin")]
        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "id", "Name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Book book,IFormFile file)
        {
            try
            {
                if (file != null)
                {

                    string folder = "images/";
                    folder += file.FileName;
                    string FilePathOnServer = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    await file.CopyToAsync(new FileStream(FilePathOnServer, FileMode.Create));

                    book.BookImage = file.FileName;


                }
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["CategoryId"] = new SelectList(_context.Categories, "id", "Name", book.CategoryId);
                return View(book);
            }
        }

        // GET: Books/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "id", "Name", book.CategoryId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id,Book book, IFormFile file)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

         
                try
                {
                _context.Entry(book).State = EntityState.Modified;
                    if (file != null)
                    {
                     
                        string folder = "images/";
                        folder += file.FileName;
                        string FilePathOnServer = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                        if (!Directory.Exists(FilePathOnServer))
                        {
                            FileStream fs=new FileStream(FilePathOnServer, FileMode.Create);
                            await file.CopyToAsync(fs);
                            fs.Close();
                            book.BookImage = file.FileName;
                        }
                    }
                    else
                    {
                      
                        _context.Entry(book).Property(x => x.BookImage).IsModified = false;
                     
                    }


       


                    await _context.SaveChangesAsync();
                }
                catch 
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            ViewData["CategoryId"] = new SelectList(_context.Categories, "id", "Name", book.CategoryId);
            return View(book);
        }

        // GET: Books/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Books == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Books'  is null.");
            }
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
          return (_context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
