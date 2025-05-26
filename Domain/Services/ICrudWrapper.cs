namespace Domain.Services;

public interface ICrudWrapper<TResponse, in TCreate, in TUpdate>
{
    Task<ResponseModel<PaginationResponse<TResponse>>> ListAsync(PaginationRequest request);
    Task<ResponseModel<NoResponseModel>> CreateAsync(TCreate request);
    Task<ResponseModel<TResponse>> GetByIdAsync(string id);
    Task<ResponseModel<NoResponseModel>> UpdateAsync(string id, TUpdate request);
    Task<ResponseModel<NoResponseModel>> DeleteAsync(string id);
}