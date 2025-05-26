using Shared.Models.Master.MasterCode;

namespace Domain.Modules.Master.MasterCode;

public class MasterCodeService(AppDbContext context) : IMasterCodeService
{
    public async Task<ResponseModel<PaginationResponse<MasterCodeResponseModel>>> ListAsync(PaginationRequest request)
    {
        var query = context.MasterCodes.Where(d => !d.IsDeleted).AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            query = query.Where(d =>
                EF.Functions.ILike(d.Name, $"%{request.Search}%") ||
                EF.Functions.ILike(d.NameMM, $"%{request.Search}%"));
        }

        query = query.ApplySorting(request.SortBy, request.IsAscending);

        var projectedQuery = query.Select(d => new MasterCodeResponseModel
        {
            Id = d.Id,
            Name = d.Name,
            NameMM = d.NameMM,
            Description = d.Description,
        });

        var paginated = await projectedQuery.ToPagedListAsync(request);
        return ResponseModel<PaginationResponse<MasterCodeResponseModel>>.Success(paginated);
    }
}