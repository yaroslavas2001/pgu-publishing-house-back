using PublishingHouse.Interfaces.Exstensions.Pagination;

namespace PublishingHouse.Interfaces.Model.Author;

public class GetAuthorResponse:IPaginationResponse<AuthorModel>
{
	public Page Page { get; set; }

	public long Count { get; set; }

	public IReadOnlyCollection<AuthorModel> Items { get; set; }
}