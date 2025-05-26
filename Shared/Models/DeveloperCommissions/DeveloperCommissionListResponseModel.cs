namespace Shared.Models.DeveloperCommissions;

public class DeveloperCommissionListResponseModel
{
    public List<DeveloperCommissionModel> DeveloperCommissions { get; set; }
}
public class DeveloperCommissionResponseModel { }
public class DeveloperCommissionModel
{
    public string PurchaseOrderNumber { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal CommissionRate { get; set; }
    public decimal CommissionFees { get; set; }
    public string ClaimedCode { get; set; }
    public bool IsClaimed { get; set; }
    public DateTime ClaimedDate { get; set; }
}

public class DeveloperClaimedCommissionListResponseModel
{
    public List<DeveloperClaimedCommissionModel> DeveloperCommissions { get; set; }
}

public class DeveloperClaimedCommissionModel
{
    public string ClaimedCode { get; set; }
    public decimal ClaimedAmount { get; set; }
}