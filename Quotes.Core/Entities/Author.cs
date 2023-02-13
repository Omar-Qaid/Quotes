using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quotes.Core.Entities
{
    [Table("Authors")]
    public class Author : BaseEntity  // inherit common columns From BaseEntity
    {
       
        [Required]
        [Display(Name = "Author Name")]
        [Column(TypeName = "varchar(250)")]
        public string Name { get; set; }
      
    }
}
