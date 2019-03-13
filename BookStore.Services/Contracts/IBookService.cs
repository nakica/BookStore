using BookStore.Data.Models;
using System.Collections.Generic;

namespace BookStore.Services.Contracts
{
    public interface IBookService
    {
        void Add(Book entity);

        Book Get(int id);

        IEnumerable<Book> GetAll();

        void SaveChanges();
    }
}
