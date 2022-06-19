using PublishingHouse.Interfaces.Exstensions.Pagination;

namespace PublishingHouse.Interfaces.Model.Departmanet;

public class GetDepartmentsRequest : IPaginationRequest
{
	public long? FacultyId { get; set; } = null;
	public Page Page { get; set; }
}