namespace Persistence.DataModels.ECommerce;

public class Stock : BaseDataModel
{
    public string ProductCode { get; set; } = null!;
    public string? CategoryId { get; set; }
    public string ProductName { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public bool IsOutOfStock { get; set; }
}