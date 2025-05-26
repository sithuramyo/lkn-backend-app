namespace Persistence.DataModels.ECommerce;

public class PurchaseOrder : BaseDataModel
{
    public string PurchaseOrderNumber { get; set; }
    public string ConsumerCode { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IncludeBundle { get; set; }
    [Column(TypeName = "timestamp without time zone")]
    public DateTime OrderDate { get; set; }

    public string ShippingAddressId { get; set; }
    public string ShippingAddressDetail { get; set; }
    public bool IsOrderAccepted { get; set; }
    [Column(TypeName = "timestamp without time zone")]
    public DateTime OrderAcceptedDate { get; set; }
    [Column(TypeName = "timestamp without time zone")]
    public DateTime DeliveryDate { get; set; }
    public decimal DeliveryAmount { get; set; }
    public bool IsDelivered { get; set; }
}