using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Books;

namespace Books.Pages
{
    public class BooksListModel : PageModel
    {
        private readonly Books.BooksDbContext _context;

        public BooksListModel(Books.BooksDbContext context)
        {
            _context = context;
        }

        public IList<Book> Book { get;set; } = default!;


        /// <summary>
        /// Получение списка всех книг 
        /// </summary>
        /// <returns>Список книг</returns>
        public async Task OnGetAsync()
        {
            if (_context.Books != null)
            {
                Book = await _context.Books
                .Include(b => b.Author).ToListAsync();
            }
            
        }
  
    }
}
