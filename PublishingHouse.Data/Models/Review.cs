namespace PublishingHouse.Data.Models;

public class Review
{
	public long Id { get; set; }
	
	public long PublicationId { get; set; }
	
	public Publication Publication { get; set; }

	public string Comment { get; set; }

	public List<File> Files { get; set; }

}