namespace Persistence.DataModels.Setup;

public class Delivery : BaseDataModel
{
    public string StateId { get; set; }
    public string TownshipId { get; set; }
    public decimal DeliveryCost { get; set; }
}