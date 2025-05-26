namespace Shared.Models.Dashboards.OverviewDashboard;

public class OverviewDashboardResponseModel
{
    public WholesaleTotalRevenueResponse WholesaleTotalRevenue { get; set; }
    public RetailTotalRevenueResponse RetailTotalRevenue { get; set; }
    public ConsumersResponseModel Consumers { get; set; }
    public MerchantsResponseModel Merchants { get; set; }
    public RecentSalesResponseModel RecentSales { get; set; }
    public WholesaleAndRetailThreeMonthsSalesResponseModel WholesaleAndRetailThreeMonthsSaleList { get; set; } = new();
}

public class WholesaleTotalRevenueResponse
{
    public decimal Amount { get; set; }
    public decimal Percentage { get; set; }
}

public class RetailTotalRevenueResponse : WholesaleTotalRevenueResponse { }

public class ConsumersResponseModel
{
    public int Count { get; set; }
    public decimal Percentage { get; set; }
}

public class MerchantsResponseModel : ConsumersResponseModel { }

public class RecentSalesResponseModel
{
    public int SalesCount { get; set; }
    public List<MerchantSalesInfoResponseModel> MerchantSalesInfo { get; set; } = [];
}

public class MerchantSalesInfoResponseModel
{
    public string MerchantName { get; set; }
    public string Address { get; set; }
    public decimal Amount { get; set; }
}

public class WholesaleAndRetailThreeMonthsSalesResponseModel
{
    public List<WholesaleAndRetailThreeMonthsSalesModel> WholesaleAndRetailThreeMonthsSales { get; set; } = [];
}

public class WholesaleAndRetailThreeMonthsSalesModel
{
    public string Date { get; set; }
    public decimal Wholesale { get; set; }
    public decimal Retail { get; set; }
}