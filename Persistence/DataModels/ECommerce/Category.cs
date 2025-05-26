namespace Persistence.DataModels.ECommerce;

public class Category : BaseDataModel
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int Order { get; set; }
}