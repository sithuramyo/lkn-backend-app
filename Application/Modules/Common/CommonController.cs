using Domain.Modules.Common;
using Shared.Models.Master.MasterCode;
using Shared.Models.Master.MasterCodeItem;

namespace Application.Modules.Common;

public class CommonController(ICommonService service) : BaseController
{
    [HttpGet("master-code")]
    [ProducesResponseType(typeof(ResponseModel<List<MasterCodeResponseModel>>), StatusCodes.Status200OK)]
    public async Task<ActionResult> List([FromQuery] MasterCodeRequestModel request)
    {
        var result = await service.MasterCodeListAsync(request);
        return result.IsSuccess ? Ok(result) : StatusCode(result.StatusCode, result);
    }

    [HttpGet("master-code-item/{masterCodeId}")]
    [ProducesResponseType(typeof(ResponseModel<List<MasterCodeItemResponseModel>>), StatusCodes.Status200OK)]
    public async Task<ActionResult> List(string masterCodeId)
    {
        var result = await service.MasterCodeItemsListAsync(masterCodeId);
        return result.IsSuccess ? Ok(result) : StatusCode(result.StatusCode, result);
    }
}