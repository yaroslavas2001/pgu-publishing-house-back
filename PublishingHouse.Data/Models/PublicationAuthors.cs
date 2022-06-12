namespace PublishingHouse.Data.Models;

public class PublicationAuthors
{
	public int PublicationId { get; set; }

	public int AuthorId { get; set; }

	public Publication Publication { get; set; }

	public Author Author { get; set; }
}