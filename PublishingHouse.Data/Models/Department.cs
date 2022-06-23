namespace PublishingHouse.Data.Models;

/// <summary>
///     Кафедра
/// </summary>
public class Department
{
	public long Id { get; set; }

	public string Name { get; set; } = null!;

	public Faculty Faculty { get; set; } = null!;

	public long FacultyId { get; set; }

	public List<Author> Authors { get; set; } = null!;
}