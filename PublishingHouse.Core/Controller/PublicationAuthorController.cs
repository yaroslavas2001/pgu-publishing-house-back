using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublishingHouse.Interfaces;
using PublishingHouse.Interfaces.Model;

namespace PublishingHouse.Controller;

[Route("/[controller]")]
[Produces("application/json")]
public class PublicationAuthorController : Microsoft.AspNetCore.Mvc.Controller
{
	private readonly IPublicationAuthorService _publicationAuthorService;

	public PublicationAuthorController(IPublicationAuthorService publicationAuthorService)
	{
		_publicationAuthorService = publicationAuthorService;
	}

	/// <summary>
	///     Установить автора публикации
	/// </summary>
	/// <param name="publicationId"></param>
	/// <param name="authorId"></param>
	/// <returns></returns>
	[HttpPost]
	[Authorize]
	[Route($"{nameof(Add)}")]
	[ProducesResponseType(200, Type = typeof(BaseResponse))]
	[ProducesResponseType(400, Type = typeof(BaseResponse))]
	public async Task<BaseResponse> Add([FromQuery] long publicationId, [FromQuery] long authorId)
	{
		await _publicationAuthorService.SetPublicationAuthorAsync(publicationId, authorId);
		return new BaseResponse();
	}

	/// <summary>
	///     Получить авторов публикации
	/// </summary>
	/// <param name="publicationId"></param>
	/// <returns></returns>
	[HttpGet]
	[Route($"{nameof(Get)}")]
	[ProducesResponseType(200, Type = typeof(BaseResponse<IReadOnlyCollection<long>>))]
	[ProducesResponseType(400, Type = typeof(BaseResponse))]
	public async Task<BaseResponse<IReadOnlyCollection<long>>> Get([FromQuery] long publicationId)
	{
		var result = await _publicationAuthorService.GetPublicationAuthors(publicationId);
		return new BaseResponse<IReadOnlyCollection<long>>(result);
	}

	/// <summary>
	///     Удалить автора публикации
	/// </summary>
	/// <param name="publicationId"></param>
	/// <param name="authorId"></param>
	/// <returns></returns>
	[HttpDelete]
	[Authorize]
	[Route($"{nameof(Remove)}")]
	[ProducesResponseType(200, Type = typeof(BaseResponse))]
	[ProducesResponseType(400, Type = typeof(BaseResponse))]
	public async Task<BaseResponse> Remove([FromQuery] long publicationId, [FromQuery] long authorId)
	{
		await _publicationAuthorService.RemovePublicationAuthorAsync(publicationId, authorId);
		return new BaseResponse();
	}
}