using Microsoft.EntityFrameworkCore;
using PublishingHouse.Data;
using PublishingHouse.Data.Models;
using PublishingHouse.Interfaces;
using PublishingHouse.Interfaces.Enums;
using PublishingHouse.Interfaces.Model;
using PublishingHouse.Interfaces.Model.Files;
using PublishingHouse.Interfaces.Model.Review;

namespace PublishingHouse.Services;

public class ReviewsService : IReviewsService
{
	private readonly DataContext _db;

	public ReviewsService(DataContext db)
	{
		_db = db;
	}

	public async Task CheckAccessToReview(long publicationId, long reviewerId)
	{
		if (!await _db.Publications.AnyAsync(x => x.Id == publicationId && x.ReviewerId == reviewerId))
			throw new PublicationHouseException(EnumErrorCode.AccessDenied);
	}

	public async Task<long> AddReviewAsync(long publicationId, string comment)
	{
		var review = new Review
		{
			PublicationId = publicationId,
			Comment = comment
		};
		await _db.Reviews.AddAsync(review);
		await _db.SaveChangesAsync();
		return review.Id;
	}

	public async Task<IReadOnlyCollection<ReviewModel>> GetPublicationReviews(long publicationId)
	{
		if (await _db.Publications.AllAsync(x => x.Id != publicationId))
			throw new PublicationHouseException($"Publication id = {publicationId} is not exists!", EnumErrorCode.EntityIsNotFound);

		return await _db.Reviews
			.Where(x => x.PublicationId == publicationId)
			.Select(x => new ReviewModel
			{
				Id = x.Id,
				Comment = x.Comment,
				Files = x.Files.Select(f => new PublicationFileModel
				{
					Name = f.Name,
					ReviewId = f.ReviewId,
					Type = f.Type,
					Url = f.Url
				}).ToArray()
			})
			.ToArrayAsync();
	}

	public async Task RemoveReviewAsync(long reviewId)
	{
		if (await _db.Reviews.AllAsync(x => x.Id != reviewId))
			throw new PublicationHouseException($"Review id = {reviewId} is not exists!", EnumErrorCode.EntityIsNotFound);

		_db.Reviews.Remove(new Review { Id = reviewId });
		await _db.SaveChangesAsync();
	}
}