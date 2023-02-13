using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Quotes.Core.Entities;

namespace Quotes.Web.Models
{
    public class QuoteViewModel
    {
        [Display(Name = "Author Name")]
        public int AuthorId { get; set; }
        public IEnumerable<Author> Authors { get; set; }

        public IEnumerable<Quote> Quotes { get; set; }

    }
}
