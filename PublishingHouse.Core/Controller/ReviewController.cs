using Microsoft.AspNetCore.Mvc;
using PublishingHouse.Interfaces;
using PublishingHouse.Interfaces.Model;
using PublishingHouse.Interfaces.Model.Review;

namespace PublishingHouse.Controller;

/// <summary>
///     Ревью
/// </summary>
[Route("/[controller]")]
[Produces("application/json")]
public class ReviewController : Microsoft.AspNetCore.Mvc.Controller
{
	private readonly IReviewsService _reviewsService;

	public ReviewController(IReviewsService reviewsService)
	{
		_reviewsService = reviewsService;
	}

	/// <summary>
	///     Добавить ревью
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	[HttpPost]
	[Route($"{nameof(Add)}")]
	[ProducesResponseType(200, Type = typeof(BaseResponse<long>))]
	[ProducesResponseType(400, Type = typeof(BaseResponse))]
	public async Task<BaseResponse<long>> Add([FromBody] AddReviewRequest request)
	{
		var result = await _reviewsService.AddReviewAsync(request.PublicationId, request.Comment);
		return new BaseResponse<long>(result);
	}

	/// <summary>
	///     Получить все ревью публикации
	/// </summary>
	/// <param name="publicationId"></param>
	/// <returns></returns>
	[HttpGet]
	[Route($"{nameof(Get)}")]
	[ProducesResponseType(200, Type = typeof(BaseResponse<IReadOnlyCollection<ReviewModel>>))]
	[ProducesResponseType(400, Type = typeof(BaseResponse))]
	public async Task<BaseResponse<IReadOnlyCollection<ReviewModel>>> Get([FromQuery] long publicationId)
	{
		var result = await _reviewsService.GetPublicationReviews(publicationId);
		return new BaseResponse<IReadOnlyCollection<ReviewModel>>(result);
	}

	/// <summary>
	///     Удалить ревью
	/// </summary>
	/// <param name="reviewId"></param>
	/// <returns></returns>
	[HttpGet]
	[Route($"{nameof(Delete)}")]
	[ProducesResponseType(200, Type = typeof(BaseResponse))]
	[ProducesResponseType(400, Type = typeof(BaseResponse))]
	public async Task<BaseResponse> Delete([FromQuery] long reviewId)
	{
		await _reviewsService.RemoveReviewAsync(reviewId);
		return new BaseResponse();
	}
}