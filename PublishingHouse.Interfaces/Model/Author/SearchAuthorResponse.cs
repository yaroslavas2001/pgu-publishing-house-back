using PublishingHouse.Interfaces.Extensions.Pagination;

namespace PublishingHouse.Interfaces.Model.Author;

public class SearchAuthorResponse : IPaginationResponse<AuthorShortModel>
{
	public Page Page { get; set; } = new Page();

	public long Count { get; set; }

	public IReadOnlyCollection<AuthorShortModel> Items { get; set; }
}