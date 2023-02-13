using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quotes.Core.Entities;
using Quotes.Core.Interfaces;
using Quotes.Web.Models;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace Quotes.Web.Controllers
{
    public class QuotesController : Controller
    {
        private readonly IAuthorService _author;
        private readonly IQuoteService _Quote;
        private readonly ILogger<QuotesController> _logger;
        public QuotesController(ILogger<QuotesController> logger, IQuoteService Quote, IAuthorService author)
        {
            _logger = logger;
            _Quote = Quote;
            _author = author;
        }

        // GET: Quote/Index
        public async Task<IActionResult> Index(int? authorId)
        {

            if (authorId.HasValue)
            {
                var model = new QuoteViewModel()
                {
                    AuthorId = authorId.Value,
                    Authors = await _author.GetAuthorListAsync(),
                    Quotes = await _Quote.GetQuoteByAuthorAsync(authorId.Value)
                };
                return View(model);

            }
            else
            {
                var model = new QuoteViewModel()
                {
                    Authors = await _author.GetAuthorListAsync(),
                    Quotes = await _Quote.GetQuoteListAsync()
                };
                return View(model);
            }
        }

        // GET: Quote/Create
        public async Task<IActionResult> Create(int id)
        {
            ViewBag.Authors = await _author.GetAuthorListAsync();
            return View(new Quote { AuthorId = id, Text = "", CreatedAt = DateTime.Now });
        }

        // POST: Quote/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Text,AuthorId,CreatedAt")] Quote Quote)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _Quote.AddQuoteAsync(Quote);
                    await _Quote.SaveChangeAsync();
                    TempData["alert-Type"] = "Success";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                TempData["alert-Type"] = "Error";
                //ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(Quote);
        }

        // GET: Quote/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Authors = await _author.GetAuthorListAsync();
            var Quote = await _Quote.FindQuoteAsync(id);
            if (Quote == null)
            {
                return NotFound();
            }
            return View(Quote);
        }

        // POST: Quote/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Id,Text,AuthorId,CreatedAt")] Quote Quote)
        {
            if (id != Quote.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _Quote.UpdateQuote(Quote);
                    _Quote.SaveChange();
                    TempData["alert-Type"] = "Success";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    TempData["alert-Type"] = "Error";
                }
            }
            return View(Quote);

        }

        // GET: Quote/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Quote = await _Quote.FindQuoteAsync(id);
            if (Quote == null)
            {
                return NotFound();
            }
            return View(Quote);
        }

        // POST: Quote/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Quote = await _Quote.FindQuoteAsync(id);
            try
            {
                _Quote.DeleteQuote(Quote);
                await _Quote.SaveChangeAsync();
                TempData["alert-Type"] = "Success";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                TempData["alert-Type"] = "Error";
            }
            return View(Quote);
        }

        public async Task<IActionResult> RandomQuote()
        {
            var model = new QuoteViewModel()
            {
                Authors = await _author.GetAuthorListAsync(),
            };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5272/api/");
                //HTTP GET
                var responseTask = client.GetAsync("getRandomQuoteApiAsync");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<Quote>();
                    readTask.Wait();

                    if (readTask.Result is not null)
                        model.Quotes = new List<Quote>() { readTask.Result };

                }
                else //web api sent error response 
                {
                    TempData["alert-Type"] = "Error";
                }
            }
            return View("Index", model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
