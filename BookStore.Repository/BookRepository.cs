namespace BookStore.Repository
{
    using BookStore.Data.Models;
    using BookStore.Repository.Contracts;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;

    public class BookRepository : IBookRepository
    {
        private readonly DbContext _context;

        public BookRepository(DbContext context)
        {
            _context = context;
        }

        public Book Get(int id) => _context.Set<Book>().FirstOrDefault(d => d.Id == id);

        public IEnumerable<Book> GetAll() => _context.Set<Book>().Include(a => a.Author);

        public void Add(Book entity) => _context.Set<Book>().Add(entity);

        public void Remove(Book entity) => _context.Set<Book>().Remove(entity);

        public void SaveChanges() => _context.SaveChanges();
    }
}
