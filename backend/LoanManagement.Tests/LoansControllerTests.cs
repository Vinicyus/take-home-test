using Xunit;
using Moq;
using FluentAssertions;
using LoanManagement.API.Controllers;
using LoanManagement.API.DTOs;
using LoanManagement.Domain.Entities;
using LoanManagement.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace LoanManagement.Tests
{
    public class LoansControllerTests
    {
        private readonly Mock<ILoanRepository> _mockRepository;
        private readonly Mock<ILogger<LoansController>> _mockLogger;
        private readonly LoansController _controller;

        public LoansControllerTests()
        {
            _mockRepository = new Mock<ILoanRepository>();
            _mockLogger = new Mock<ILogger<LoansController>>();
            _controller = new LoansController(_mockRepository.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetAllLoans_ShouldReturnAllLoans()
        {
            // Arrange
            var loans = new List<Loan>
            {
                new Loan { Id = 1, ApplicantName = "Test", Amount = 1000, CurrentBalance = 500 }
            };
            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(loans);

            // Act
            var result = await _controller.GetAllLoans();

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            var returnedLoans = okResult!.Value as List<Loan>;
            returnedLoans.Should().HaveCount(1);
        }

        [Fact]
        public async Task MakePayment_ShouldDeductFromBalance()
        {
            // Arrange
            var loan = new Loan { Id = 1, CurrentBalance = 500, Status = "active" };
            _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(loan);
            _mockRepository.Setup(r => r.UpdateAsync(It.IsAny<Loan>())).ReturnsAsync(loan);

            // Act
            var result = await _controller.MakePayment(1, new PaymentDto { Amount = 200 });

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            var updatedLoan = okResult!.Value as Loan;
            updatedLoan!.CurrentBalance.Should().Be(300);
        }
    }
}