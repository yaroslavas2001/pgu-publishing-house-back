using Microsoft.EntityFrameworkCore;
using PublishingHouse.Data;
using PublishingHouse.Data.Models;
using PublishingHouse.Interfaces;
using PublishingHouse.Interfaces.Exstensions.Pagination;
using PublishingHouse.Interfaces.Model.Publication;
using PublishingHouse.StorageEnums;

namespace PublishingHouse.Services;

public class PublicationService : IPublicationService
{
	private readonly DataContext _db;

	public PublicationService(DataContext db)
	{
		_db = db;
	}

	public async Task<long> AddPublicationAsync(AddPublicationModel model)
	{
		var publication = new Publication
		{
			Name = model.Name,
			Status = EnumPublicationStatus.Draft,
			Tags = model.Tags,
			Type = model.Type,
			UDC = model.UDC,
			UserId = model.UserId
		};

		await _db.Publications.AddAsync(publication);
		await _db.SaveChangesAsync();
		return publication.Id;
	}

	public async Task<GetPublicationResponse> GetPublicationsAsync(GetPublicationsRequest request)
	{
		var query = request.PublicationId.HasValue
			? _db.Publications.Where(x => x.Id == request.PublicationId)
			: _db.Publications;

		if (request.Status.HasValue)
			query = query.Where(x => x.Status == request.Status);

		if (request.Type.HasValue)
			query = query.Where(x => x.Type == request.Type);

		if (request.ReviewerId.HasValue)
			query = query.Where(x => x.ReviewerId == request.ReviewerId);

		if (request.UserId.HasValue)
			query = query.Where(x => x.UserId == request.UserId);

		return await query.GetPageAsync<GetPublicationResponse, Publication, PublicationModel>(request,
			x => new PublicationModel
			{
				Id = x.Id,
				Name = x.Name,
				Type = x.Type,
				ReviewerId = x.ReviewerId,
				Status = x.Status,
				Tags = x.Tags,
				UDC = x.UDC,
				UserId = x.UserId
			});
	}

	public async Task UpdatePublicationAsync(UpdatePublicationModel model)
	{
		var publication = await _db.Publications.FirstOrDefaultAsync(x => x.Id == model.PublicationId);
		if (publication == null)
			throw new Exception($"Publication id = {model.PublicationId} is not found");

		if (!string.IsNullOrWhiteSpace(model.Name))
			publication.Name = model.Name;

		if (!string.IsNullOrWhiteSpace(model.Tags))
			publication.Tags = model.Tags;

		if (!string.IsNullOrWhiteSpace(model.UDC))
			publication.UDC = model.UDC;

		if (model.ReviewerId.HasValue)
			publication.ReviewerId = model.ReviewerId;

		publication.Type = model.Type;

		await _db.SaveChangesAsync();
	}

	public async Task SetPublicationStatusAsync(long publicationId, EnumPublicationStatus status)
	{
		var publication = await _db.Publications.FirstOrDefaultAsync(x => x.Id == publicationId);
		if (publication == null)
			throw new Exception($"Publication id = {publicationId} is not found");

		publication.Status = status;
		await _db.SaveChangesAsync();
	}
}