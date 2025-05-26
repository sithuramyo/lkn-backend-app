namespace Shared.Models.WholeSale;

public class OpenWholeSaleVoucherRequestModel
{
    [Required(ErrorMessage = "Merchant code is required")]
    public string MerchantCode { get; set; } = null!;
    public bool IsOldVoucher { get; set; }
    public DateTime VoucherOpenedDate { get; set; }
    public List<ProductCodeList> ProductInfos { get; set; }
}

public class ProductCodeList
{
    public string ProductCode { get; set; } = null!;
    public string? ProductColor { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    
}