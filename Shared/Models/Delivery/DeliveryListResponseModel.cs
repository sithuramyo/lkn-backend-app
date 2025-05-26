namespace Shared.Models.Delivery;

public class DeliveryListResponseModel
{
    public List<DeliveryModel> Deliveries { get; set; } = [];
}


public class DeliveryResponseModel
{
    public GetDeliveryModel Delivery { get; set; } = new();
}

public class GetDeliveryModel
{
    public string Id { get; set; }
    public string StateId { get; set; }
    public string TownshipId { get; set; }
    public decimal DeliveryCost { get; set; }
}
public class DeleteDeliveryResponseModel { }
public class DeliveryModel
{
    public string Id { get; set; }
    public string? State { get; set; }
    public string? Township { get; set; }
    public decimal DeliveryCost { get; set; }
}