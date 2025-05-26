namespace Shared.Models.PurchaseOrders;

public class OpenPurchaseOrderRequestModel
{
    public string ConsumerCode { get; set; }
    public decimal TotalAmount { get; set; }
    public List<StockModel> Stocks { get; set; }
    public bool IncludeBundle { get; set; }
    public string[] BundleId{ get; set; }
    public string ShippingAddressId { get; set; }
    public string ShippingAddressDetail { get; set; }
    public string DeliveryAmountId { get; set; }
}

public class AcceptPurchaseOrderRequestModel
{
    public string PurchaseOrderNumber { get; set; }
    public bool IsAccept {get;set;}
}


public class DeliverPurchaseOrderRequestModel
{
    public string PurchaseOrderNumber { get; set; }
    public bool IsDeliver { get; set; }
}

public class StockModel
{
    public string StockId { get; set; }
    public int Quantity { get; set; }
    public bool IsBundleStock { get; set; }
}
