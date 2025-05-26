namespace Shared.Models.Products;

public class CreateProductRequestModel
{
    [Required(ErrorMessage = "Product name is required")]
    public string Name { get; set; } = null!;
    [Required(ErrorMessage = "Product price is required")]
    public decimal Price { get; set; }
    public string? SizeId { get; set; }
    public string[]? ColorIds { get; set; }
    public string? PackagingId { get; set; }
    public string? MadeInId { get; set; }
}

public class UpdateProductRequestModel : CreateProductRequestModel { }