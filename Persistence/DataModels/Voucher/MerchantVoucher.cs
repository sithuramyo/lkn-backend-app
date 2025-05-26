namespace Persistence.DataModels.Voucher;

public class MerchantVoucher : BaseDataModel
{
    public string VoucherCode { get; set; } = null!;
    public string MerchantCode { get; set; } = null!;
    [Column(TypeName = "timestamp without time zone")]
    public DateTime VoucherOpenedDate { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal LeftPrice { get; set; }
    public string PaidStatus { get; set; } = null!;
}