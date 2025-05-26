namespace Shared.Models.DashboardConfig;

public class DashboardBodyConfigResponseModel
{
    public List<DashboardBodyConfigModel> DashboardBodyConfigs { get; set; } = [];
}

public class DashboardBodyConfigModel
{
    public string Date { get; set; }
    public decimal Sale { get; set; }
}