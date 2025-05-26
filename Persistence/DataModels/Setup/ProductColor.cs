namespace Persistence.DataModels.Setup;

public class ProductColor
{
    [Key]
    public string Id { get; set; } = null!;
    public string ProductId { get; set; } = null!;
    public string ColorId { get; set; } = null!;
}