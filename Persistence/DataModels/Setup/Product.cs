namespace Persistence.DataModels.Setup;

public class Product : BaseDataModel
{
    public string ProductCode { get; set; } = null!;
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string? SizeId { get; set; }
    public string? PackagingId { get; set; }
    public string? MadeInId { get; set; }
}