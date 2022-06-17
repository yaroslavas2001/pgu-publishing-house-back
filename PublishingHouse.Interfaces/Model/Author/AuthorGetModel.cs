namespace PublishingHouse.Interfaces.Model.Author;

public class AuthorGetModel : PaginationRequest
{
	public string Search { get; set; } = string.Empty;
}