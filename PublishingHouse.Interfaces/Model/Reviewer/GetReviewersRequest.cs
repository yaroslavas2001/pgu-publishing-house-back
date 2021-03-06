using PublishingHouse.Interfaces.Extensions.Pagination;

namespace PublishingHouse.Interfaces.Model.Reviewer;

public class GetReviewersRequest : IPaginationRequest
{
	public string Search { get; set; } = string.Empty;

	public long? PublicationId { get; set; }

	public long? ReviewerId { get; set; }

	public Page Page { get; set; } = new Page();
}