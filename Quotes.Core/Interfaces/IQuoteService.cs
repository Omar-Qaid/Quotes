using Quotes.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quotes.Core.Interfaces
{
    public interface IQuoteService
    {
        public Task<IEnumerable<Quote>> GetQuoteListAsync();
        public Task<Quote> AddQuoteAsync(Quote entity);
        public void UpdateQuote(Quote entity);
        public void DeleteQuote(Quote entity);
        public Task<Quote> FindQuoteAsync(object Id);
        public Quote FindQuote(object Id);
        public Task<bool> SaveChangeAsync();
        public bool SaveChange();
        public Task<IEnumerable<Quote>> GetQuoteByAuthorAsync(int AuthorId);
        public Task<Quote> GetRandomQuoteAsync(int? AuthorId);
    
        
    }
}
