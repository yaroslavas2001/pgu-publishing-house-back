using PublishingHouse.Interfaces.Exstensions.Pagination;

namespace PublishingHouse.Interfaces.Model.Faculty;

public class GetFacultyRequest : IPaginationRequest
{
	public Page Page { get; set; }
}