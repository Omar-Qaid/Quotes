using Microsoft.EntityFrameworkCore;
using Quotes.Core.Entities;
using Quotes.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quotes.EF.Repositories
{
    public class QuoteService : IQuoteService
    {
        private readonly DataContext _context;

        public QuoteService(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Quote>> GetQuoteListAsync()
        {
            return await _context.Quotes.Include(x => x.Author).ToListAsync();
        }

        public async Task<IEnumerable<Quote>> GetQuoteByAuthorAsync(int AuthorId)
        {
            return await _context.Quotes.Where(x => x.AuthorId == AuthorId).ToListAsync();
        }

        public async Task<int> CountQuoteAsync()
        {
            return await _context.Quotes.CountAsync();
        }

        public async Task<Quote> AddQuoteAsync(Quote entity)
        {
            return (await _context.Quotes.AddAsync(entity)).Entity;
        }
        public void UpdateQuote(Quote entity)
        {
            var Quote = FindQuote(entity.Id);
            _context.Entry(Quote).CurrentValues.SetValues(entity);
        }
        public void DeleteQuote(Quote entity)
        {
            _context.Quotes.Remove(entity);
        }
        public async Task<bool> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public bool SaveChange()
        {
            return _context.SaveChanges() > 0;
        }
        public async Task<Quote?> FindQuoteAsync(object Id)
        {
            return await _context.Quotes.FindAsync(Id);
        }
        public Quote? FindQuote(object Id)
        {
            return _context.Quotes.Find(Id);
        }
        public async Task<Quote> GetRandomQuoteAsync(int? AuthorId)
        {
            Random rand = new Random();
            var Quotes =  _context.Quotes.Include(x=>x.Author).AsQueryable();
            if (AuthorId.HasValue)
                 Quotes = Quotes.Where(x => x.AuthorId == AuthorId);

            int toSkip = rand.Next(0, await Quotes.CountAsync());
            return await Quotes.Skip(toSkip).Take(1).FirstAsync();

        }
     
    }

}
