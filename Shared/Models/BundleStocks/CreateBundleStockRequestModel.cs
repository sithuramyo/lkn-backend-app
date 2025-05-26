namespace Shared.Models.BundleStocks;

public class CreateBundleCategoryRequestModel
{
    public string BundleId { get; set; }
    public string[] CategoryIds { get; set; }
}

public class UpdateBundleCategoryRequestModel
{
    public string[] CategoryIds { get; set; }
}
