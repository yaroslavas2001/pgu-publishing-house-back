using PublishingHouse.Interfaces.Extensions.Pagination;

namespace PublishingHouse.Interfaces.Model.Reviewer;

public class GetReviewersResponse : IPaginationResponse<ReviewerModel>
{
	public Page Page { get; set; } = new Page();

	public long Count { get; set; }

	public IReadOnlyCollection<ReviewerModel> Items { get; set; }
}