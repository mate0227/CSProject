using konyvtar.Controllers;
using konyvtar.Contracts;
using konyvtar.IServices;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace webapiservices.Tests
{
    public class BooksControllerTests
    {
        [Fact]
        public async Task GetBooks_ReturnsListOfBooks()
        {
            var bookServiceMock = new Mock<IBookService>();
            var controller = new BooksController(bookServiceMock.Object);

            var expectedBooks = new List<Book> { new Book { Id = 1, Title = "Book 1" } };
            bookServiceMock.Setup(service => service.Get()).ReturnsAsync(expectedBooks);

            var result = await controller.GetBooks();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualBooks = Assert.IsAssignableFrom<List<Book>>(okResult.Value);
            Assert.Single(actualBooks);
        }

        [Fact]
        public async Task AddBook_WithValidBook_ReturnsOk()
        {
            var bookServiceMock = new Mock<IBookService>();
            var controller = new BooksController(bookServiceMock.Object);

            var newBook = new Book { Id = 1, Title = "New Book" };
            bookServiceMock.Setup(service => service.Get(1)).ReturnsAsync((Book)null);

            var result = await controller.AddBook(newBook);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task UpdateBook_WithValidId_ReturnsNoContent()
        {
            var bookServiceMock = new Mock<IBookService>();
            var controller = new BooksController(bookServiceMock.Object);

            var updatedBook = new Book { Id = 1, Title = "Updated Book" };

            var result = await controller.UpdateBook(1, updatedBook);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteBook_WithValidId_ReturnsOk()
        {
            var bookServiceMock = new Mock<IBookService>();
            var controller = new BooksController(bookServiceMock.Object);

            var existingBook = new Book { Id = 1, Title = "Existing Book" };
            bookServiceMock.Setup(service => service.Get(1)).ReturnsAsync(existingBook);

            var result = await controller.DeleteBook(1);

            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(existingBook.Id, Assert.IsAssignableFrom<Book>(((OkObjectResult)result.Result).Value).Id);
        }
    }
}
