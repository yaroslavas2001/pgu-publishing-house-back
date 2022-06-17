using PublishingHouse.Interfaces.Model.Departmanet;

namespace PublishingHouse.Interfaces;

public interface IDepartmentService
{
	Task<long> AddDepartmentAsync(long facultyId, string name);

	Task<IReadOnlyCollection<DepartmentModel>> GetDepartments(long? facultyId = null);

	Task RenameDepartment(long departmentId, string name);

	Task DeleteDepartment(long departmentId);
}