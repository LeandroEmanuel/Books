using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Models
{
    public class Autores
    {
        public int AutorId { get; set; }
        
        [Required]
        [StringLength(256)]
        public string Nome { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
