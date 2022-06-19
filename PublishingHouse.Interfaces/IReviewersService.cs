using PublishingHouse.Interfaces.Model.Reviewer;

namespace PublishingHouse.Interfaces;

public interface IReviewersService
{
	Task<long> AddReviewerAsync(AddReviewerRequest request);

	Task<GetReviewersResponse> GetReviewers(GetReviewersRequest request);

	Task<IReadOnlyCollection<long>> GetReviewerPublications(long reviewerId);
}