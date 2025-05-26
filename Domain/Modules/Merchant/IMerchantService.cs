using Domain.Services;
using Shared.Models.Merchants;

namespace Domain.Modules.Merchant;

public interface IMerchantService : ICrudWrapper<MerchantResponseModel, CreateMerchantRequestModel, UpdateMerchantRequestModel>
{

}