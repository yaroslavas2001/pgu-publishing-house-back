using PublishingHouse.StorageEnums;

namespace PublishingHouse.Data.Models;

public class Publication
{
	public long Id { get; set; }

	public EnumPublicationStatus Status { get; set; }

	public string Name { get; set; } = string.Empty;

	public string Tags { get; set; } = string.Empty;

	public EnumPublicationType Type { get; set; } = EnumPublicationType.Article;

	public string UDC { get; set; } = string.Empty;

	public List<File> Files { get; set; }

	public User User { get; set; }

	public int UserId { get; set; }

	public List<PublicationAuthors> Authors { get; set; }

	public Reviewer Reviewer { get; set; }

	public int? ReviewerId { get; set; }
}