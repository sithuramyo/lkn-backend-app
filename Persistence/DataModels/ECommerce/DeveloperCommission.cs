namespace Persistence.DataModels.ECommerce;

public class DeveloperCommission : BaseDataModel
{
    public string PurchaseOrderNumber { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal CommissionRate { get; set; }
    public decimal CommissionFees { get; set; }
    public string ClaimedCode { get; set; }
    public bool IsClaimed { get; set; }
    [Column(TypeName = "timestamp without time zone")]
    public DateTime ClaimedDate { get; set; }
}