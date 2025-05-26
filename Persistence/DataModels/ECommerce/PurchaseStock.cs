namespace Persistence.DataModels.ECommerce;

public class PurchaseStock : BaseDataModel
{
    public string PurchaseOrderNumber { get; set; }
    public string StockId { get; set; }
    public int Quantity { get; set; }
    public bool IsBundleStock { get; set; }
}