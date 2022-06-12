namespace PublishingHouse.Interfaces.Model.Author;

public class AuthorShortResponse
{
	public long Id { get; set; }
	public string FirstName { get; set; } = string.Empty;
	public string FatgerName { get; set; } = string.Empty;
	public string SecondName { get; set; } = string.Empty;
}