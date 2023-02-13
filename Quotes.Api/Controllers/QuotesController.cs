using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quotes.Api.DTOs;
using Quotes.Core.Entities;
using Quotes.Core.Interfaces;

namespace Quotes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class QuotesController : ControllerBase
    {
        private readonly ILogger<QuotesController> _logger;
        private readonly IQuoteService _Quote;
        public QuotesController(ILogger<QuotesController> logger, IQuoteService Quote)
        {
            _logger = logger;
            _Quote = Quote;
         
        }
        // GET: api/<QuotesController>
        [HttpGet]
        public async Task<IEnumerable<Quote>> Get()
        {
            return await _Quote.GetQuoteListAsync();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{AuthorId}")]
        public async Task<IEnumerable<Quote>> Get(int AuthorId)
        {
            return await _Quote.GetQuoteByAuthorAsync(AuthorId);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task Post([FromBody] Quote model)
        {
            await _Quote.AddQuoteAsync(model);
            await _Quote.SaveChangeAsync();
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] Quote model)
        {
            if (await _Quote.FindQuoteAsync(id) is not null)
            {
                _Quote.UpdateQuote(model);
                await _Quote.SaveChangeAsync();
            }
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            if (await _Quote.FindQuoteAsync(id) is Quote model)
            {
                _Quote.DeleteQuote(model);
                _Quote.SaveChange();
            }
        }


    }
}
