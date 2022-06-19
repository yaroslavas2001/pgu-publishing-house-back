using PublishingHouse.Interfaces.Exstensions.Pagination;
using PublishingHouse.StorageEnums;

namespace PublishingHouse.Interfaces.Model.Publication;

public class GetPublicationsRequest : IPaginationRequest
{
	public long? PublicationId { get; set; }

	public EnumPublicationStatus? Status { get; set; }

	public EnumPublicationType? Type { get; set; }

	public long? UserId { get; set; }

	public long? ReviewerId { get; set; }

	public Page Page { get; set; }
}