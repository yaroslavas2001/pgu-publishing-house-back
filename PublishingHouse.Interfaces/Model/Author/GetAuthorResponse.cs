using PublishingHouse.Interfaces.Extensions.Pagination;

namespace PublishingHouse.Interfaces.Model.Author;

public class GetAuthorResponse : IPaginationResponse<AuthorModel>
{
	public Page Page { get; set; } = new Page();

	public long Count { get; set; }

	public IReadOnlyCollection<AuthorModel> Items { get; set; }
}