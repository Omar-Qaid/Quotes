using Microsoft.AspNetCore.Mvc;
using Quotes.Core.Entities;
using Quotes.Core.Interfaces;

namespace Quotes.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly ILogger<AuthorsController> _logger;
        private readonly IAuthorService _author;
        public AuthorsController(ILogger<AuthorsController> logger, IAuthorService author)
        {
            _logger = logger;
            _author = author;
        }

        // GET: api/<AuthorsController>
        [HttpGet]
        public async Task<IEnumerable<Author>> Get()
        {
            return await _author.GetAuthorListAsync();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<Author> Get(int id)
        {
            return await _author.FindAuthorAsync(id);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task Post([FromBody] Author model)
        {
            await _author.AddAuthorAsync(model);
            await _author.SaveChangeAsync();
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] Author model)
        {
            if (await _author.FindAuthorAsync(id) is not null)
            {
                _author.UpdateAuthor(model);
                await _author.SaveChangeAsync();
            }
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            if (await _author.FindAuthorAsync(id) is Author model)
            {
                _author.DeleteAuthor(model);
                _author.SaveChange();
            }
        }
    }




}
