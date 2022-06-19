using Microsoft.EntityFrameworkCore;
using PublishingHouse.Data;
using PublishingHouse.Data.Models;
using PublishingHouse.Interfaces;
using PublishingHouse.Interfaces.Exstensions.Pagination;
using PublishingHouse.Interfaces.Model.Author;
using PublishingHouse.StorageEnums;

namespace PublishingHouse.Services;

public class AuthorService : IAuthorService
{
	private readonly DataContext _db;

	public AuthorService(DataContext db)
	{
		_db = db;
	}

	public async Task<AuthorShortModel> Add(AuthorAddModel model)
	{
		var add = new Author
		{
			Email = model.Email,
			Contacts = model.Contacts,
			SureName = model.FatherName,
			FirstName = model.FirstName,
			IsTeacher = model.IsTeacher,
			LastName = model.LastName
		};

		if (model.IsTeacher)
		{
			add.AcademicDegree = (EnumAcademicDegree) model.DegreeId;
			add.DepartmentId = model.DepartmentId;
			add.EmployeerPosition = (EnumEmployeePosition) model.PositionId;
		}
		else
		{
			add.DepartmentId = null;
			add.EmployeerPosition = null;
			add.AcademicDegree = null;
		}

		await _db.AddAsync(add);
		await _db.SaveChangesAsync();

		return new AuthorShortModel
		{
			SureName = add.SureName,
			FirstName = add.FirstName,
			Id = add.Id,
			SecondName = add.LastName
		};
	}

	public async Task<SearchAuthorResponse> SearchAuthor(AuthorGetModel model)
	{
		return await _db.Authors
			.Where(x =>
				x.SureName.Contains(model.Search)
				|| x.FirstName.Contains(model.Search)
				|| x.LastName.Contains(model.Search)
			).GetPageAsync<SearchAuthorResponse, Author, AuthorShortModel>(model, x => new AuthorShortModel
			{
				Id = x.Id,
				FirstName = x.FirstName,
				SecondName = x.LastName,
				SureName = x.SureName
			});
	}


	public async Task<GetAuthorResponse> GetAuthorsAsync(GetAuthorsRequest request) //todo PAGINATION
	{
		var query = _db.Authors.AsQueryable();

		if (request.AuthorId.HasValue)
			query = query.Where(x => x.Id == request.AuthorId);

		return await query.GetPageAsync<GetAuthorResponse, Author, AuthorModel>(request, x => new AuthorModel
		{
			SureName = x.SureName,
			FirstName = x.FirstName,
			SecondName = x.LastName,
			Email = x.Email,
			DepartmentId = x.DepartmentId,
			Contacts = x.Contacts,
			IsTeacher = x.IsTeacher,
			EmployeerPosition = x.EmployeerPosition,
			AcademicDegree = x.AcademicDegree,
			NonStuffPosition = x.NonStuffPosition,
			NonStuffWorkPlace = x.NonStuffWorkPlace
		});
	}

	public async Task Update(AuthorUpdateModel model)
	{
		var author = await _db.Authors.FirstOrDefaultAsync(x => x.Id == model.AuthorId);
		if (author is null)
			throw new Exception($"Author Id = {model.AuthorId} is not found!");

		if (!string.IsNullOrWhiteSpace(model.FirstName))
			author.FirstName = model.FirstName;

		if (!string.IsNullOrWhiteSpace(model.LastName))
			author.LastName = model.LastName;

		if (!string.IsNullOrWhiteSpace(model.SureName))
			author.SureName = model.SureName;

		if (!string.IsNullOrWhiteSpace(model.Email))
			author.Email = model.Email;

		if (model.AcademicDegree.HasValue)
			author.AcademicDegree = model.AcademicDegree;

		if (model.EmployeerPosition.HasValue)
			author.EmployeerPosition = model.EmployeerPosition;

		await _db.SaveChangesAsync();
	}

	public async Task Remove(long id)
	{
		if (await _db.Authors.AllAsync(x => x.Id != id))
			throw new Exception($"Author id = {id} is not exists!");

		if (await _db.PublicationsAuthors.AnyAsync(x => x.AuthorId == id))
			throw new Exception("Author with publications can't be deleted!");

		_db.Authors.Remove(new Author {Id = id});
		await _db.SaveChangesAsync();
	}
}