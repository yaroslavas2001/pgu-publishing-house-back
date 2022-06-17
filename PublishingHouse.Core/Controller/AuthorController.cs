using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublishingHouse.Interfaces;
using PublishingHouse.Interfaces.Model;
using PublishingHouse.Interfaces.Model.Author;

namespace PublishingHouse.Controller;

[Route("/[controller]")]
[Produces("application/json")]
public class AuthorController : Microsoft.AspNetCore.Mvc.Controller
{
	private readonly IAuthorService _authorService;

	public AuthorController(IAuthorService authorService)
	{
		_authorService = authorService;
	}

	[HttpPost]
	[Authorize]
	[Route($"{nameof(Add)}")]
	[ProducesResponseType(200, Type = typeof(BaseResponse<AuthorShortModel>))]
	[ProducesResponseType(400, Type = typeof(BaseResponse))]
	public async Task<BaseResponse<AuthorShortModel>> Add(AuthorAddModel model)
	{
		var response = await _authorService.Add(model);
		return new BaseResponse<AuthorShortModel>(response);
	}

	[HttpGet]
	[Route($"{nameof(Search)}")]
	[ProducesResponseType(200, Type = typeof(BaseResponse<IReadOnlyCollection<AuthorShortModel>>))]
	[ProducesResponseType(400, Type = typeof(BaseResponse))]
	public async Task<BaseResponse<IReadOnlyCollection<AuthorShortModel>>> Search([FromQuery] AuthorGetModel model)
	{
		var result = await _authorService.SearchAuthor(model);
		return new BaseResponse<IReadOnlyCollection<AuthorShortModel>>(result);
	}

	[HttpGet]
	[Route($"{nameof(Get)}")]
	[ProducesResponseType(200, Type = typeof(BaseResponse<IReadOnlyCollection<AuthorShortModel>>))]
	[ProducesResponseType(400, Type = typeof(BaseResponse))]
	public async Task<BaseResponse<IReadOnlyCollection<AuthorModel>>> Get([FromQuery] PaginationRequest page, [FromQuery] long? authorId = null)
	{
		var result = await _authorService.GetAuthorAsync(page, authorId);
		return new BaseResponse<IReadOnlyCollection<AuthorModel>>(result);
	}

	[HttpPatch]
	[Authorize]
	[Route($"{nameof(Update)}")]
	[ProducesResponseType(200, Type = typeof(BaseResponse))]
	[ProducesResponseType(400, Type = typeof(BaseResponse))]
	public async Task<BaseResponse> Update([FromBody] AuthorUpdateModel model)
	{
		await _authorService.Update(model);
		return new BaseResponse();
	}

	[HttpDelete]
	[Authorize]
	[Route($"{nameof(Delete)}")]
	[ProducesResponseType(200, Type = typeof(BaseResponse))]
	[ProducesResponseType(400, Type = typeof(BaseResponse))]
	public async Task<BaseResponse> Delete([FromQuery] long authorId)
	{
		await _authorService.Remove(authorId);
		return new BaseResponse();
	}
}