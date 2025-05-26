using Shared.Models.Master.MasterCode;

namespace Domain.Modules.Master.MasterCode;

public interface IMasterCodeService
{
    Task<ResponseModel<PaginationResponse<MasterCodeResponseModel>>> ListAsync(PaginationRequest request);
}