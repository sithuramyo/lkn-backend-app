namespace Shared.Models.DeliverModels;

public class PurchaseOrderDataEmailConfigModel
{
    public string PurchaseOrderNumber { get; set; }
    public string ConsumerName { get; set; }
    public string? Email { get; set; }
    public string PhoneNumber { get; set; }
    public string ShippingAddress { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderItem> OrderItems { get; set; } = [];
    public List<BundleItem> Bundles { get; set; } = [];
    public decimal TotalPrice { get; set; }
}

public class OrderItem
{
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public bool IsBundleOrderItem { get; set; }
}

public class BundleItem
{
    public string BundleName { get; set; }
    public List<OrderItem> Items { get; set; } = [];
}