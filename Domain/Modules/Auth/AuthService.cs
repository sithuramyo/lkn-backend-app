using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Persistence.DataModels.Auth;
using Shared.Constants;
using Shared.Extensions;

namespace Domain.Modules.Auth;

public class AuthService(AppDbContext context, TokenHelper tokenHelper, IDistributedCache distributedCache)
    : IAuthService
{
    public async Task<ResponseModel<LoginResponseModel>> AdminLoginAsync(LoginRequestModel request)
    {
        var admin = await context.Admins.FirstOrDefaultAsync(x => x.Email == request.Email);

        if (admin is null || !PasswordHelper.VerifyPassword(request.Password, admin.Password))
        {
            return ResponseModel<LoginResponseModel>.BadRequest("Invalid email or password.");
        }

        var response = tokenHelper.GenerateToken(admin.Id, admin.Name, admin.Role);
        return ResponseModel<LoginResponseModel>.Success(response);
    }

    public async Task<ResponseModel<LoginResponseModel>> ConsumerLoginAsync(LoginRequestModel request)
    {
        var consumer = await context.Consumers.FirstOrDefaultAsync(x => x.Email == request.Email);

        if (consumer is null || !PasswordHelper.VerifyPassword(request.Password, consumer.Password!))
        {
            return ResponseModel<LoginResponseModel>.BadRequest("Invalid email or password.");
        }

        var response = tokenHelper.GenerateToken(consumer.ConsumerCode, consumer.Name);
        return ResponseModel<LoginResponseModel>.Success(response);
    }

    public async Task<ResponseModel<LoginResponseModel>> ConsumerSocialLoginAsync(SocialLoginRequestModel request)
    {
        var consumer = await context.Consumers
            .FirstOrDefaultAsync(x => x.Email == request.Email && x.Provider == request.Provider);

        if (consumer is null)
        {
            var count = await context.Consumers.CountAsync();
            consumer = new Consumer
            {
                ConsumerCode = CodeConstants.CONSUMER_CODE.GetCode(count, CodeConstants.CONSUMER_CODE_DIGIT),
                Id = Guid.NewGuid().ToString(),
                Name = request.Name!,
                Email = request.Email,
                Type = request.Type,
                EmailVerified = request.EmailVerified,
                Provider = request.Provider,
                ProviderAccountId = request.ProviderAccountId,
                ProfilePicture = request.ProfilePicture,
                CreatedDate = DateTime.Now.ToMyanmarTime(),
            };

            context.Consumers.Add(consumer);
            await context.SaveChangesAsync();
        }

        var consumerName = string.IsNullOrEmpty(consumer.Name) ? consumer.ConsumerCode : consumer.Name;
        var response = tokenHelper.GenerateToken(consumer.ConsumerCode, consumerName);
        return ResponseModel<LoginResponseModel>.Success(response);
    }

    public async Task<ResponseModel<NoResponseModel>> LogoutAsync(string jti)
    {
        NoResponseModel response = new();
        if (string.IsNullOrEmpty(jti))
        {
            return ResponseModel<NoResponseModel>.BadRequest("Invalid token.");
        }

        var cacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = DateTimeOffset.UtcNow.AddDays(3)
        };
        await distributedCache.SetStringAsync(jti, "blacklisted", cacheOptions);
        return ResponseModel<NoResponseModel>.Success(response);
    }
}