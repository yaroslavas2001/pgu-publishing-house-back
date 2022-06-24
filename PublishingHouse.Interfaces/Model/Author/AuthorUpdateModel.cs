using PublishingHouse.StorageEnums;

namespace PublishingHouse.Interfaces.Model.Author;

public class AuthorUpdateModel
{
	public long AuthorId { get; set; }
	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;
	public string SureName { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public EnumAcademicDegree? AcademicDegree { get; set; }
	public EnumEmployeePosition? EmployerPosition { get; set; }
}