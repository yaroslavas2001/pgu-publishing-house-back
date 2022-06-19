using PublishingHouse.Interfaces.Model.Review;

namespace PublishingHouse.Interfaces;

public interface IReviewsService
{
	Task CheckAccessToReview(long publicationId, long reviewerId);

	Task<long> AddReviewAsync(long publicationId, string comment);

	Task<IReadOnlyCollection<ReviewModel>> GetPublicationReviews(long publicationId);

	Task RemoveReviewAsync(long reviewId);
}