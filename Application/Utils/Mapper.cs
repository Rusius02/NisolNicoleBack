using NisolNicole.Utils.Dtos;
using AutoMapper;
using Domain;
using Application.UseCases.Books.Dtos;
using Application.UseCases.WritingEvents.dtos;
using Application.UseCases.Orders.Dtos;

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
                cfg.CreateMap<InputDtoUpdateBook, Book>();
                cfg.CreateMap<Book, OutputDtoCreateBook>();
                cfg.CreateMap<InputDtoDeleteBook, Book>();
                cfg.CreateMap<Book, OutputDtoBook>();
                cfg.CreateMap<InputDtoBook, Book>();
                //Writing event
                cfg.CreateMap<InputDtoCreateWritingEvent, WritingEvent>();
                cfg.CreateMap<WritingEvent, OutputDtoCreateWritingEvent>();
                cfg.CreateMap<InputDtoDeleteWritingEvent, WritingEvent>();
                cfg.CreateMap<WritingEvent, OutputDtoWritingEvent>();
                cfg.CreateMap<InputDtoWritingEvent, WritingEvent>();
                cfg.CreateMap<InputDtoUpdateWritingEvent, WritingEvent>();
                //Order
                cfg.CreateMap<InputDtoCreateOrder, Order>();
                cfg.CreateMap<Order, OutputDtoCreateOrder>();
            });
            return new AutoMapper.Mapper(config);
        }
    }
}