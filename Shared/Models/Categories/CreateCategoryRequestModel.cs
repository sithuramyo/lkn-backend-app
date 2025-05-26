namespace Shared.Models.Categories;

public class CreateCategoryRequestModel
{
    public string Name { get; set; }
    public string Description { get; set; }
}

public class UpdateCategoryRequestModel : CreateCategoryRequestModel { }

public class UpdateCategoryOrderRequestModel
{
    public string CategoryId { get; set; }
    public int Order { get; set; }
}