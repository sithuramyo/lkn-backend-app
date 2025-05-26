namespace Shared.Models.WholeSale;

public class WholeSaleResponseModel
{
    public string VoucherCode { get; set; } = null!;
    public string MerchantName { get; set; } = null!;
    public DateTime VoucherOpenedDate { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal LeftAmount { get; set; }
    public decimal DepositAmount { get; set; }
    public string PaidStatus { get; set; } = null!;
}