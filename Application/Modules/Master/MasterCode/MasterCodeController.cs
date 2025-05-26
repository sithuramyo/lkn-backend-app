using Domain.Modules.Master.MasterCode;
using Shared.Models.Master.MasterCode;

namespace Application.Modules.Master.MasterCode;

public class MasterCodeController(IMasterCodeService service) : BaseController
{
    [HttpGet]
    [ProducesResponseType(typeof(ResponseModel<PaginationResponse<MasterCodeResponseModel>>), StatusCodes.Status200OK)]
    public async Task<ActionResult> List([FromQuery] PaginationRequest request)
    {
        var result = await service.ListAsync(request);
        return result.IsSuccess ? Ok(result) : StatusCode(result.StatusCode, result);
    }
}