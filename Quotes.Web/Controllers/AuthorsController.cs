using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quotes.Core.Entities;
using Quotes.Core.Interfaces;
using Quotes.Web.Models;
using System.Diagnostics;

namespace Quotes.Web.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IAuthorService _author;
        private readonly ILogger<AuthorsController> _logger;
        public AuthorsController(ILogger<AuthorsController> logger, IAuthorService author)
        {
            _logger = logger;
            _author = author;
        }

        // GET: Author/Index
        public async Task<IActionResult> Index()
        {
            var authors = await _author.GetAuthorListAsync();
            return View(authors);
        }

        // GET: Author/Create
        public ActionResult Create()
        {
            return View(new Author { Name = "", CreatedAt = DateTime.Now });
        }

        // POST: Author/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,CreatedAt")] Author author)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _author.AddAuthorAsync(author);
                    await _author.SaveChangeAsync();
                    TempData["alert-Type"] = "Success";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                TempData["alert-Type"] = "Error";
            }
            return View(author);
        }

        // GET: Author/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var author = await _author.FindAuthorAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        // POST: Author/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Id,Name,CreatedAt")] Author author)
        {
            if (id != author.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _author.UpdateAuthor(author);
                    _author.SaveChange();
                    TempData["alert-Type"] = "Success";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    TempData["alert-Type"] = "Error";
                }
      
            }
            return View(author);

        }

        // GET: Author/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var author = await _author.FindAuthorAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        // POST: Author/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var author = await _author.FindAuthorAsync(id);
                _author.DeleteAuthor(author);
                await _author.SaveChangeAsync();
                TempData["alert-Type"] = "Success";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                TempData["alert-Type"] = "Error";
            }
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
