namespace Shared.Models.WholeSale;

public class GenerateWholeSaleVoucherResponseModel
{
    public GenerateWholeSaleVoucherModel GenerateWholeSaleVoucher { get; set; } = new();
}

public class GenerateWholeSaleVoucherModel
{
    public string MerchantName { get; set; }
    public string PhoneNumber { get; set; }
    public string SaleDate { get; set; }
    public string PaymentStatus { get; set; }
    public List<WholeSaleVoucherList> MerchantVoucherLists { get; set; } = [];
    public decimal TotalAmount { get; set; }
    public decimal LeftAmount { get; set; }
    public decimal PaidAmount { get; set; }
}

public class WholeSaleVoucherList
{
    public int No { get; set; }
    public string ProductName { get; set; }
    public string? Color { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal Amount { get; set; }
}