using PublishingHouse.Data.Models;

namespace PublishingHouse.Interfaces;

public interface IFacultyService
{
	Task<Faculty?> CreateFacultyAsync(string name);

	Task<IReadOnlyCollection<(long id, string name)>> GetAllFacultyAsync();

	Task<Faculty?> GetFacultyAsync(long facultyId);

	Task RenameFacultyAsync(long facultyId, string name);

	Task DeleteFacultyAsync(long facultyId);

}