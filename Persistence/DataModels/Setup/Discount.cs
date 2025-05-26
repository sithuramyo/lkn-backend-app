namespace Persistence.DataModels.Setup;

public class Discount : BaseDataModel
{
    public string CouponCode { get; set; } = null!;
    public decimal? DiscountPercentage { get; set; }
    public long DiscountCount { get; set; }
    public long UsedDiscountCount { get; set; }
}