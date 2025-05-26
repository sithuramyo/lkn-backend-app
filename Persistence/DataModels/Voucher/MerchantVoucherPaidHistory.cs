namespace Persistence.DataModels.Voucher;

public class MerchantVoucherPaidHistory
{
    [Key]
    public string Id { get; set; } = null!;
    public string VoucherCode { get; set; } = null!;
    public string PaymentMethodId { get; set; } = null!;
    public decimal PaidAmount { get; set; }
    [Column(TypeName = "timestamp without time zone")]
    public DateTime PaidDate { get; set; }
}