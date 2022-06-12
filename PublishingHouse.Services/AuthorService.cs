using Microsoft.EntityFrameworkCore;
using PublishingHouse.Data;
using PublishingHouse.Data.Models;
using PublishingHouse.Interfaces;
using PublishingHouse.Interfaces.Model;
using PublishingHouse.Interfaces.Model.Author;
using PublishingHouse.Services.Extensions;
using PublishingHouse.StorageEnums;

namespace PublishingHouse.Services;

public class AuthorService : IAuthorService
{
	private readonly DataContext _db;

	public AuthorService(DataContext db)
	{
		_db = db;
	}

	public async Task<BaseResponse<AuthorShortResponse>> Add(AuthorAddRequest request)
	{
		var add = new Author
		{
			Email = request.Email,
			Contacts = request.Contacts,
			SureName = request.FatherName,
			FirstName = request.FirstName,
			IsTeacher = request.IsTeacher,
			LastName = request.LastName
		};

		if (request.IsTeacher)
		{
			add.AcademicDegree = (EnumAcademicDegree) request.DegreeId;
			add.DepartmentId = request.DepartmentId;
			add.EmployeerPosition = (EnumEmployeePosition) request.PositionId;
		}
		else
		{
			add.DepartmentId = null;
			add.EmployeerPosition = null;
			add.AcademicDegree = null;
		}

		await _db.AddAsync(add);
		await _db.SaveChangesAsync();

		return new BaseResponse<AuthorShortResponse>(new AuthorShortResponse
		{
			FatgerName = add.SureName,
			FirstName = add.FirstName,
			Id = add.Id,
			SecondName = add.LastName
		});
	}

	public async Task<BaseResponse<List<AuthorShortResponse>>> Get(AuthorGetRequest request)
	{
		return new BaseResponse<List<AuthorShortResponse>>(
			await
				_db
					.Authors
					.Where(x =>
						x.SureName.Contains(request.Search)
						|| x.FirstName.Contains(request.Search)
						|| x.LastName.Contains(request.Search)
					).Page(request)
					.Select(x => new AuthorShortResponse
					{
						FatgerName = x.SureName,
						FirstName = x.FirstName,
						Id = x.Id,
						SecondName = x.LastName
					}).ToListAsync());
	}

	public Task<BaseResponse> Update(AuthorUpdateRequest request)
	{
		throw new NotImplementedException();
	}
}