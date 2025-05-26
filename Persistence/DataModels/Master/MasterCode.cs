
namespace Persistence.DataModels.Master;

public class MasterCode : BaseDataModel
{
    public string Name { get; set; } = null!;
    public string? NameMM { get; set; }
    public string Type { get; set; } = null!;
    public string? Description { get; set; }
}