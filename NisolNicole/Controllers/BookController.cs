﻿using Application.UseCases.Books;
using Application.UseCases.Books.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace NisolNicole.Controllers
{
    [ApiController]
    [Route("api/Books")]
    public class BookController : ControllerBase
    {
        private readonly UseCaseCreateBook _useCaseCreateBook;
        private readonly UseCaseDeleteBook _useCaseDeleteBook;
        private readonly UseCaseListBook _useCaseListBook;

        public BookController(UseCaseCreateBook useCaseCreateBook, UseCaseDeleteBook useCaseDeleteBook, UseCaseListBook useCaseListBook)
        {
            _useCaseCreateBook = useCaseCreateBook;
            _useCaseDeleteBook = useCaseDeleteBook;
            _useCaseListBook = useCaseListBook;
        }

        [HttpPost]
        [Route("Create")]
        public ActionResult<OutputDtoCreateBook> Create([FromForm] InputDtoCreateBook book, [FromForm] IFormFile coverImage)
        {
            if (coverImage != null)
            {
                var imageFolderPath = Path.Combine("wwwroot", "images", "covers");
                if (!Directory.Exists(imageFolderPath))
                {
                    Directory.CreateDirectory(imageFolderPath);
                }

                var imageFileName = Guid.NewGuid() + Path.GetExtension(coverImage.FileName);
                var imagePath = Path.Combine(imageFolderPath, imageFileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    coverImage.CopyTo(stream);
                }

                book.CoverImagePath = "/images/covers/" + imageFileName; 
            }

            return StatusCode(201, _useCaseCreateBook.Execute(book));
        }


        [HttpDelete]
        [Route("Delete")]
        [ProducesResponseType(200)]
        public ActionResult<bool> Delete(int id)
        {
           return StatusCode(200, _useCaseDeleteBook.Execute(new InputDtoDeleteBook()
            {
                Id = id
            }));
        }
        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(200)]
        public ActionResult<List<OutputDtoBook>> GetAll()
        {
            return StatusCode(200, _useCaseListBook.Execute());
        }

        [HttpPost]
        [Route("GetBook")]
        [ProducesResponseType(200)]
        public ActionResult<OutputDtoBook> GetBook([FromBody] InputDtoBook inputDtoBook)
        {
            return StatusCode(200, _useCaseListBook.Execute(inputDtoBook));
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [Route("updateBook")]
        public ActionResult<bool> Update([FromForm] InputDtoUpdateBook book, [FromForm] IFormFile? coverImage)
        {
            // Si une nouvelle image de couverture est fournie, gérer son enregistrement
            if (coverImage != null)
            {
                // Sauvegarder la nouvelle image
                var filePath = Path.Combine("wwwroot/images/covers", coverImage.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    coverImage.CopyTo(stream);
                }

                // Mettre à jour le chemin de l'image dans l'objet book
                book.CoverImagePath = "/images/covers/" + coverImage.FileName;
            }

            // Exécuter le cas d'utilisation de mise à jour
            var result = _useCaseCreateBook.Execute(book);

            return StatusCode(200, result);
        }

    }
}
