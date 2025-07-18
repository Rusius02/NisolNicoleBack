﻿using Application.UseCases.Books.Dtos;
using Application.Utils;
using Infrastructure.SqlServer.Repository.Books;
using Infrastructure.SqlServer.Repository.Orders;

namespace Application.UseCases.Books
{
    public class UseCaseListBook
    {
        private readonly IBookRepository _bookRepository;

        public UseCaseListBook(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public List<OutputDtoBook> Execute()
        {
            List<Domain.Book> books = _bookRepository.GetAll();
            return Mapper.GetInstance().Map<List<OutputDtoBook>>(books);
        }

        public OutputDtoBook Execute(InputDtoBook dto)
        {
            var bookFromDto = Mapper.GetInstance().Map<Domain.Book>(dto);
            Domain.Book book = _bookRepository.GetBook(bookFromDto) ?? throw new Exception("Book is null");
            return Mapper.GetInstance().Map<OutputDtoBook>(book);
        }
    }
}
