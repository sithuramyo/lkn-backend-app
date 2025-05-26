using Persistence.DataModels.Auth;
using Shared.Models.SystemUser;

namespace Domain.Modules.SystemUser;

public class SystemUserService(AppDbContext context) : ISystemUserService
{
    public async Task<ResponseModel<PaginationResponse<SystemUserResponseModel>>> ListAsync(PaginationRequest request)
    {
        var query = context.Admins.Where(d => !d.IsDeleted).AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            query = query.Where(d =>
                EF.Functions.ILike(d.Name, $"%{request.Search}%") ||
                EF.Functions.ILike(d.Email, $"%{request.Search}%"));
        }

        query = query.ApplySorting(request.SortBy, request.IsAscending);

        var projectedQuery = query.Select(d => new SystemUserResponseModel
        {
            Id = d.Id,
            Name = d.Name,
            Email = d.Email,
            Role = d.Role
        });

        var paginated = await projectedQuery.ToPagedListAsync(request);
        return ResponseModel<PaginationResponse<SystemUserResponseModel>>.Success(paginated);
    }

    public async Task<ResponseModel<NoResponseModel>> CreateAsync(CreateSystemUserRequestModel request)
    {
        NoResponseModel response = new();
        var isExist = await context.Admins.AnyAsync(x => x.Email == request.Email && !x.IsDeleted);
        if (isExist)
        {
            return ResponseModel<NoResponseModel>.BadRequest("Email is already exist.");
        }

        Admin data = new()
        {
            Name = request.Name,
            Email = request.Email,
            Password = PasswordHelper.HashPassword(request.Password),
            Role = request.Role,
        };
        await context.Admins.AddAsync(data);
        await context.SaveChangesAsync();
        return ResponseModel<NoResponseModel>.Success(response);
    }

    public async Task<ResponseModel<SystemUserResponseModel>> GetByIdAsync(string id)
    {
        SystemUserResponseModel response = new();
        var data = await context.Admins.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        if (data is null)
        {
            return ResponseModel<SystemUserResponseModel>.NotFound("Not found");
        }
        response.Name = data.Name;
        response.Email = data.Email;
        response.Role = data.Role;
        return ResponseModel<SystemUserResponseModel>.Success(response);
    }

    public async Task<ResponseModel<NoResponseModel>> UpdateAsync(string id, UpdateSystemUserRequestModel request)
    {
        NoResponseModel response = new();
        var data = await context.Admins.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        if (data is null)
        {
            return ResponseModel<NoResponseModel>.NotFound("Not found");
        }

        var isExist = await context.Admins
            .AnyAsync(x => x.Email == request.Email && x.Id != id && !x.IsDeleted);
        
        if (isExist)
        {
            return ResponseModel<NoResponseModel>.BadRequest("Email already exist");
        }
        
        data.Name = request.Name;
        data.Email = request.Email;
        data.Password = string.IsNullOrEmpty(request.Password) ? data.Password : PasswordHelper.HashPassword(request.Password);
        data.Role = request.Role;
        context.Admins.Update(data);
        await context.SaveChangesAsync();
        return ResponseModel<NoResponseModel>.Success(response);
    }

    public async Task<ResponseModel<NoResponseModel>> DeleteAsync(string id)
    {
        NoResponseModel response = new();
        var data = await context.Admins.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        if (data is null)
        {
            return ResponseModel<NoResponseModel>.NotFound("Not found");
        }

        data.IsDeleted = true;
        context.Admins.Update(data);
        await context.SaveChangesAsync();
        return ResponseModel<NoResponseModel>.Success(response);
    }
}