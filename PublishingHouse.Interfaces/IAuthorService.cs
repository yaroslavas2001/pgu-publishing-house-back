using PublishingHouse.Interfaces.Model;
using PublishingHouse.Interfaces.Model.Author;

namespace PublishingHouse.Interfaces;

public interface IAuthorService
{
	Task<AuthorShortModel> Add(AuthorAddModel model);

	Task<SearchAuthorResponse> SearchAuthor(AuthorGetModel model);

	Task<GetAuthorResponse> GetAuthorsAsync(GetAuthorsRequest request);

	Task Update(AuthorUpdateModel model);

	Task Remove(long id);
}