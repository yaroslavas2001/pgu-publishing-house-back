using Microsoft.AspNetCore.Mvc;
using PublishingHouse.Interfaces;
using PublishingHouse.Interfaces.Model;
using PublishingHouse.Interfaces.Model.Reviewer;

namespace PublishingHouse.Controller;

/// <summary>
///     Ревьюеры
/// </summary>
[Route("/[controller]")]
[Produces("application/json")]
public class ReviewerController : Microsoft.AspNetCore.Mvc.Controller
{
	private readonly IReviewersService _reviewersService;

	public ReviewerController(IReviewersService reviewersService)
	{
		_reviewersService = reviewersService;
	}

	/// <summary>
	///     Добавить ревьюера
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	[HttpPost]
	[Route($"{nameof(Add)}")]
	[ProducesResponseType(200, Type = typeof(BaseResponse<long>))]
	[ProducesResponseType(400, Type = typeof(BaseResponse))]
	public async Task<BaseResponse<long>> Add([FromBody] AddReviewerRequest request)
	{
		var result = await _reviewersService.AddReviewerAsync(request);
		return new BaseResponse<long>(result);
	}

	/// <summary>
	///     Получить ревьюеров
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	[HttpGet]
	[Route($"{nameof(Get)}")]
	[ProducesResponseType(200, Type = typeof(BaseResponse<GetReviewersResponse>))]
	[ProducesResponseType(400, Type = typeof(BaseResponse))]
	public async Task<BaseResponse<GetReviewersResponse>> Get([FromQuery] GetReviewersRequest request)
	{
		var result = await _reviewersService.GetReviewers(request);
		return new BaseResponse<GetReviewersResponse>(result);
	}

	/// <summary>
	///     Получить идентификаторы публикаций ревьюера
	/// </summary>
	/// <param name="reviewerId"></param>
	/// <returns></returns>
	[HttpGet]
	[Route($"{nameof(GetPublications)}")]
	[ProducesResponseType(200, Type = typeof(BaseResponse<IReadOnlyCollection<long>>))]
	[ProducesResponseType(400, Type = typeof(BaseResponse))]
	public async Task<BaseResponse<IReadOnlyCollection<long>>> GetPublications([FromQuery] long reviewerId)
	{
		var result = await _reviewersService.GetReviewerPublications(reviewerId);
		return new BaseResponse<IReadOnlyCollection<long>>(result);
	}
}