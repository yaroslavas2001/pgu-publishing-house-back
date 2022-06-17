using Microsoft.EntityFrameworkCore;
using PublishingHouse.Data;
using PublishingHouse.Data.Models;
using PublishingHouse.Interfaces;

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
			throw new ArgumentNullException(nameof(name), $"Incorrect faculty name!");

		if (await _db.Faculties.AnyAsync(x => x.Name == name))
			throw new Exception($"Faculty with name {name} is already exists!");

		var faculty = new Faculty
		{
			Name = name
		};

		await _db.Faculties.AddAsync(faculty);
		await _db.SaveChangesAsync();

		return faculty;

	}

	public async Task<IReadOnlyCollection<(long id, string name)>> GetAllFacultyAsync()
	{
		return (await _db.Faculties.ToListAsync())
			.Select(x => (x.Id, x.Name))
			.ToList()
			.AsReadOnly();
	}

	public async Task<Faculty?> GetFacultyAsync(long facultyId)
	{
		return await _db.Faculties.FirstOrDefaultAsync(x => x.Id == facultyId);
	}

	public async Task RenameFacultyAsync(long facultyId, string name)
	{
		var faculty = await _db.Faculties.FirstOrDefaultAsync(x => x.Id == facultyId);

		if (faculty is null)
			throw new Exception("Faculty is not exists!");

		faculty.Name = name;
		await _db.SaveChangesAsync();
	}

	public async Task DeleteFacultyAsync(long facultyId)
	{
		if (await _db.Faculties.AnyAsync(x => x.Id == facultyId))
			throw new Exception("Faculty is not exists!");

		_db.Faculties.Remove(new Faculty { Id = facultyId });
		await _db.SaveChangesAsync();
	}
}