namespace BookStore.Web.Controllers
{
    using BookStore.Data.Models;
    using BookStore.Services.Contracts;
    using BookStore.Web.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public ViewResult Index()
        {
            var viewModel = new BookDetailsViewModel
            {
                BookDetails = _bookService.GetAll()
            };
            return View(viewModel);
        }

        // GET: Books/Details/
        public ActionResult DetailsPartial(int id)
        {
            var viewModel = new BookViewModel
            {
                BookDetails = _bookService.Get(id)
            };

            return PartialView(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ViewResult Create() => View();

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookSetViewModel viewModel)
        {
            var userBook = new Book
            {
                Title = viewModel.Title,
                Genre = viewModel.Genre,
                Description = viewModel.Description,
                Price = viewModel.Price,
                Author = new Author
                {
                    FirstName = viewModel.AuthorFirstName,
                    LastName = viewModel.AuthorLastName
                }
            };
            _bookService.Add(userBook);
            _bookService.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if(id == null)
        //    {
        //        return NotFound();
        //    }

        //    var book = await _context.Books.FindAsync(id);
        //    if(book == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Id", book.AuthorId);
        //    return View(book);
        //}

        //// POST: Books/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Price,ImageUrl,AuthorID")] Book book)
        //{
        //    if(id != book.Id)
        //    {
        //        return NotFound();
        //    }

        //    if(ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(book);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch(DbUpdateConcurrencyException)
        //        {
        //            if(!BookExists(book.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Id", book.AuthorId);
        //    return View(book);
        //}

        //// GET: Books/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if(id == null)
        //    {
        //        return NotFound();
        //    }

        //    var book = await _context.Books
        //        .Include(b => b.Author)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if(book == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(book);
        //}

        //// POST: Books/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var book = await _context.Books.FindAsync(id);
        //    _context.Books.Remove(book);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool BookExists(int id) => _context.Books.Any(e => e.Id == id);
    }
}
