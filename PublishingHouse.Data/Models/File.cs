using PublishingHouse.StorageEnums;

namespace PublishingHouse.Data.Models;

public class File
{
	public long Id { get; set; }

	public EnumFileType Type { get; set; }

	public string Url { get; set; } = string.Empty;

	public string Name { get; set; } = string.Empty;

	public long PublicationId { get; set; }

	public Publication Publication { get; set; } = null!;

	public bool IsVisibleForReviewers { get; set; } = false;

	public long? ReviewId { get; set; }

	public Review Review { get; set; } = null!;
}