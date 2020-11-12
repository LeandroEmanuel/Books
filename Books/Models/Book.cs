using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Models
{
    public class Book
    {
        public int BookId { get; set; }

        [Required]
        [StringLength(512)]
        public string Title { get; set; }

        public string Description { get; set; }

        public int AutorId { get; set; }
        public Autores autor { get; set; }
    }
}
