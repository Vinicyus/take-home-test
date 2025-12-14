using Microsoft.AspNetCore.Mvc;
using LoanManagement.Domain.Entities;
using LoanManagement.Infrastructure.Repositories;
using LoanManagement.API.DTOs;

namespace LoanManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoansController : ControllerBase
    {
        private readonly ILoanRepository _loanRepository;
        private readonly ILogger<LoansController> _logger;

        public LoansController(ILoanRepository loanRepository, ILogger<LoansController> logger)
        {
            _loanRepository = loanRepository;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<Loan>> CreateLoan([FromBody] CreateLoanDto dto)
        {
            try
            {
                var loan = new Loan
                {
                    Amount = dto.Amount,
                    CurrentBalance = dto.CurrentBalance,
                    ApplicantName = dto.ApplicantName,
                    Status = "active"
                };

                var created = await _loanRepository.CreateAsync(loan);
                return CreatedAtAction(nameof(GetLoan), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating loan");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Loan>> GetLoan(int id)
        {
            var loan = await _loanRepository.GetByIdAsync(id);
            if (loan == null)
                return NotFound();

            return Ok(loan);
        }

        [HttpGet]
        public async Task<ActionResult<List<Loan>>> GetAllLoans()
        {
            var loans = await _loanRepository.GetAllAsync();
            return Ok(loans);
        }

        [HttpPost("{id}/payment")]
        public async Task<ActionResult<Loan>> MakePayment(int id, [FromBody] PaymentDto dto)
        {
            try
            {
                var loan = await _loanRepository.GetByIdAsync(id);
                if (loan == null)
                    return NotFound();

                if (dto.Amount <= 0)
                    return BadRequest("Payment amount must be positive");

                if (dto.Amount > loan.CurrentBalance)
                    return BadRequest("Payment amount exceeds current balance");

                loan.CurrentBalance -= dto.Amount;

                if (loan.CurrentBalance == 0)
                    loan.Status = "paid";

                await _loanRepository.UpdateAsync(loan);
                return Ok(loan);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing payment");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}