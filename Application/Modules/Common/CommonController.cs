using Domain.Modules.Common;
using Shared.Models.Master.MasterCodeItem;

namespace Application.Modules.Common;

public class CommonController(ICommonService service) : BaseController
{
    [HttpGet]
    [ProducesResponseType(typeof(ResponseModel<List<MasterCodeItemResponseModel>>), StatusCodes.Status200OK)]
    public async Task<ActionResult> List([FromQuery] MasterCodeItemRequestModel request)
    {
        var result = await service.MasterCodeListAsync(request);
        return result.IsSuccess ? Ok(result) : StatusCode(result.StatusCode, result);
    }
}