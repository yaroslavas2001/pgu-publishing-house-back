namespace PublishingHouse.Models.Department;

/// <summary>
///     Модель запроса добавления кафедры
/// </summary>
public class AddDepartmentRequest
{
	/// <summary>
	///     Название кафедры
	/// </summary>
	public string DepartmentName { get; set; }

	/// <summary>
	///     Идентификатор факультета
	/// </summary>
	public long FacultyId { get; set; }
}