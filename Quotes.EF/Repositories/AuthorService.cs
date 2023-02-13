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
    public class AuthorService : IAuthorService
    {
        private readonly DataContext _context;

        public AuthorService(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Author>> GetAuthorListAsync()
        {
            return await _context.Authors.ToListAsync();
        }
        public async Task<Author> AddAuthorAsync(Author entity)
        {
            return (await _context.Authors.AddAsync(entity)).Entity;
        }
        public void UpdateAuthor(Author entity)
        {
            var author = FindAuthor(entity.Id);
            _context.Entry(author).CurrentValues.SetValues(entity);
        }
        public void DeleteAuthor(Author entity)
        {
            _context.Authors.Remove(entity);
        }
        public async Task<bool> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public bool SaveChange()
        {
            return _context.SaveChanges() > 0;
        }
        public async Task<Author?> FindAuthorAsync(object Id)
        {
            return await _context.Authors.FindAsync(Id);
        }
        public Author? FindAuthor(object Id)
        {
            return _context.Authors.Find(Id);
        }
    

    }
}
