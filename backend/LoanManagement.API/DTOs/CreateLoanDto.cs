namespace LoanManagement.API.DTOs
{
    public class CreateLoanDto
    {
        public decimal Amount { get; set; }
        public decimal CurrentBalance { get; set; }
        public string ApplicantName { get; set; } = string.Empty;
    }

    public class PaymentDto
    {
        public decimal Amount { get; set; }
    }
}