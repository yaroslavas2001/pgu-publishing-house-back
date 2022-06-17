namespace PublishingHouse.Interfaces.Model.Author;

public class AuthorShortModel
{
	public long Id { get; set; }
	public string FirstName { get; set; } = string.Empty;
	public string SureName { get; set; } = string.Empty;
	public string SecondName { get; set; } = string.Empty;
}