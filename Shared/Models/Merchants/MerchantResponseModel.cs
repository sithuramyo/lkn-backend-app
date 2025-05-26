using System.Text.Json.Serialization;
namespace Shared.Models.Merchants;

public class MerchantResponseModel
{
    public string Id { get; set; }
    public string MerchantCode { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? State { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? StateId { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Township { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? TownshipId { get; set; }
    public string? Description { get; set; }
}
