﻿namespace Infrastructure.SqlServer.Repository.Books
{
    public interface IBookRepository
    {
        Domain.Book Create(Domain.Book book);

        List<Domain.Book> GetAll();

        Domain.Book GetBook(Domain.Book book);

        bool Delete(Domain.Book book);

        bool Update(Domain.Book book);
    }
}
