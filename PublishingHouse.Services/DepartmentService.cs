﻿using Microsoft.EntityFrameworkCore;
using PublishingHouse.Data;
using PublishingHouse.Data.Models;
using PublishingHouse.Interfaces;
using PublishingHouse.Interfaces.Model.Departmanet;

namespace PublishingHouse.Services;

public class DepartmentService : IDepartmentService
{
	private readonly DataContext _db;

	public DepartmentService(DataContext db)
	{
		_db = db;
	}

	public async Task<long> AddDepartmentAsync(long facultyId, string name)
	{
		if (await _db.Faculties.AllAsync(x => x.Id != facultyId))
			throw new Exception($"Faculty {facultyId} is not exists!");

		var department = new Department
		{
			Name = name,
			FacultyId = facultyId
		};

		await _db.Departments.AddAsync(department);
		await _db.SaveChangesAsync();

		return department.Id;
	}


	public async Task<IReadOnlyCollection<DepartmentModel>> GetDepartments(long? facultyId = null)
	{
		var query = facultyId.HasValue
			? _db.Departments.Where(x => x.FacultyId == facultyId)
			: _db.Departments.AsQueryable();

		var result = await query
			.Select(x => new DepartmentModel
			{
				Id = x.Id,
				FacultyId = x.FacultyId,
				DepartmentName = x.Name
			}).ToListAsync();

		return result.AsReadOnly();
	}

	public async Task RenameDepartment(long departmentId, string name)
	{
		var department = await _db.Departments.FirstOrDefaultAsync(x => x.Id == departmentId);
		if (department is null)
			throw new Exception($"Department {departmentId} is not exists!");

		department.Name = name;
		await _db.SaveChangesAsync();
	}

	public async Task DeleteDepartment(long departmentId)
	{
		_db.Departments.Remove(new Department {Id = departmentId});
		await _db.SaveChangesAsync();
	}
}