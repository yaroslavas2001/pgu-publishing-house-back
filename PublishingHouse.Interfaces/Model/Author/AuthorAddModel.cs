using PublishingHouse.StorageEnums;

namespace PublishingHouse.Interfaces.Model.Author;

public class AuthorAddModel
{
	public string FirstName { get; set; } = string.Empty;

	public string LastName { get; set; } = string.Empty;

	public string SureName { get; set; } = string.Empty;

	public string Contacts { get; set; }

	public string Email { get; set; }

	public bool IsTeacher { get; set; }

	public long? DepartmentId { get; set; }

	public EnumEmployeePosition? PositionId { get; set; }

	public EnumAcademicDegree? DegreeId { get; set; }

	public string? NonStuffPosition { get; set; }

	public string? NonStuffWorkPlace { get; set; }
}