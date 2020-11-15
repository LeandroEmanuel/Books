using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Books.Data;
using Books.Models;

namespace Books.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly BooksDbContext _context;

        public AuthorsController(BooksDbContext context)//recebe a bd dos books
        {
            _context = context;
        }

        // GET: Authors
        public async Task<IActionResult> Index()//lista dos autores
        {
            return View(await _context.Author.ToListAsync());//alteracoes assincronas
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(int? id)//recebe o id do autor que é opcional
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Author
                .SingleOrDefaultAsync(m => m.AuthorId == id);// um registo ou o default que é null
            if (author == null)
            {
                //todo: talvez alguem tenha apagado esse autor. mostrar uma mensagem apropriada para o user
                return NotFound();
            }

            return View(author);
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]//validacao de seguranca
        public async Task<IActionResult> Create([Bind("AuthorId,Name")] Author author)//serve para evitar alguns ataques, só recebo os campos que estão no bind 
        {
            if (ModelState.IsValid)
            {
                //todo: validacoes adicionais antes de inserir o autor
                _context.Add(author);
                await _context.SaveChangesAsync();
                //todo: informar o user, autor criado com sucesso
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Author.FindAsync(id);
            if (author == null)
            {
                //todo: talvez alguem tenha apagado esse autor. mostrar uma mensagem apropriada para o user
                return NotFound();
            }
            return View(author);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AuthorId,Name")] Author author)
        {
            if (id != author.AuthorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(author);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(author.AuthorId))
                    {
                        // todo: talvez alguem apagou isto
                        //informar o user se quer criar um novo com os mesmos dados
                        return NotFound();
                    }
                    else
                    {
                        // todo: mostrar o erro e perguntar se quer tentar outra vez
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Author
                .FirstOrDefaultAsync(m => m.AuthorId == id);
            if (author == null)
            {
                // todo: talvez alguem apagou isto, informar o user
                return NotFound();
            }

            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var author = await _context.Author.FindAsync(id);
            if(author != null) { 
            _context.Author.Remove(author);
            await _context.SaveChangesAsync();
            }
            // todo: informar o user que o autor foi apagado com sucesso
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(int id)
        {
            return _context.Author.Any(e => e.AuthorId == id);
        }
    }
}
