namespace BookStore.Services
{
    using BookStore.Data.Models;
    using BookStore.Repository.Contracts;
    using BookStore.Services.Contracts;
    using System.Collections.Generic;

    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public void Add(Book entity) => _bookRepository.Add(entity);
        public Book Get(int id) => _bookRepository.Get(id);
        public IEnumerable<Book> GetAll() => _bookRepository.GetAll();

        public void Remove(Book entity) => _bookRepository.Remove(entity);
        public void SaveChanges() => _bookRepository.SaveChanges();
    }
}
