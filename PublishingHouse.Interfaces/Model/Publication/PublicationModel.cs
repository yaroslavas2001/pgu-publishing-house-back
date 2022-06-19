using PublishingHouse.StorageEnums;

namespace PublishingHouse.Interfaces.Model.Publication;

public class PublicationModel
{
	public string UDC;
	public long Id { get; set; }

	public string Name { get; set; }

	public string Tags { get; set; }

	public EnumPublicationType Type { get; set; }

	public EnumPublicationStatus Status { get; set; }

	public long? ReviewerId { get; set; }

	public long UserId { get; set; }
}