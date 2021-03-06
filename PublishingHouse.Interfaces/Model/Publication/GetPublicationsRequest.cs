using PublishingHouse.Interfaces.Extensions.Pagination;
using PublishingHouse.StorageEnums;

namespace PublishingHouse.Interfaces.Model.Publication;

public class GetPublicationsRequest : IPaginationRequest
{
	public string Search { get; set; }

	public long? PublicationId { get; set; }

	public EnumPublicationStatus? Status { get; set; }

	public EnumPublicationType? Type { get; set; }

	public long? UserId { get; set; }

	public long? ReviewerId { get; set; }

	public bool ExcludeDraft { get; set; } = false;

	public Page Page { get; set; } = new Page();
}