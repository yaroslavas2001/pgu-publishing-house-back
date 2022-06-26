using Microsoft.EntityFrameworkCore;
using PublishingHouse.Data;
using PublishingHouse.Data.Models;
using PublishingHouse.External.Mail;
using PublishingHouse.Interfaces;
using PublishingHouse.Interfaces.Enums;
using PublishingHouse.Interfaces.Extensions.Pagination;
using PublishingHouse.Interfaces.Model;
using PublishingHouse.Interfaces.Model.Reviewer;

namespace PublishingHouse.Services;

public class ReviewersService : IReviewersService
{
	private readonly DataContext _db;

	public ReviewersService(DataContext db)
	{
		_db = db;
	}

	public async Task<long> AddReviewerAsync(AddReviewerRequest request)
	{
		if (!MailService.IsValidEmailAddress(request.Email))
			throw new PublicationHouseException($"Reviewer email address = {request.Email} is not valid", EnumErrorCode.EmailIsNotValid);

		var reviewer = new Reviewer
		{
			FirstName = request.FirstName,
			LastName = request.LastName,
			SureName = request.SureName,
			Email = request.Email
		};

		await _db.Reviewers.AddAsync(reviewer);
		await _db.SaveChangesAsync();
		return reviewer.Id;
	}

	public async Task<GetReviewersResponse> GetReviewers(GetReviewersRequest request)
	{
		var query = _db.Reviewers.AsQueryable();

		if (request.PublicationId.HasValue)
			query = query.Where(x => x.Publications.Any(p => p.Id == request.PublicationId));

		if (request.ReviewerId.HasValue)
			query = query.Where(x => x.Id == request.ReviewerId);

		if (!string.IsNullOrWhiteSpace(request.Search))
		{
			var search = request.Search.Trim().ToLower();
			query = query.Where(x =>
				x.FirstName.ToLower().Contains(search)
				|| x.LastName.ToLower().Contains(search)
				|| x.SureName.ToLower().Contains(search)
				|| x.Email.ToLower().Contains(search));
		}

		return await query.GetPageAsync<GetReviewersResponse, Reviewer, ReviewerModel>(request,
			x => new ReviewerModel
			{
				Id = x.Id,
				FirstName = x.FirstName,
				SureName = x.SureName,
				LastName = x.LastName,
				Email = x.Email
			});
	}

	public async Task<IReadOnlyCollection<long>> GetReviewerPublications(long reviewerId)
	{
		return await _db.Publications.Where(x => x.ReviewerId == reviewerId).Select(x => x.Id).ToArrayAsync();
	}
}