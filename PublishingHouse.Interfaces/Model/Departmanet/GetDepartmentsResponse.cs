using PublishingHouse.Interfaces.Exstensions.Pagination;

namespace PublishingHouse.Interfaces.Model.Departmanet;

public class GetDepartmentsResponse : IPaginationResponse<DepartmentModel>
{
	public Page Page { get; set; }
	public long Count { get; set; }
	public IReadOnlyCollection<DepartmentModel> Items { get; set; }
}