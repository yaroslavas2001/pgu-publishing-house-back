using PublishingHouse.StorageEnums;

namespace PublishingHouse.Interfaces.Model.Publication;

public class UpdatePublicationModel
{

	public string UDC { get; set; } = string.Empty;

	public string Name { get; set; } = string.Empty;

	public string Tags { get; set; } = string.Empty;

	public EnumPublicationType Type { get; set; }

	public long UserId { get; set; }

	public long PublicationId { get; set; }

	public long? ReviewerId { get; set; }
}