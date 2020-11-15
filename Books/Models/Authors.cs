using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        
        [Required(ErrorMessage ="Por favor escreva o nome do autor que pretende inserir!")]
        [StringLength(256, MinimumLength = 2, ErrorMessage ="O nome deve ter no minimo 2 caracteres e no maximo 256 caracters")]
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
