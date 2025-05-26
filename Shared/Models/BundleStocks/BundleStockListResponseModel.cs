using Shared.Models.Stocks;

namespace Shared.Models.BundleStocks;

public class BundleCategoryListResponseModel
{
    public List<BundleCategoryListModel> BundleCategories { get; set; } = [];
}

public class BundleCategoryListModel
{
    public string Id { get; set; }
    public string BundleId { get; set; }
    public string BundleName { get; set; }
    public string BundleDescription { get; set; }
    public int CategoryCount { get; set; }
    
}

public class BundleCategoryModel
{
    public string BundleId { get; set; }
    public string[] CategoryIds { get; set; }
}
public class BundleCategoryResponseModel
{
    public string Id { get; set; }
    public string BundleName { get; set; }
    public string[] Categories { get; set; }
}

public class BundleCategoryDetailsResponseModel
{
    public string BundleId { get; set; }
    public string BundleName { get; set; }
    public string? BundleDescription { get; set; }
    public List<CategoryListModel> Categories { get; set; } = [];
}

public class CategoryListModel
{
    public string CategoryId { get; set; }
    public string CategoryName { get; set; }
    public int StockCount { get; set; }
    public List<BundleCategoryStockDetails> Stocks { get; set; } = [];
}

public class BundleCategoryStockDetails
{
    public string ProductCode { get; set; }
    public string StockName { get; set; }
    public string[] StockImages { get; set; }
    public decimal StockPrice { get; set; }
    public string StockDescription { get; set; }
}
public class DeleteBundleCategoryResponseModel{}