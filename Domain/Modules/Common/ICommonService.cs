using Shared.Models.Master.MasterCodeItem;

namespace Domain.Modules.Common;

public interface ICommonService
{
    Task<ResponseModel<List<MasterCodeItemResponseModel>>> MasterCodeListAsync(MasterCodeItemRequestModel request);

}