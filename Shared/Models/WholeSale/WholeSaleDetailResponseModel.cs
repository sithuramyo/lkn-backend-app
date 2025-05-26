namespace Shared.Models.WholeSale;

public class WholeSaleDetailResponseModel
{
    public string VoucherCode { get; set; }
    public string ProductCode { get; set; }
    public string ProductName { get; set; }
    public int ProductQuantity { get; set; }
    public decimal ProductPrice { get; set; }
    public decimal ProductTotalPrice { get; set; }
}
