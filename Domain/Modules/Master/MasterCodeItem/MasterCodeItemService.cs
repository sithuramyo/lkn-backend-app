using Shared.Models.Master.MasterCodeItem;

namespace Domain.Modules.Master.MasterCodeItem;

public class MasterCodeItemService(AppDbContext context) : IMasterCodeItemService
{
    public async Task<ResponseModel<PaginationResponse<MasterCodeItemResponseModel>>> ListAsync(
        PaginationRequest request, string masterCodeId)
    {
        var query = context.MasterCodeItems.Where(d => !d.IsDeleted && d.MasterCodeId == masterCodeId).AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            query = query.Where(d =>
                EF.Functions.ILike(d.Value, $"%{request.Search}%") ||
                EF.Functions.ILike(d.ValueMM, $"%{request.Search}%"));
        }

        query = query.ApplySorting(request.SortBy, request.IsAscending);

        var projectedQuery = query.Select(d => new MasterCodeItemResponseModel
        {
            Id = d.Id,
            Value = d.Value,
            ValueMM = d.ValueMM,
            Description = d.Description,
        });

        var paginated = await projectedQuery.ToPagedListAsync(request);
        return ResponseModel<PaginationResponse<MasterCodeItemResponseModel>>.Success(paginated);
    }

    public async Task<ResponseModel<NoResponseModel>> CreateAsync(CreateMasterCodeItemRequestModel request)
    {
        NoResponseModel response = new();

        var isValid = await context.MasterCodes.AnyAsync(x => x.Id == request.MasterCodeId && !x.IsDeleted);
        if (!isValid)
        {
            return ResponseModel<NoResponseModel>.NotFound("Not found");
        }

        var isExist = await context.MasterCodeItems.AnyAsync(x =>
            x.MasterCodeId == request.MasterCodeId && x.Value == request.Value && !x.IsDeleted);

        if (isExist)
        {
            return ResponseModel<NoResponseModel>.BadRequest("Master code item is already exist");
        }

        var data = new Persistence.DataModels.Master.MasterCodeItem
        {
            Id = Guid.NewGuid().ToString(),
            MasterCodeId = request.MasterCodeId,
            Value = request.Value,
            ValueMM = request.ValueMM,
            Description = request.Description,
            CreatedDate = DateTime.Now.ToMyanmarTime()
        };
        await context.MasterCodeItems.AddAsync(data);
        await context.SaveChangesAsync();
        return ResponseModel<NoResponseModel>.Success(response);
    }

    public async Task<ResponseModel<MasterCodeItemResponseModel>> GetByIdAsync(string id)
    {
        MasterCodeItemResponseModel response = new();

        var data = await context.MasterCodeItems.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        if (data is null)
        {
            return ResponseModel<MasterCodeItemResponseModel>.NotFound("Not found");
        }

        response.Value = data.Value;
        response.ValueMM = data.ValueMM ?? string.Empty;
        response.Description = data.Description ?? string.Empty;
        return ResponseModel<MasterCodeItemResponseModel>.Success(response);
    }

    public async Task<ResponseModel<NoResponseModel>> UpdateAsync(string id, UpdateMasterCodeItemRequestModel request)
    {
        NoResponseModel response = new();
        var data = await context.MasterCodeItems.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        if (data is null)
        {
            return ResponseModel<NoResponseModel>.NotFound("Not found");
        }
        data.Value = request.Value;
        data.ValueMM = request.ValueMM;
        data.Description = request.Description;
        data.UpdatedDate = DateTime.Now.ToMyanmarTime();
        context.MasterCodeItems.Update(data);
        await context.SaveChangesAsync();
        return ResponseModel<NoResponseModel>.Success(response);
    }

    public async Task<ResponseModel<NoResponseModel>> DeleteAsync(string id)
    {
        NoResponseModel response = new();
        var data = await context.MasterCodeItems.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        if (data is null)
        {
            return ResponseModel<NoResponseModel>.NotFound("Not found");
        }

        data.IsDeleted = true;
        data.UpdatedDate = DateTime.Now.ToMyanmarTime();
        context.MasterCodeItems.Update(data);
        await context.SaveChangesAsync();
        return ResponseModel<NoResponseModel>.Success(response);
    }
}