using PublishingHouse.StorageEnums;

namespace PublishingHouse.Interfaces.Model.Author;

public class AuthorModel
{
	public string SureName { get; set; }
	public string FirstName { get; set; }
	public string SecondName { get; set; }
	public string Email { get; set; }
	public long? DepartmentId { get; set; }
	public string Contacts { get; set; }
	public bool IsTeacher { get; set; }
	public EnumEmployeePosition? EmployeerPosition { get; set; }
	public EnumAcademicDegree? AcademicDegree { get; set; }
	public string NonStuffPosition { get; set; }
	public string NonStuffWorkPlace { get; set; }
}