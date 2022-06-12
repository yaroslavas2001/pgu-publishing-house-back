namespace PublishingHouse.Interfaces.Model.Author;

public class AuthorGetRequest : BaseGetRequest
{
	public string Search { get; set; } = string.Empty;
}