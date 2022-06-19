using PublishingHouse.Interfaces.Exstensions.Pagination;

namespace PublishingHouse.Interfaces.Model.Reviewer;

public class GetReviewersResponse : IPaginationResponse<ReviewerModel>
{
	public Page Page { get; set; }

	public long Count { get; set; }

	public IReadOnlyCollection<ReviewerModel> Items { get; set; }
}