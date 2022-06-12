namespace PublishingHouse.Data.Models;

public class Reviewer
{
	public int Id { get; set; }

	public string FirstName { get; set; } = string.Empty;

	public string LastName { get; set; } = string.Empty;

	public string SureName { get; set; } = string.Empty;

	public string Email { get; set; } = string.Empty;

	public List<Publication> Publications { get; set; }
}