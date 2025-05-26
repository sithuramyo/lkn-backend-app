using Shared.Enums;
using Shared.Models.WholeSale;

namespace Domain.Modules.WholeSale;

public interface IWholeSaleService
{
    Task<ResponseModel<PaginationResponse<WholeSaleResponseModel>>> GetWholeSaleVouchersAsync(PaginationRequest request);
    Task<ResponseModel<PaginationResponse<WholeSaleStatusResponseModel>>> GetWholeSaleVouchersStatusAsync(PaginationRequest request, PaidStage status);
    Task<ResponseModel<NoResponseModel>> DeleteWholeSaleVoucherAsync(string voucherCode);
    Task<ResponseModel<PayableWholeSaleVoucherResponseModel>> PayableWholeSaleVoucherAsync(
        PayableWholeSaleVoucherRequestModel request);
    Task<ResponseModel<PaginationResponse<WholeSaleDetailResponseModel>>> GetWholeSaleVoucherDetailsAsync(PaginationRequest request, string voucherCode);
    Task<ResponseModel<GenerateWholeSaleVoucherResponseModel>> GenerateWholeSaleVoucherAsync(string voucherCode);
    Task<ResponseModel<NoResponseModel>> OpenWholeSaleVoucherAsync(OpenWholeSaleVoucherRequestModel request);
}