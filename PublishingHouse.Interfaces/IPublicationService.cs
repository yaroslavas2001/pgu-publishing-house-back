using PublishingHouse.Interfaces.Model.Publication;
using PublishingHouse.StorageEnums;

namespace PublishingHouse.Interfaces;

public interface IPublicationService
{
	Task<long> AddPublicationAsync(AddPublicationModel model);

	Task<GetPublicationResponse> GetPublicationsAsync(GetPublicationsRequest request);

	Task UpdatePublicationAsync(UpdatePublicationModel model);

	Task SetPublicationStatusAsync(long publicationId, EnumPublicationStatus status);
}