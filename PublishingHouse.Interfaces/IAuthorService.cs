using PublishingHouse.Interfaces.Model;
using PublishingHouse.Interfaces.Model.Author;

namespace PublishingHouse.Interfaces;

public interface IAuthorService
{
	Task<AuthorShortModel> Add(AuthorAddModel model);

	Task<IReadOnlyCollection<AuthorShortModel>> SearchAuthor(AuthorGetModel model);

	Task<IReadOnlyCollection<AuthorModel>> GetAuthorAsync(PaginationRequest page, long? authorId = null);

	Task Update(AuthorUpdateModel model);

	Task Remove(long id);
}