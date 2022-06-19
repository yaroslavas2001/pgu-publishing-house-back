using PublishingHouse.Interfaces.Exstensions.Pagination;

namespace PublishingHouse.Interfaces.Model.Reviewer;

public class GetReviewersRequest : IPaginationRequest
{
	public string Search { get; set; }

	public long? PublicationId { get; set; }

	public long? ReviewerId { get; set; }

	public Page Page { get; set; }
}