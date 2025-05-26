namespace Shared.Models.Images;

public class ImageResponseModel
{
    public ImageModel Image { get; set; } = new();
}

public class UploadImageResponseModel{}
public class ImageModel
{
    public string ObjectId { get; set; }
    public string[] ImageUrl { get; set; }
}