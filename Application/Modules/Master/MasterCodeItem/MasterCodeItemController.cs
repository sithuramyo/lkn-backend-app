using Domain.Modules.Master.MasterCodeItem;
using Shared.Models.Master.MasterCodeItem;

namespace Application.Modules.Master.MasterCodeItem;

public class MasterCodeItemController(IMasterCodeItemService service) : BaseController
{
    [HttpGet]
    [ProducesResponseType(typeof(ResponseModel<PaginationResponse<MasterCodeItemResponseModel>>), StatusCodes.Status200OK)]
    public async Task<ActionResult> List([FromQuery] PaginationRequest request, [FromQuery] string masterCodeId)
    {
        var result = await service.ListAsync(request, masterCodeId);
        return result.IsSuccess ? Ok(result) : StatusCode(result.StatusCode, result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseModel<NoResponseModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult> Create(RequestModel<CreateMasterCodeItemRequestModel> request)
    {
        var result = await service.CreateAsync(request.Request);
        return result.IsSuccess ? Ok(result) : StatusCode(result.StatusCode, result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseModel<MasterCodeItemResponseModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetById(string id)
    {
        var result = await service.GetByIdAsync(id);
        return result.IsSuccess ? Ok(result) : StatusCode(result.StatusCode, result);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ResponseModel<NoResponseModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult> Update(string id, RequestModel<UpdateMasterCodeItemRequestModel> request)
    {
        var result = await service.UpdateAsync(id, request.Request);
        return result.IsSuccess ? Ok(result) : StatusCode(result.StatusCode, result);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ResponseModel<NoResponseModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult> Delete(string id)
    {
        var result = await service.DeleteAsync(id);
        return result.IsSuccess ? Ok(result) : StatusCode(result.StatusCode, result);
    }
}