using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quotes.Core.Entities
{
    [Table("Quotes")]
    public class Quote : BaseEntity    // inherit common columns From BaseEntity
    {
       
        [Required]
        [Display(Name = "Quote Content")]
        [Column(TypeName="varchar(250)")]
        public string Text { get; set; }


        [Required]
        [ForeignKey("Author")]
        public int AuthorId { get; set; }

        [Display(Name = "Author")]
        public virtual Author? Author { get; set; }

    }
}
