using System.Net;
using Microsoft.EntityFrameworkCore;
using PublishingHouse.Data;
using PublishingHouse.Data.Models;
using PublishingHouse.External.Mail;
using PublishingHouse.Interfaces;
using PublishingHouse.Interfaces.Enums;
using PublishingHouse.Interfaces.Extensions.Pagination;
using PublishingHouse.Interfaces.Model;
using PublishingHouse.Interfaces.Model.Publication;
using PublishingHouse.StorageEnums;

namespace PublishingHouse.Services;

public class PublicationService : IPublicationService
{
	private readonly DataContext _db;
	private readonly MailService _mailService;

	public PublicationService(DataContext db)
	{
		_db = db;
		_mailService = new();
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

		if (!string.IsNullOrWhiteSpace(request.Search))
			query = query.Where(x =>
				x.Name.ToLower().Contains(request.Search.ToLower())
				|| x.Tags.ToLower().Contains(request.Search.ToLower()));

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
			throw new PublicationHouseException($"Publication id = {model.PublicationId} is not found", EnumErrorCode.EntityIsNotFound);

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

		if (model.ReviewerId.HasValue)
			await SendMailToReviewer(publication);
	}

	public async Task SetPublicationStatusAsync(long publicationId, EnumPublicationStatus status)
	{
		var publication = await _db.Publications.FirstOrDefaultAsync(x => x.Id == publicationId);
		if (publication == null)
			throw new PublicationHouseException($"Publication id = {publicationId} is not found", EnumErrorCode.EntityIsNotFound);

		publication.Status = status;
		await _db.SaveChangesAsync();
	}

	private async Task SendMailToReviewer(Publication publication)
	{
		var reviewer = await _db.Reviewers.FirstOrDefaultAsync(x => x.Id == publication.ReviewerId);

		if (string.IsNullOrWhiteSpace(reviewer?.Email) || !MailService.IsValidEmailAddress(reviewer.Email))
			throw new PublicationHouseException("Reviewer email is not valid!", EnumErrorCode.EmailIsNotValid);

		var host = Dns.GetHostName(); //todo make it normally

		var emailParams = new List<KeyValuePair<string, string>>
		{
			new( "@Reviewer", $" {reviewer.SureName} {reviewer.FirstName} {reviewer.LastName}"),
			new( "@Publication", $"{publication.Name}"),
			new( "@Url", $"{host}\\Publication\\get?publicationId={publication.Id}")
		};

		await _mailService.SendEvent(reviewer.Email, "addReviewer", emailParams);
	}
}