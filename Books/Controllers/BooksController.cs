using Books.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Books.Controllers
{
    public class BooksController : Controller
    {
        BooksDbContext context;
        public IList<Book> Book { get; set; } = default!;

        /// <summary>
        /// Конструктор класса 
        /// </summary>
        public BooksController()
        {
            context = new BooksDbContext();
        }
        
        /// <summary>
        /// Получение всех книг
        /// </summary>
        /// <returns>Представление со списком всех книг</returns>
        [HttpGet("allBooks")]
        public async Task<IActionResult> getAllBooks()
        {
            var book = await context.Books
                .Include(b => b.Author).ToListAsync();
            return View("BooksLis", book);
        }
        /// <summary>
        /// Получение всех авторов
        /// </summary>
        /// <returns>Список авторов</returns>
        public List<Author> getAllAuthors()
        {
            return context.Authors.ToList();
        }

        /// <summary>
        /// Получение строки с фамилией, именем и отчеством автора
        /// </summary>
        /// <param name="id">Идентификатор автора</param>
        /// <returns>Строка, содержащая фамилию имя и отчество автора</returns>
        public string getFIOAuthor(int? id)
        {
            Author author = context.Authors.Find(id);
            return author.Surname + " " + author.Nameauthor + " " + author.Patronimic;
        }
        /// <summary>
        /// Получение идентификатора автора
        /// </summary>
        /// <param name="fio">Строка, содержащая фамилию имя и отчество автора</param>
        /// <returns>Идентификатор автора</returns>
        public int getIdAuthor(string fio)
        {
            string[] splitfio = fio.Split(" ");
            Author author = context.Authors.FirstOrDefault(p => p.Surname == splitfio[0]
            && p.Nameauthor == splitfio[1]
            && p.Patronimic == splitfio[2]);
            return author.Id;
        }

        /// <summary>
        /// Получение страницы для добавления новой книги
        /// </summary>
        /// <returns>Переадресация на страницу добавления книги</returns>
        [HttpGet]
        public IActionResult AddBook()
        {
            return RedirectToPage("/NewBook");
        }
        /// <summary>
        /// Получение данных со страницы и добавление новой книги
        /// </summary>
        /// <param name="model">Модель для книги</param>
        /// <returns>Переадресация на основную страницу</returns>
        [HttpPost]
        public async Task<IActionResult> AddBook(BookModel model)
        {
            if (ModelState.IsValid)
            {
                Book book = new Book();
                book.Title = model.Title;
                book.Category = model.Category;
                book.AuthorId = this.getIdAuthor(model.Author);
                context.Books.Add(book);
                context.SaveChanges();
            }
            return RedirectToPage("/BooksL");

        }

        /// <summary>
        /// Поиск книги по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор книги</param>
        /// <returns>Представление с информацией о книге</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View("Details",book);
        }

    }
}
