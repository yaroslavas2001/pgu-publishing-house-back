using PublishingHouse.Interfaces.Exstensions.Pagination;

namespace PublishingHouse.Interfaces.Model.Author;

public class SearchAuthorResponse:IPaginationResponse<AuthorShortModel>
{
	public Page Page { get; set; }
	
	public long Count { get; set; }

	public IReadOnlyCollection<AuthorShortModel> Items { get; set; }
}