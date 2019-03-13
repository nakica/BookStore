namespace BookStore.Repository.Contracts
{
    using BookStore.Data.Models;
    using System.Collections.Generic;

    public interface IBookRepository
    {
        Book Get(int id);

        IEnumerable<Book> GetAll();

        void Add(Book entity);

        void Remove(Book entity);

        void SaveChanges();
    }
}
