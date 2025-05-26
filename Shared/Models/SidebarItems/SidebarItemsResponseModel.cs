namespace Shared.Models.SidebarItems;

public class SidebarItemsResponseModel
{
    public List<SidebarItemsModel> SidebarItems { get; set; } = [];
}

public class SidebarItemsModel
{
    public string Id { get; set; }
    public string Label { get; set; }
    public string? Href { get; set; }
    public string? Icon { get; set; }
    public bool IsDropdown { get; set; }
    public List<DropdownItemsModel>? DropdownItems { get; set; } = [];
}

public class DropdownItemsModel
{
    public string Id { get; set; }
    public string Label { get; set; }
    public string? Icon { get; set; }
    public List<MenuItemsModel>? MenuItems { get; set; } = [];
}

public class MenuItemsModel
{
    public string Id { get; set; }
    public string Label { get; set; }
    public string? Icon { get; set; }
    public string Href { get; set; }
}
