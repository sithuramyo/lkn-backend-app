using Shared.Models.Master.MasterCode;
using Shared.Models.Master.MasterCodeItem;

namespace Domain.Modules.Common;

public interface ICommonService
{
    Task<ResponseModel<List<MasterCodeResponseModel>>> MasterCodeListAsync(MasterCodeRequestModel request);
    Task<ResponseModel<List<MasterCodeItemResponseModel>>> MasterCodeItemsListAsync(string masterCodeId);
}