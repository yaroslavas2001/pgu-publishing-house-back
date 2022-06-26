using Microsoft.AspNetCore.Mvc;
using PublishingHouse.Interfaces;
using PublishingHouse.Interfaces.Model;
using PublishingHouse.Interfaces.Model.Auth;

namespace PublishingHouse.Controller;

/// <summary>
/// Авторизация
/// </summary>
[Route("/[controller]")]
[Produces("application/json")]
public class AuthController : Microsoft.AspNetCore.Mvc.Controller
{
	[HttpPost]
	[Route($"{nameof(Login)}")]
	[ProducesResponseType(200, Type = typeof(BaseResponse<LoginResponse>))]
	[ProducesResponseType(400, Type = typeof(BaseResponse))]
	public async Task<BaseResponse<LoginResponse>> Login([FromServices] IAuthService auth, LoginRequest request)
	{
		return await auth.Login(request);
	}

	[HttpPost]
	[Route($"{nameof(Register)}")]
	[ProducesResponseType(200, Type = typeof(BaseResponse))]
	[ProducesResponseType(400, Type = typeof(BaseResponse))]
	public async Task<BaseResponse> Register([FromServices] IAuthService auth, RegisterRequest request)
	{
		return await auth.Register(request);
	}

	[HttpPost]
	[Route($"{nameof(ActivateAccount)}")]
	[ProducesResponseType(200, Type = typeof(BaseResponse<TokenResponse>))]
	[ProducesResponseType(400, Type = typeof(BaseResponse))]
	public async Task<BaseResponse> ActivateAccount([FromServices] IAuthService auth, ActivateAccountRequest request)
	{
		return await auth.ActivateAccount(request);
	}
}