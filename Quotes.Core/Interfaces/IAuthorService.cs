using Quotes.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quotes.Core.Interfaces
{
    public interface IAuthorService
    {
        public Task<IEnumerable<Author>> GetAuthorListAsync();
        public Task<Author> AddAuthorAsync(Author entity);
        public void UpdateAuthor(Author entity);
        public void DeleteAuthor(Author entity);
        public Task<Author> FindAuthorAsync(object Id);
        public Author FindAuthor(object Id);
        public Task<bool> SaveChangeAsync();
        public bool SaveChange();
    }
}
