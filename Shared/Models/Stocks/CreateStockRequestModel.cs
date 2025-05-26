namespace Shared.Models.Stocks;

public class CreateStockRequestModel
{
    public string ProductCode { get; set; } = null!;
    public string? CategoryId { get; set; }
    public string ProductName { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
}

public class UpdateStockRequestModel : CreateStockRequestModel
{
}

public class UpdateOutOfStockRequestModel
{
    public bool IsOutOfStock { get; set; }
}