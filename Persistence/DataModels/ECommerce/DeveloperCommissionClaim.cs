namespace Persistence.DataModels.ECommerce;

public class DeveloperCommissionClaim : BaseDataModel
{
    public string ClaimedCode { get; set; }
    public decimal ClaimedAmount { get; set; }
}