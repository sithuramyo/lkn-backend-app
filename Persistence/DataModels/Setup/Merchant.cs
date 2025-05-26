namespace Persistence.DataModels.Setup;

public class Merchant : BaseDataModel
{
    public string MerchantCode { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string StateId { get; set; } = null!;
    public string TownShipId { get; set; } = null!;
    public string? Description { get; set; }
}