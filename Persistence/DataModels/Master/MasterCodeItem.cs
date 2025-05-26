namespace Persistence.DataModels.Master;

public class MasterCodeItem : BaseDataModel
{
    public string MasterCodeId { get; set; } = null!;
    public string Value { get; set; } = null!;
    public string? ValueMM { get; set; }
    public string? Description { get; set; }
}