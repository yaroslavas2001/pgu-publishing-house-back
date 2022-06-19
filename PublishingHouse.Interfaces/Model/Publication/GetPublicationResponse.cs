using PublishingHouse.Interfaces.Exstensions.Pagination;

namespace PublishingHouse.Interfaces.Model.Publication;

public class GetPublicationResponse : IPaginationResponse<PublicationModel>
{
	public Page Page { get; set; }

	public long Count { get; set; }

	public IReadOnlyCollection<PublicationModel> Items { get; set; }
}