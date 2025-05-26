namespace Shared.Models.WholeSale;

public class WholeSaleStatusResponseModel
{
    public string VoucherCode { get; set; } = null!;
    public string? PaymentMethod { get; set; }
    public decimal Amount { get; set; }
    public DateTime IssueDate { get; set; }
}