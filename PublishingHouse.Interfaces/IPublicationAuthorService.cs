namespace PublishingHouse.Interfaces;

public interface IPublicationAuthorService
{
	Task SetPublicationAuthorAsync(long publicationId, long authorId);

	Task<IReadOnlyCollection<long>> GetPublicationAuthors(long publicationId);

	Task RemovePublicationAuthorAsync(long publicationId, long authorId);
}