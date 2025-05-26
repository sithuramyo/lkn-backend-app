namespace Shared.Models.WholeSale;

public class PayableWholeSaleVoucherRequestModel
{
    [Required(ErrorMessage = "Merchant code is required")]
    public string MerchantCode { get; set; } = null!;
    [Range(1000.0, double.MaxValue, ErrorMessage = "Payable amount must be at least 1000")]
    public decimal PayableAmount { get; set; }
}