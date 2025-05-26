namespace Shared.Models.Delivery;

public class CreateDeliveryRequestModel
{
    public string StateId { get; set; }
    public string TownshipId { get; set; }
    public decimal DeliveryCost { get; set; }
}

public class UpdateDeliveryRequestModel : CreateDeliveryRequestModel {}

