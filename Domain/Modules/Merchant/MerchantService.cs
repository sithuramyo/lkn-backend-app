using Shared.Constants;
using Shared.Models.Merchants;

namespace Domain.Modules.Merchant;

public class MerchantService(AppDbContext context) : IMerchantService
{
    public async Task<ResponseModel<PaginationResponse<MerchantResponseModel>>> ListAsync(PaginationRequest request)
    {
        var query = context.Merchants
            .AsNoTracking()
            .Where(m => !m.IsDeleted);

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            query = query.Where(m =>
                EF.Functions.ILike(m.Name, $"%{request.Search}%") ||
                EF.Functions.ILike(m.MerchantCode, $"%{request.Search}%"));
        }

        query = query.ApplySorting(request.SortBy, request.IsAscending);

        var projectedQuery = from m in query
                             join state in context.MasterCodes.AsNoTracking() on m.StateId equals state.Id into stateGroup
                             from state in stateGroup.DefaultIfEmpty()
                             join township in context.MasterCodeItems.AsNoTracking() on m.TownShipId equals township.Id into
                                 townshipGroup
                             from township in townshipGroup.DefaultIfEmpty()
                             select new MerchantResponseModel
                             {
                                 Id = m.Id,
                                 Name = m.Name,
                                 MerchantCode = m.MerchantCode,
                                 PhoneNumber = m.PhoneNumber,
                                 State = state != null ? state.Name + "-" + state.NameMM : null,
                                 Township = township != null ? township.Value + "-" + township.ValueMM : null,
                                 Description = m.Description
                             };

        var paginated = await projectedQuery.ToPagedListAsync(request);
        return ResponseModel<PaginationResponse<MerchantResponseModel>>.Success(paginated);
    }

    public async Task<ResponseModel<NoResponseModel>> CreateAsync(CreateMerchantRequestModel request)
    {
        NoResponseModel response = new();
        var isPhoneExist = await context.Merchants.AnyAsync(x =>
            x.PhoneNumber == request.PhoneNumber && !x.IsDeleted);

        if (isPhoneExist)
        {
            return ResponseModel<NoResponseModel>.BadRequest("Phone number is already exist");
        }

        var count = await context.Merchants.CountAsync();

        Persistence.DataModels.Setup.Merchant data = new()
        {
            Id = Guid.NewGuid().ToString(),
            MerchantCode = CodeConstants.MERCHANT_CODE.GetCode(count, CodeConstants.OTHER_CODE_DIGIT),
            Name = request.Name,
            PhoneNumber = request.PhoneNumber,
            StateId = request.StateId,
            TownShipId = request.TownshipId,
            Description = request.Description,
        };
        await context.Merchants.AddAsync(data);
        await context.SaveChangesAsync();
        return ResponseModel<NoResponseModel>.Success(response);
    }

    public async Task<ResponseModel<MerchantResponseModel>> GetByIdAsync(string id)
    {
        MerchantResponseModel response = new();
        var data = await context.Merchants.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        if (data is null)
        {
            return ResponseModel<MerchantResponseModel>.NotFound("Not found");
        }

        response.MerchantCode = data.MerchantCode;
        response.Name = data.Name;
        response.PhoneNumber = data.PhoneNumber;
        response.StateId = data.StateId;
        response.TownshipId = data.TownShipId;
        response.Description = data.Description;
        return ResponseModel<MerchantResponseModel>.Success(response);
    }

    public async Task<ResponseModel<NoResponseModel>> UpdateAsync(string id, UpdateMerchantRequestModel request)
    {
        NoResponseModel response = new();
        var data = await context.Merchants.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        if (data is null)
        {
            return ResponseModel<NoResponseModel>.NotFound("Not found");
        }

        var isPhoneExist = await context.Merchants
            .AnyAsync(x => x.PhoneNumber == request.PhoneNumber
                           && x.Id != id && !x.IsDeleted);

        if (isPhoneExist)
        {
            return ResponseModel<NoResponseModel>.BadRequest("Phone number already exist");
        }


        data.Name = request.Name;
        data.PhoneNumber = request.PhoneNumber;
        data.StateId = request.StateId;
        data.TownShipId = request.TownshipId;
        data.Description = request.Description;
        context.Merchants.Update(data);
        await context.SaveChangesAsync();
        return ResponseModel<NoResponseModel>.Success(response);
    }

    public async Task<ResponseModel<NoResponseModel>> DeleteAsync(string id)
    {
        NoResponseModel response = new();
        var data = await context.Merchants.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        if (data is null)
        {
            return ResponseModel<NoResponseModel>.NotFound("Not found");
        }

        data.IsDeleted = true;
        context.Merchants.Update(data);
        await context.SaveChangesAsync();
        return ResponseModel<NoResponseModel>.Success(response);
    }
}