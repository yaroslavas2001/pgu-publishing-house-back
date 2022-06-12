using PublishingHouse.Interfaces.Model;
using PublishingHouse.Interfaces.Model.Auth;

namespace PublishingHouse.Interfaces;

public interface IAuthService
{
	Task<BaseResponse<LoginResponse>> Login(LoginRequest request);

	Task<BaseResponse> Register(RegisterRequest request);

	Task<BaseResponse<TokenResponse>> ActivateAccount(ActivateAccountRequest request);
}