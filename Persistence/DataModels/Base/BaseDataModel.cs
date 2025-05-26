using Shared.Extensions;

namespace Persistence.DataModels.Base;

public class BaseDataModel
{
    [Key]
    public string Id { get; init; } = Guid.NewGuid().ToString();
    
    [Column(TypeName = "timestamp without time zone")]
    public DateTime? CreatedDate { get; set; } = DateTime.Now.ToMyanmarTime();
    
    public string? CreatedId { get; set; }
    
    [Column(TypeName = "timestamp without time zone")]
    public DateTime? UpdatedDate { get; set; } = DateTime.Now.ToMyanmarTime();
    public string? UpdatedId { get; set; }
    public bool IsDeleted { get; set; }
}