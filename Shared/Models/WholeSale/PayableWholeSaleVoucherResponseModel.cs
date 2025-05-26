namespace Shared.Models.WholeSale;

public class PayableWholeSaleVoucherResponseModel
{
    public List<PayableWholeSaleVouchers> PayableWholeSaleVouchers { get; set; } = [];
    public decimal PayableAmount { get; set; }
}

public class PayableWholeSaleVouchers
{
    public string VoucherCode { get; set; } = null!;
    public decimal TotalAmount { get; set; }
    public decimal LeftAmount { get; set; }
}