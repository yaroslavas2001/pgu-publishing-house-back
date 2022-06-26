using PublishingHouse.Interfaces.Extensions.Pagination;

namespace PublishingHouse.Interfaces.Model.Faculty;

public class GetFacultyRequest : IPaginationRequest
{
	public Page Page { get; set; } = new Page();
}