namespace PublishingHouse.Interfaces.Model.Review;

public class AddReviewRequest
{
	public long PublicationId { get; set; }

	public string Comment { get; set; }
}