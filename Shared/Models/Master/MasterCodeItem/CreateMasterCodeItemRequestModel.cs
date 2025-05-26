namespace Shared.Models.Master.MasterCodeItem;

public class CreateMasterCodeItemRequestModel
{
    [Required(ErrorMessage = "Master Code is required")]
    public string MasterCodeId { get; set; } = null!;
    [Required(ErrorMessage = "Value is required")]
    public string Value { get; set; } = null!;
    public string? ValueMM { get; set; }
    public string? Description { get; set; }
}

public class UpdateMasterCodeItemRequestModel : CreateMasterCodeItemRequestModel { }