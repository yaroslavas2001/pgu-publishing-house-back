using PublishingHouse.Interfaces.Extensions.Pagination;

namespace PublishingHouse.Interfaces.Model.Departmanet;

public class GetDepartmentRequest : IPaginationRequest
{
	public long? DepartmanetId { get; set; } = null;

	public Page Page { get; set; } = new Page();
}