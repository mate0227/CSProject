using System.Collections.Generic;
using System.Threading.Tasks;
using konyvtar.Controllers;
using konyvtar.Contracts;
using konyvtar.IServices;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace webapiservices.Tests
{
    public class ReadersControllerTests
    {
        [Fact]
        public async Task GetReaders_ReturnsListOfReaders()
        {
            var readerServiceMock = new Mock<IReaderService>();
            var controller = new ReadersController(readerServiceMock.Object);

            var expectedReaders = new List<Reader> { new Reader { Id = 1, Name = "Reader 1" } };
            readerServiceMock.Setup(service => service.Get()).ReturnsAsync(expectedReaders);

            var result = await controller.GetReaders();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualReaders = Assert.IsAssignableFrom<List<Reader>>(okResult.Value);
            Assert.Single(actualReaders);
        }

        [Fact]
        public async Task AddReader_WithValidReader_ReturnsOk()
        {
            var readerServiceMock = new Mock<IReaderService>();
            var controller = new ReadersController(readerServiceMock.Object);

            var newReader = new Reader { Id = 1, Name = "New Reader" };
            readerServiceMock.Setup(service => service.Get(1)).ReturnsAsync((Reader)null);

            var result = await controller.AddReader(newReader);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task UpdateReader_WithValidId_ReturnsNoContent()
        {
            var readerServiceMock = new Mock<IReaderService>();
            var controller = new ReadersController(readerServiceMock.Object);

            var updatedReader = new Reader { Id = 1, Name = "Updated Reader" };

            var result = await controller.UpdateReader(1, updatedReader);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteReader_WithValidId_ReturnsOk()
        {
            var readerServiceMock = new Mock<IReaderService>();
            var controller = new ReadersController(readerServiceMock.Object);

            var existingReader = new Reader { Id = 1, Name = "Existing Reader" };
            readerServiceMock.Setup(service => service.Get(1)).ReturnsAsync(existingReader);

            var result = await controller.DeleteReader(1);

            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(existingReader.Id, Assert.IsAssignableFrom<Reader>(((OkObjectResult)result.Result).Value).Id);
        }
    }

}
