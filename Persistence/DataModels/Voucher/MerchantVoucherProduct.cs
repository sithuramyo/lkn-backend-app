namespace Persistence.DataModels.Voucher;

public class MerchantVoucherProduct : BaseDataModel
{
    public string VoucherCode { get; set; } = null!;
    public string ProductCode { get; set; } = null!;
    public string? ColorId { get; set; }
    public int ProductQuantity { get; set; }
    public decimal ProductPrice { get; set; }
}