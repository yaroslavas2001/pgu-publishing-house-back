using PublishingHouse.StorageEnums;

namespace PublishingHouse.Interfaces.Model.Files;

public class PublicationFileModel
{
	public string Name { get; set; }

	public EnumFileType Type { get; set; }

	public string Url { get; set; }

	public long? ReviewId { get; set; }
}