using Shared.Models.Master.MasterCodeItem;

namespace Domain.Modules.Common;

public class CommonService(AppDbContext context) : ICommonService
{
    public async Task<ResponseModel<MasterCodeItemsResponseModel>> MasterCodeListAsync(MasterCodeItemRequestModel request)
    {
        MasterCodeItemsResponseModel response = new();
        var masterCodeIds = await context.MasterCodes.Where(x => request.Type.Contains(x.Type)).Select(x => x.Id).ToListAsync();
        var masterCodeItems = context.MasterCodeItems
            .Where(x => masterCodeIds.Contains(x.Id))
            .Select(x => new MasterCodeItemResponseModel
            {
                Id = x.Id,
                Value = x.Value,
                ValueMM = x.ValueMM ?? string.Empty,
                Description = x.Description ?? string.Empty,
            })
            .ToList();
        response.MasterCodeItems = masterCodeItems;
        return ResponseModel<MasterCodeItemsResponseModel>.Success(response);
    }
}