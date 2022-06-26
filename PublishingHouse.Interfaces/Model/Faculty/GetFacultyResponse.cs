using PublishingHouse.Interfaces.Extensions.Pagination;

namespace PublishingHouse.Interfaces.Model.Faculty;

public class GetFacultyResponse : IPaginationResponse<FacultyShortModel>
{
	/// <summary>
	///     Параметры постранички
	/// </summary>
	public Page Page { get; set; } = new Page();

	/// <summary>
	///     Количество
	/// </summary>
	public long Count { get; set; }

	/// <summary>
	///     Факультеты
	/// </summary>
	public IReadOnlyCollection<FacultyShortModel> Items { get; set; }
}