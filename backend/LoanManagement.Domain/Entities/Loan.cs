namespace LoanManagement.Domain.Entities
{
    public class Loan
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public decimal CurrentBalance { get; set; }
        public string ApplicantName { get; set; } = string.Empty;
        public string Status { get; set; } = "active"; // active or paid
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}