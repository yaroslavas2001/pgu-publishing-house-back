using PublishingHouse.Interfaces.Exstensions.Pagination;

namespace PublishingHouse.Interfaces.Model.Author;

public class GetAuthorsRequest : IPaginationRequest
{
	public long? AuthorId { get; set; } = null;
	public Page Page { get; set; } = new Page();
}