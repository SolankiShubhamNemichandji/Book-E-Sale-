using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using BookStore.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/book")]
    public class BookController : ControllerBase
    {
        BookRepository _bookRepository = new BookRepository();


        [HttpGet]
        [Route("list")]
        [ProducesResponseType(typeof(ListResponse<BookModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult GetBooks(int pageIndex = 1, int pageSize = 10, string? keyword = "")
        {
            try
            {
                if (pageIndex > 0)
                {
                    var books = _bookRepository.GetBook(pageIndex, pageSize, keyword);
                    if (books == null)
                    {
                        return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Books not found!");
                    }
                    ListResponse<BookModel> booklist = new ListResponse<BookModel>()
                    {
                        records = books.records.Select(c => new BookModel(c)),
                        totalRecords = books.totalRecords,
                    };
                    return StatusCode(HttpStatusCode.OK.GetHashCode(), booklist);
                }
                return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Please insert details properly!");
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);

            }
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ListResponse<BookModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult GetBookById(int id)
        {
            try
            {
                if (id > 0)
                {
                    var book = _bookRepository.GetBookId(id);
                    if (book == null)
                    {
                        return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Book not found!");
                    }
                    BookModel bookbyid = new BookModel(book);
                    return StatusCode(HttpStatusCode.OK.GetHashCode(), bookbyid);
                }
                return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Please insert details properly!");
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);

            }
        }

        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(ListResponse<BookModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult AddNewBook(BookModel model)
        {
            try
            {
                if (model == null)
                {
                    return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Please insert details properly!");
                }
                Book model1 = new Book()
                {
                    Id = model.id,
                    Name = model.name,
                    Price = model.price,
                    Description = model.description,
                    Base64image = model.base64image,
                    Categoryid = model.categoryId,
                    Publisherid = model.publisherId,
                    Quantity = model.quantity,
                };
                var addingbook = _bookRepository.addBook(model1);
                BookModel book = new BookModel(addingbook);
                return StatusCode(HttpStatusCode.OK.GetHashCode(), book);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);

            }
        }

        [HttpPut]
        [Route("update")]
        [ProducesResponseType(typeof(ListResponse<BookModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateBook(BookModel model)
        {
            try
            {
                if (model == null)
                {
                    return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Please insert details properly!");
                }
                Book model1 = new Book()
                {
                    Id = model.id,
                    Name = model.name,
                    Price = model.price,
                    Description = model.description,
                    Base64image = model.base64image,
                    Categoryid = model.categoryId,
                    Publisherid = model.publisherId,
                    Quantity = model.quantity,
                };
                var updatingbook = _bookRepository.updateBook(model1);
                if (updatingbook == null)
                {
                    return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Book not found!");
                }
                BookModel book = new BookModel(updatingbook);
                return StatusCode(HttpStatusCode.OK.GetHashCode(), book);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);

            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult delete(int id)
        {
            try
            {
                if (id > 0)
                {
                    var deletebook = _bookRepository.deleteBook(id);
                    return StatusCode(HttpStatusCode.OK.GetHashCode(), deletebook);
                }
                return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Please insert details properly!");
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);

            }
        }
    }
}
