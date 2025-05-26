namespace Domain.Modules.Auth;

public interface IAuthService
{
    Task<ResponseModel<LoginResponseModel>> AdminLoginAsync(LoginRequestModel request);
    Task<ResponseModel<LoginResponseModel>> ConsumerLoginAsync(LoginRequestModel request);
    Task<ResponseModel<LoginResponseModel>> ConsumerSocialLoginAsync(SocialLoginRequestModel request);
    Task<ResponseModel<NoResponseModel>> LogoutAsync(string jti);
}