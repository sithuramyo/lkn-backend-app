namespace Persistence.DataModels.ECommerce;

public class Season : BaseDataModel
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}