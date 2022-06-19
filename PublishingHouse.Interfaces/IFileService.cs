using PublishingHouse.Interfaces.Model.Files;

namespace PublishingHouse.Interfaces;

public interface IFileService
{
	Task<string> AddFileAsync(AddFileModel? model);

	Task<IReadOnlyCollection<PublicationFileModel>> GetPublicationFilesAsync(long publicationId, bool isReviewer);
}