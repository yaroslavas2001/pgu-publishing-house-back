using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublishingHouse.Interfaces;
using PublishingHouse.Interfaces.Model;
using PublishingHouse.Interfaces.Model.Departmanet;
using PublishingHouse.Models.Department;

namespace PublishingHouse.Controller;

/// <summary>
/// Кафедры
/// </summary>
[Route("/Department")]
[Produces("application/json")]
public class DepartmaneController : Microsoft.AspNetCore.Mvc.Controller
{
	private readonly IDepartmentService _departmentService;

	public DepartmaneController(IDepartmentService departmentService)
	{
		_departmentService = departmentService;
	}


	/// <summary>
	/// Добавить кафедру
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	[HttpPost]
	[Route($"{nameof(Add)}")]
	[ProducesResponseType(200, Type = typeof(BaseResponse<long>))]
	[ProducesResponseType(400, Type = typeof(BaseResponse))]
	[Authorize]
	public async Task<BaseResponse<long>> Add([FromBody] AddDepartmentRequest request)
	{
		var result = await _departmentService.AddDepartmentAsync(request.FacultyId, request.DepartmentName);
		return new BaseResponse<long>(result);
	}

	/// <summary>
	/// Получить список всех кафедр
	/// </summary>
	/// <returns></returns>
	[HttpGet]
	[Route($"{nameof(GetAll)}")]
	[ProducesResponseType(200, Type = typeof(BaseResponse<IReadOnlyCollection<DepartmentModel>>))]
	[ProducesResponseType(400, Type = typeof(BaseResponse))]
	public async Task<BaseResponse<IReadOnlyCollection<DepartmentModel>>> GetAll([FromQuery] long? facultyId)
	{
		var result = await _departmentService.GetDepartments(facultyId);
		return new BaseResponse<IReadOnlyCollection<DepartmentModel>>(result);
	}

	
	/// <summary>
	/// Переименовать Кафедру
	/// </summary>
	/// <returns></returns>
	[HttpPatch]
	[Route($"{nameof(Rename)}")]
	[ProducesResponseType(200, Type = typeof(BaseResponse))]
	[ProducesResponseType(400, Type = typeof(BaseResponse))]
	[Authorize]
	public async Task<BaseResponse> Rename([FromQuery] long id, [FromQuery] string name)
	{
		await _departmentService.RenameDepartment(id, name);
		return new BaseResponse();
	}

	/// <summary>
	/// Удалить кафедру
	/// </summary>
	/// <returns></returns>
	[HttpDelete]
	[Route($"{nameof(Delete)}")]
	[ProducesResponseType(200, Type = typeof(BaseResponse))]
	[ProducesResponseType(400, Type = typeof(BaseResponse))]
	[Authorize]
	public async Task<BaseResponse> Delete([FromQuery] long id)
	{
		await _departmentService.DeleteDepartment(id);
		return new BaseResponse();
	}

}