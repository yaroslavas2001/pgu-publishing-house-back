using PublishingHouse.StorageEnums;

namespace PublishingHouse.Interfaces.Model.Author;

public class AuthorAddModel
{
	public string FirstName { get; set; } = string.Empty;

	public string LastName { get; set; } = string.Empty;

	public string SureName { get; set; } = string.Empty;

	public string Contacts { get; set; } = string.Empty;

	public string Email { get; set; } = string.Empty;

	public bool IsTeacher { get; set; }

	public long? DepartmentId { get; set; } = null;

	public EnumEmployeePosition? PositionId { get; set; } = null;

	public EnumAcademicDegree? DegreeId { get; set; } = null;

	public string? NonStuffPosition { get; set; } = null;

	public string? NonStuffWorkPlace { get; set; } = null;
}