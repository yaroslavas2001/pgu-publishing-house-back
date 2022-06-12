using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublishingHouse.Interfaces;
using PublishingHouse.Interfaces.Model;
using PublishingHouse.Interfaces.Model.Author;

namespace PublishingHouse.Controller;

[Route("/Author")]
[Produces("application/json")]
[Authorize]
public class AuthorController : Microsoft.AspNetCore.Mvc.Controller
{
	[HttpPost]
	[Route($"/Author/{nameof(Add)}")]
	[ProducesResponseType(200, Type = typeof(BaseResponse<AuthorShortResponse>))]
	[ProducesResponseType(400, Type = typeof(BaseResponse))]
	public async Task<BaseResponse<AuthorShortResponse>> Add([FromServices] IAuthorService service,
		AuthorAddRequest request)
	{
		try
		{
			return await service.Add(request);
		}
		catch (Exception e)
		{
			return new BaseResponse<AuthorShortResponse>(e);
		}
	}

	[HttpPost]
	[Route($"/Author/{nameof(Get)}")]
	[ProducesResponseType(200, Type = typeof(BaseResponse<AuthorShortResponse>))]
	[ProducesResponseType(400, Type = typeof(BaseResponse))]
	public async Task<BaseResponse<List<AuthorShortResponse>>> Get([FromServices] IAuthorService service,
		AuthorGetRequest request)
	{
		try
		{
			return await service.Get(request);
		}
		catch (Exception e)
		{
			return new BaseResponse<List<AuthorShortResponse>>(e);
		}
	}
}