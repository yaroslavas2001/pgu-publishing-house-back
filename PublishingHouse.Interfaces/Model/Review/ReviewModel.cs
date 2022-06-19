using PublishingHouse.Interfaces.Model.Files;

namespace PublishingHouse.Interfaces.Model.Review;

public class ReviewModel
{
	public long Id { get; set; }

	public string Comment { get; set; }

	public IReadOnlyCollection<PublicationFileModel> Files { get; set; }
}