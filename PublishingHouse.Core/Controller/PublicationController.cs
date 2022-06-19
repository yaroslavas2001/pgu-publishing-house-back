using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublishingHouse.Interfaces;
using PublishingHouse.Interfaces.Model;
using PublishingHouse.Interfaces.Model.Publication;
using PublishingHouse.StorageEnums;

namespace PublishingHouse.Controller;

[Route("/[controller]")]
[Produces("application/json")]
public class PublicationController : Microsoft.AspNetCore.Mvc.Controller
{
	private readonly IPublicationService _publication;

	public PublicationController(IPublicationService publication)
	{
		_publication = publication;
	}

	/// <summary>
	///     Добавить публикацию
	/// </summary>
	/// <param name="model"></param>
	/// <returns></returns>
	[HttpPost]
	[Authorize]
	[Route($"{nameof(Add)}")]
	[ProducesResponseType(200, Type = typeof(BaseResponse<long>))]
	[ProducesResponseType(400, Type = typeof(BaseResponse))]
	public async Task<BaseResponse<long>> Add([FromBody] AddPublicationModel model)
	{
		var result = await _publication.AddPublicationAsync(model);
		return new BaseResponse<long>(result);
	}

	/// <summary>
	///     Добавить публикацию
	/// </summary>
	/// <param name="model"></param>
	/// <returns></returns>
	[HttpGet]
	[Route($"{nameof(Get)}")]
	[ProducesResponseType(200, Type = typeof(BaseResponse<GetPublicationResponse>))]
	[ProducesResponseType(400, Type = typeof(BaseResponse))]
	public async Task<BaseResponse<GetPublicationResponse>> Get([FromQuery] GetPublicationsRequest model)
	{
		var result = await _publication.GetPublicationsAsync(model);
		return new BaseResponse<GetPublicationResponse>(result);
	}

	/// <summary>
	///     Установить статус публикации
	/// </summary>
	/// <param name="id"></param>
	/// <param name="status"></param>
	/// <returns></returns>
	[HttpPatch]
	[Authorize]
	[Route($"{nameof(SetStatus)}")]
	[ProducesResponseType(200, Type = typeof(BaseResponse))]
	[ProducesResponseType(400, Type = typeof(BaseResponse))]
	public async Task<BaseResponse> SetStatus([FromQuery] long id, [FromQuery] EnumPublicationStatus status)
	{
		await _publication.SetPublicationStatusAsync(id, status);
		return new BaseResponse();
	}

	/// <summary>
	///     Обновить публикацию
	/// </summary>
	/// <param name="model"></param>
	/// <returns></returns>
	[HttpPatch]
	[Authorize]
	[Route($"{nameof(Update)}")]
	[ProducesResponseType(200, Type = typeof(BaseResponse))]
	[ProducesResponseType(400, Type = typeof(BaseResponse))]
	public async Task<BaseResponse> Update([FromBody] UpdatePublicationModel model)
	{
		await _publication.UpdatePublicationAsync(model);
		return new BaseResponse();
	}
}