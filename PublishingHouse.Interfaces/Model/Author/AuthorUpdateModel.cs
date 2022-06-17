using PublishingHouse.StorageEnums;

namespace PublishingHouse.Interfaces.Model.Author;

public class AuthorUpdateModel
{
	public long AuthorId { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string SureName { get; set; }
	public string Email { get; set; }
	public EnumAcademicDegree? AcademicDegree { get; set; }
	public EnumEmployeePosition? EmployeerPosition { get; set; }
}