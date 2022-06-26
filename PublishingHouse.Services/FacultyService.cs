using Microsoft.EntityFrameworkCore;
using PublishingHouse.Data;
using PublishingHouse.Data.Models;
using PublishingHouse.Interfaces;
using PublishingHouse.Interfaces.Enums;
using PublishingHouse.Interfaces.Extensions.Pagination;
using PublishingHouse.Interfaces.Model;
using PublishingHouse.Interfaces.Model.Faculty;

namespace PublishingHouse.Services;

public class FacultyService : IFacultyService
{
	private readonly DataContext _db;

	public FacultyService(DataContext db)
	{
		_db = db;
	}

	public async Task<Faculty?> CreateFacultyAsync(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
			throw new PublicationHouseException("Incorrect faculty name!", EnumErrorCode.ArgumentIsIncorrect);

		if (await _db.Faculties.AnyAsync(x => x.Name == name))
			throw new PublicationHouseException($"Faculty with name {name} is already exists!", EnumErrorCode.EntityIsAlreadyExists);

		var faculty = new Faculty
		{
			Name = name
		};

		await _db.Faculties.AddAsync(faculty);
		await _db.SaveChangesAsync();

		return faculty;
	}

	public async Task<GetFacultyResponse> GetAllFacultyAsync(GetFacultyRequest request)
	{
		return await _db.Faculties.GetPageAsync<GetFacultyResponse, Faculty, FacultyShortModel>(request, faculty =>
			new FacultyShortModel
			{
				Id = faculty.Id,
				Name = faculty.Name
			});
	}

	public async Task<Faculty?> GetFacultyAsync(long facultyId)
	{
		return await _db.Faculties.FirstOrDefaultAsync(x => x.Id == facultyId) 
		       ?? throw new PublicationHouseException($"Faculty {facultyId} is not found!", EnumErrorCode.EntityIsNotFound);
	}

	public async Task RenameFacultyAsync(long facultyId, string name)
	{
		var faculty = await _db.Faculties.FirstOrDefaultAsync(x => x.Id == facultyId);

		if (faculty is null)
			throw new PublicationHouseException("Faculty is not exists!", EnumErrorCode.EntityIsNotFound);

		faculty.Name = name;
		await _db.SaveChangesAsync();
	}

	public async Task DeleteFacultyAsync(long facultyId)
	{
		if (await _db.Faculties.AnyAsync(x => x.Id == facultyId))
			throw new PublicationHouseException("Faculty is not exists!", EnumErrorCode.EntityIsNotFound);

		_db.Faculties.Remove(new Faculty {Id = facultyId});
		await _db.SaveChangesAsync();
	}
}