using Microsoft.AspNetCore.Mvc;
using PublishingHouse.Interfaces;
using PublishingHouse.Interfaces.Model;
using PublishingHouse.Interfaces.Model.Auth;

namespace PublishingHouse.Controller;

[Route("/Auth")]
[Produces("application/json")]
public class AuthController : Microsoft.AspNetCore.Mvc.Controller
{
	[HttpPost]
	[Route($"/Auth/{nameof(Login)}")]
	[ProducesResponseType(200, Type = typeof(BaseResponse<LoginResponse>))]
	[ProducesResponseType(400, Type = typeof(BaseResponse))]
	public async Task<BaseResponse<LoginResponse>> Login([FromServices] IAuthService auth, LoginRequest request)
	{
		try
		{
			return await auth.Login(request);
		}
		catch (Exception e)
		{
			return new BaseResponse<LoginResponse>(e);
		}
	}

	[HttpPost]
	[Route($"/Auth/{nameof(Register)}")]
	[ProducesResponseType(200, Type = typeof(BaseResponse))]
	[ProducesResponseType(400, Type = typeof(BaseResponse))]
	public async Task<BaseResponse> Register([FromServices] IAuthService auth, RegisterRequest request)
	{
		try
		{
			return await auth.Register(request);
		}
		catch (Exception e)
		{
			return new BaseResponse<TokenResponse>(e);
		}
	}

	[HttpPost]
	[Route($"/Auth/{nameof(ActivateAccount)}")]
	[ProducesResponseType(200, Type = typeof(BaseResponse<TokenResponse>))]
	[ProducesResponseType(400, Type = typeof(BaseResponse))]
	public async Task<BaseResponse> ActivateAccount([FromServices] IAuthService auth, ActivateAccountRequest request)
	{
		try
		{
			return await auth.ActivateAccount(request);
		}
		catch (Exception e)
		{
			return new BaseResponse<TokenResponse>(e);
		}
	}
}