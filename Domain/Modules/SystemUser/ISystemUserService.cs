using Domain.Services;
using Shared.Models.SystemUser;

namespace Domain.Modules.SystemUser;

public interface ISystemUserService : ICrudWrapper<SystemUserResponseModel, CreateSystemUserRequestModel, UpdateSystemUserRequestModel>
{

}