using NisolNicole.Utils.Dtos;
using AutoMapper;
using Domain;
using Application.UseCases.Books.Dtos;

namespace Application.Utils
{
    public class Mapper
    {
        private static AutoMapper.Mapper _instance;

        public static AutoMapper.Mapper GetInstance()
        {
            return _instance ??= CreateMapper();
        }

        private static AutoMapper.Mapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                // Source, Destination
                //User
                cfg.CreateMap<InputDtoCreateUsers, Users>();
                cfg.CreateMap<Users, OutputDtoCreateUser>();
                cfg.CreateMap<Users, OutputDtoUser>();
                cfg.CreateMap<InputDtoUpdateUsers, Users>();
                cfg.CreateMap<InputDtoUsers, Users>();
                //Book
                cfg.CreateMap<InputDtoCreateBook, Book>();
                cfg.CreateMap<Book, OutputDtoCreateBook>();
                cfg.CreateMap<InputDtoDeleteBook, Book>();
                cfg.CreateMap<Book, OutputDtoBook>();
                cfg.CreateMap<InputDtoBook, Book>();
            });
            return new AutoMapper.Mapper(config);
        }
    }
}