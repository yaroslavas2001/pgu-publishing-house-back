using PublishingHouse.StorageEnums;

namespace PublishingHouse.Interfaces.Model.Publication;

public class AddPublicationModel
{
	public string UDC;

	public string Name { get; set; }

	public string Tags { get; set; }

	public EnumPublicationType Type { get; set; }

	public long UserId { get; set; }
}