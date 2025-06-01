using System.Linq.Dynamic.Core;
using Shared.Models.Master.MasterCode;
using Shared.Models.Master.MasterCodeItem;

namespace Domain.Modules.Common;

public class CommonService(AppDbContext context) : ICommonService
{
    public async Task<ResponseModel<List<MasterCodeResponseModel>>> MasterCodeListAsync(MasterCodeRequestModel request)
    {
        var masterCodes = await context.MasterCodes.Where(x => request.Types.Contains(x.Type))
            .Select(x => new MasterCodeResponseModel
            {
                Id = x.Id,
                Name = x.Name,
                NameMM = x.NameMM ?? string.Empty,
                Description = x.Description ?? string.Empty,
            }).ToListAsync();
        return ResponseModel<List<MasterCodeResponseModel>>.Success(masterCodes);
    }

    public async Task<ResponseModel<List<MasterCodeItemResponseModel>>> MasterCodeItemsListAsync(string masterCodeId)
    {
        var masterCodeItems = await context.MasterCodeItems.Where(x => x.MasterCodeId == masterCodeId)
            .Select(x => new MasterCodeItemResponseModel
            {
                Id = x.Id,
                Value = x.Value,
                ValueMM = x.ValueMM ?? string.Empty,
                Description = x.Description ?? string.Empty,
            }).ToListAsync();
        return ResponseModel<List<MasterCodeItemResponseModel>>.Success(masterCodeItems);
    }
}