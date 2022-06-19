using PublishingHouse.Interfaces.Exstensions.Pagination;

namespace PublishingHouse.Interfaces.Model.Faculty;

public class GetFacultyResponse : IPaginationResponse<FacultyShortModel>
{
	/// <summary>
	///     Параметры постранички
	/// </summary>
	public Page Page { get; set; }

	/// <summary>
	///     Количество
	/// </summary>
	public long Count { get; set; }

	/// <summary>
	///     Факультеты
	/// </summary>
	public IReadOnlyCollection<FacultyShortModel> Items { get; set; }
}