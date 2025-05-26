namespace Shared.Models.DashboardConfig;


public class DashboardHeaderConfigResponseModel
{
    public DashboardHeaderConfigModel DashboardHeaderConfig { get; set; } = new();
}
public class DashboardHeaderConfigModel
{
    public MerchantsAndSuppliersCount MerchantsAndSuppliers { get; set; } = new();
    public List<TopSaleProducts> TopSaleProducts { get; set; } = [];
    public List<TopPurchasedMerchants> TopPurchasedMerchants { get; set; } = [];
}

public class MerchantsAndSuppliersCount
{
    public string Month { get; set; }
    public int MerchantCount { get; set; }
    public int SupplierCount { get; set; }
}

public class TopSaleProducts
{
    public string Label { get; set; }
    public string Product { get; set; }
    public int BuyerCount { get; set; }
}

public class TopPurchasedMerchants
{
    public string Label { get; set; }
    public string Name { get; set; }
    public decimal PurchasedPrice { get; set; }
}

public class ProductSummary
{
    public string ProductCode { get; set; } = null!;
    public int TotalQuantity { get; set; }
}

public class MerchantSummary
{
    public string MerchantCode { get; set; } = null!;
    public decimal TotalPrice { get; set; }
}