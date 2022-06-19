namespace PublishingHouse.Interfaces.Model.Publication;

public class UpdatePublicationModel : AddPublicationModel
{
	public long PublicationId { get; set; }

	public long? ReviewerId { get; set; }
}