using Shared.Models.Master.MasterCodeItem;

namespace Domain.Modules.Master.MasterCodeItem;

public interface IMasterCodeItemService
{
    Task<ResponseModel<PaginationResponse<MasterCodeItemResponseModel>>> ListAsync(PaginationRequest request,string masterCodeId);
    Task<ResponseModel<NoResponseModel>> CreateAsync(CreateMasterCodeItemRequestModel request);
    Task<ResponseModel<MasterCodeItemResponseModel>> GetByIdAsync(string id);
    Task<ResponseModel<NoResponseModel>> UpdateAsync(string id, UpdateMasterCodeItemRequestModel request);
    Task<ResponseModel<NoResponseModel>> DeleteAsync(string id);
}