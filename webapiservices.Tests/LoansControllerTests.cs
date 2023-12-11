using System;
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
    public class LoansControllerTests
    {
        [Fact]
        public async Task GetLoans_ReturnsListOfLoans()
        {
            var loanServiceMock = new Mock<ILoanService>();
            var bookServiceMock = new Mock<IBookService>();
            var readerServiceMock = new Mock<IReaderService>();
            var controller = new LoansController(loanServiceMock.Object, bookServiceMock.Object, readerServiceMock.Object);

            var expectedLoans = new List<Loan> { new Loan { Id = 1, BookId = 1, ReaderId = 1 } };
            loanServiceMock.Setup(service => service.Get()).ReturnsAsync(expectedLoans);

            var result = await controller.GetLoans();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualLoans = Assert.IsAssignableFrom<List<Loan>>(okResult.Value);
            Assert.Single(actualLoans);
        }

        [Fact]
        public async Task AddLoan_WithValidLoanDTO_ReturnsOk()
        {
            var loanServiceMock = new Mock<ILoanService>();
            var bookServiceMock = new Mock<IBookService>();
            var readerServiceMock = new Mock<IReaderService>();
            var controller = new LoansController(loanServiceMock.Object, bookServiceMock.Object, readerServiceMock.Object);

            var loanDTO = new LoanDTO { BookId = 1, ReaderId = 1, BorrowDate = DateTime.Now, ReturnDeadline = DateTime.Now.AddDays(7) };
            loanServiceMock.Setup(service => service.Get(loanDTO.Id)).ReturnsAsync((Loan)null);
            bookServiceMock.Setup(service => service.Get(loanDTO.BookId)).ReturnsAsync(new Book { Id = 1, Title = "Book 1" });
            readerServiceMock.Setup(service => service.Get(loanDTO.ReaderId)).ReturnsAsync(new Reader { Id = 1, Name = "Reader 1" });

            var result = await controller.AddLoan(loanDTO);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task UpdateLoan_WithValidId_ReturnsNoContent()
        {
            var loanServiceMock = new Mock<ILoanService>();
            var bookServiceMock = new Mock<IBookService>();
            var readerServiceMock = new Mock<IReaderService>();
            var controller = new LoansController(loanServiceMock.Object, bookServiceMock.Object, readerServiceMock.Object);

            var updatedLoan = new Loan { Id = 1, BookId = 1, ReaderId = 1, BorrowDate = DateTime.Now, ReturnDeadline = DateTime.Now.AddDays(7) };

            var result = await controller.UpdateLoan(1, updatedLoan);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteLoan_WithValidId_ReturnsOk()
        {
            var loanServiceMock = new Mock<ILoanService>();
            var bookServiceMock = new Mock<IBookService>();
            var readerServiceMock = new Mock<IReaderService>();
            var controller = new LoansController(loanServiceMock.Object, bookServiceMock.Object, readerServiceMock.Object);

            var existingLoan = new Loan { Id = 1, BookId = 1, ReaderId = 1 };
            loanServiceMock.Setup(service => service.Get(1)).ReturnsAsync(existingLoan);

            var result = await controller.DeleteLoan(1);

            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(existingLoan.Id, Assert.IsAssignableFrom<Loan>(((OkObjectResult)result.Result).Value).Id);
        }
    }
}
