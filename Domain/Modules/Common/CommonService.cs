using Shared.Models.Master.MasterCodeItem;

namespace Domain.Modules.Common;

public class CommonService(AppDbContext context) : ICommonService
{
    public async Task<ResponseModel<List<MasterCodeItemResponseModel>>> MasterCodeListAsync(MasterCodeItemRequestModel request)
    {
        List<MasterCodeItemResponseModel> response = [];
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
        response = masterCodeItems;
        return ResponseModel<List<MasterCodeItemResponseModel>>.Success(response);
    }
}