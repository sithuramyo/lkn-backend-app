namespace Shared.Models.PurchaseOrders;

public class OpenPurchaseOrderResponseModel
{
    
}

public class PurchaseOrderResponseModel
{
    
}

public class DeliveryCostResponseModel
{
    public DeliveryCostModel Delivery { get; set; } = new();
}
public class DeliveryCostModel
{
    public string Id { get; set; }
    public string StateId { get; set; }
    public string TownshipId { get; set; }
    public decimal DeliveryCost { get; set; }
}
public class PurchaseOrderListResponseModel
{
    public List<PurchaseOrderModel> PurchaseOrders { get; set; }
}

public class PurchaseOrderModel
{
    public string PurchaseOrderNumber { get; set; }
    public string ConsumerName { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IncludeBundle { get; set; }
    public DateTime OrderDate { get; set; }
    public string ShippingAddressDetail { get; set; }
    public bool IsOrderAccepted { get; set; }
    public DateTime OrderAcceptedDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public decimal DeliveryAmount { get; set; }
    public bool IsDelivered { get; set; }
}