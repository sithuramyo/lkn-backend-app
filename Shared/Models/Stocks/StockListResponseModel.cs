namespace Shared.Models.Stocks;

public class StockListResponseModel
{
    public List<StockListModel> Stocks { get; set; } = [];
}

public class StockResponseModel
{
    public StockModel Stock { get; set; } = new();
}

public class DeleteStockResponseModel { }

public class StockListModel
{
    public string Id { get; set; }
    public string? CategoryName { get; set; }
    public string? ProductCode { get; set; }
    public string ProductName { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public bool IsOutOfStock { get; set; }
}

public class StockModel
{
    public string Id { get; set; }
    public string ProductCode { get; set; } = null!;
    public string? CategoryId { get; set; }
    public string ProductName { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
}