using PublishingHouse.Interfaces.Extensions.Pagination;

namespace PublishingHouse.Interfaces.Model.Departmanet;

public class GetDepartmentsResponse : IPaginationResponse<DepartmentModel>
{
	public Page Page { get; set; } = new Page();

	public long Count { get; set; }

	public IReadOnlyCollection<DepartmentModel> Items { get; set; }
}