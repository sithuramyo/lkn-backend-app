namespace Shared.Models.Categories;

public class CategoryListResponseModel
{
    public List<CategoryModel> Categories { get; set; } = [];
}

public class CategoryResponseModel
{
    public CategoryModel Category { get; set; } = new();
}
public class DeleteCategoryResponseModel { }
public class CategoryModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Order { get; set; }
}