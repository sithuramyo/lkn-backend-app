using Domain.Modules.WholeSale;
using Shared.Enums;
using Shared.Models.WholeSale;

namespace Application.Modules.WholeSale;

public class WholeSaleController(IWholeSaleService service) : BaseController
{
    [HttpGet("whole-sale-voucher")]
    [ProducesResponseType(typeof(ResponseModel<PaginationResponse<WholeSaleResponseModel>>), StatusCodes.Status200OK)]
    public async Task<ActionResult> WholeSaleVoucher([FromQuery] PaginationRequest request)
    {
        var result = await service.GetWholeSaleVouchersAsync(request);
        return result.IsSuccess ? Ok(result) : StatusCode(result.StatusCode, result);
    }
    
    [HttpGet("whole-sale-voucher-status")]
    [ProducesResponseType(typeof(ResponseModel<PaginationResponse<WholeSaleStatusResponseModel>>), StatusCodes.Status200OK)]
    public async Task<ActionResult> List([FromQuery] PaginationRequest request,[FromQuery] PaidStage status)
    {
        var result = await service.GetWholeSaleVouchersStatusAsync(request,status);
        return result.IsSuccess ? Ok(result) : StatusCode(result.StatusCode, result);
    }
    
    [HttpGet("detail")]
    [ProducesResponseType(typeof(ResponseModel<PaginationResponse<WholeSaleDetailResponseModel>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMerchantVoucherDetailList([FromQuery] PaginationRequest request,[FromQuery] string voucherCode)
    {
        var result = await service.GetWholeSaleVoucherDetailsAsync(request,voucherCode);
        return result.IsSuccess ? Ok(result) : StatusCode(result.StatusCode, result);
    }

    [HttpPost("open-merchant-voucher")]
    [ProducesResponseType(typeof(ResponseModel<NoResponseModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> OpenMerchantVoucher(RequestModel<OpenWholeSaleVoucherRequestModel> request)
    {
        var result = await service.OpenWholeSaleVoucherAsync(request.Request);
        return result.IsSuccess ? Ok(result) : StatusCode(result.StatusCode, result);
    }

    [HttpGet("payable-merchant-voucher")]
    [ProducesResponseType(typeof(ResponseModel<PayableWholeSaleVoucherResponseModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> PayableMerchantVoucher([FromQuery] PayableWholeSaleVoucherRequestModel request)
    {
        var result = await service.PayableWholeSaleVoucherAsync(request);
        return result.IsSuccess ? Ok(result) : StatusCode(result.StatusCode, result);
    }
    
    [HttpGet("generate-merchant-voucher/{voucherCode}")]
    [ProducesResponseType(typeof(ResponseModel<GenerateWholeSaleVoucherResponseModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GenerateWholeSaleVoucher(string voucherCode)
    {
        var result = await service.GenerateWholeSaleVoucherAsync(voucherCode);
        return result.IsSuccess ? Ok(result) : StatusCode(result.StatusCode, result);
    }

    [HttpDelete("{voucherCode}")]
    [ProducesResponseType(typeof(ResponseModel<NoResponseModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteWholeSaleVoucher(string voucherCode)
    {
        var result = await service.DeleteWholeSaleVoucherAsync(voucherCode);
        return result.IsSuccess ? Ok(result) : StatusCode(result.StatusCode, result);
    }
}