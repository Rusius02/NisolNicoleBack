﻿namespace Application.UseCases.Books.Dtos
{
    public class InputDtoCreateBook
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int ISBN { get; set; }
    }
}