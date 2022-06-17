using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublishingHouse.Interfaces;
using PublishingHouse.Interfaces.Model;
using PublishingHouse.Models;
using PublishingHouse.Models.Faculty;

namespace PublishingHouse.Controller;

/// <summary>
/// Факультеты
/// </summary>
[Route("/[Controller]/[action]")]
[Produces("application/json")]
public class FacultyController : Microsoft.AspNetCore.Mvc.Controller
{
	private readonly IFacultyService _faculty;

	public FacultyController(IFacultyService faculty)
	{
		_faculty = faculty;
	}

	/// <summary>
	/// Добавить факультет
	/// </summary>
	/// <param name="model"></param>
	/// <returns></returns>
	[HttpPost]
	//[Route($"/Faculty/{nameof(Add)}")]
	[ProducesResponseType(200, Type = typeof(BaseResponse<long>))]
	[ProducesResponseType(400, Type = typeof(BaseResponse))]
	//[Authorize]
	public async Task<BaseResponse<long>> Add([FromBody] AddFacultyRequest model)
	{
		var result = await _faculty.CreateFacultyAsync(model.Name);
		return new BaseResponse<long>(result?.Id ?? 0);
	}

	/// <summary>
	/// Получить список всех факультетов
	/// </summary>
	/// <returns></returns>
	[HttpGet]
	[Route($"/Faculty/{nameof(GetAll)}")]
	[ProducesResponseType(200, Type = typeof(BaseResponse<IReadOnlyCollection<(long Id, string Name)>>))]
	[ProducesResponseType(400, Type = typeof(BaseResponse))]
	public async Task<BaseResponse<IReadOnlyCollection<(long Id, string Name)>>> GetAll()
	{
		var result = await _faculty.GetAllFacultyAsync();
		return new BaseResponse<IReadOnlyCollection<(long Id, string Name)>>(result);
	}

	/// <summary>
	/// Получить факультет
	/// </summary>
	/// <returns></returns>
	[HttpGet]
	[Route($"/Faculty/{nameof(Get)}")]
	[ProducesResponseType(200, Type = typeof(BaseResponse<(long Id, string Name)>))]
	[ProducesResponseType(400, Type = typeof(BaseResponse))]
	public async Task<BaseResponse<(long Id, string Name)>> Get([FromQuery] long id)
	{
		var result = await _faculty.GetFacultyAsync(id);
		return new BaseResponse<(long Id, string Name)>((result.Id, result.Name));
	}

	/// <summary>
	/// Переименовать факультет
	/// </summary>
	/// <returns></returns>
	[HttpPatch]
	[Route($"/Faculty/{nameof(Rename)}")]
	[ProducesResponseType(200, Type = typeof(BaseResponse))]
	[ProducesResponseType(400, Type = typeof(BaseResponse))]
	[Authorize]
	public async Task<BaseResponse> Rename([FromQuery] long id, [FromQuery] string name)
	{
		await _faculty.RenameFacultyAsync(id, name);
		return new BaseResponse();
	}

	/// <summary>
	/// Удалить факультет
	/// </summary>
	/// <returns></returns>
	[HttpDelete]
	[Route($"/Faculty/{nameof(Delete)}")]
	[ProducesResponseType(200, Type = typeof(BaseResponse))]
	[ProducesResponseType(400, Type = typeof(BaseResponse))]
	[Authorize]
	public async Task<BaseResponse> Delete([FromQuery] long id)
	{
		await _faculty.DeleteFacultyAsync(id);
		return new BaseResponse();
	}
}