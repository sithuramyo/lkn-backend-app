namespace Shared.Models.Master.MasterCode;

public record MasterCodeResponseModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string NameMM { get; set; }
    public string Description { get; set; }
}