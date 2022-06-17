namespace PublishingHouse.Interfaces.Model.Author;

public class AuthorAddModel
{
	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;
	public string FatherName { get; set; } = string.Empty;
	public string Contacts { get; set; }
	public string Email { get; set; }
	public bool IsTeacher { get; set; }
	public long DepartmentId { get; set; }
	public long PositionId { get; set; }
	public long DegreeId { get; set; }
}