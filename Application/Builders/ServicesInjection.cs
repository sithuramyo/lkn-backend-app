using Domain.Modules.Auth;
using Domain.Modules.Master.MasterCode;
using Domain.Modules.Master.MasterCodeItem;
using Domain.Modules.Merchant;
using Domain.Modules.Product;
using Domain.Modules.SystemUser;
using Domain.Modules.WholeSale;
using Shared.Helpers;

namespace Application.Builders;

public static class ServicesInjection
{
    public static void InjectServices(this IServiceCollection services)
    {
        #region Token Helper
        services.AddScoped<TokenHelper>();
        #endregion
        
        #region Services Injection

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IMasterCodeService, MasterCodeService>();
        services.AddScoped<IMasterCodeItemService, MasterCodeItemService>();
        services.AddScoped<ISystemUserService, SystemUserService>();
        services.AddScoped<IMerchantService, MerchantService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IWholeSaleService, WholeSaleService>();

        #endregion
    }
}