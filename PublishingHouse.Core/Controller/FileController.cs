﻿using Microsoft.AspNetCore.Mvc;
using PublishingHouse.Interfaces;
using PublishingHouse.Interfaces.Model;
using PublishingHouse.Interfaces.Model.Files;

namespace PublishingHouse.Controller;

[Route("/[controller]")]
[Produces("application/json")]
public class FileController : Microsoft.AspNetCore.Mvc.Controller
{
	/// <summary>
	///     Загрузить файл и прикрепить его к публикации
	/// </summary>
	/// <param name="service"></param>
	/// <param name="model">Модель файла</param>
	/// <returns></returns>
	[HttpPut]
	[Route($"{nameof(UploadFileForPublication)}")]
	[ProducesResponseType(200, Type = typeof(BaseResponse))]
	[ProducesResponseType(400, Type = typeof(BaseResponse))]
	public async Task<BaseResponse<string>> UploadFileForPublication([FromServices] IFileService service,
		[FromBody] AddFileModel model)
	{
		var result = await service.AddFileAsync(model);
		return new BaseResponse<string>(result);
	}
}