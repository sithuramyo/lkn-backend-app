namespace Shared.Models.Merchants;

public class CreateMerchantRequestModel
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = null!;
    [Required(ErrorMessage = "Phone number is required")]
    [RegularExpression(@"^(09|\+?959)\d{7,9}$", ErrorMessage = "Invalid Myanmar phone number")]
    public string PhoneNumber { get; set; } = null!;
    [Required(ErrorMessage = "State is required")]
    public string StateId { get; set; } = null!;
    [Required(ErrorMessage = "Township is required")]
    public string TownshipId { get; set; } = null!;
    public string? Description { get; set; } 
}

public class UpdateMerchantRequestModel : CreateMerchantRequestModel{}