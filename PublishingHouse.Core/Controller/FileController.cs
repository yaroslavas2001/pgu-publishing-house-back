using Microsoft.AspNetCore.Mvc;
using PublishingHouse.Interfaces;
using PublishingHouse.Interfaces.Model;
using PublishingHouse.Interfaces.Model.Files;

namespace PublishingHouse.Controller;

/// <summary>
/// Файлы
/// </summary>
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
		if (string.IsNullOrWhiteSpace(model.Name) || model.Name.IndexOfAny(Path.GetInvalidFileNameChars()) > 0)
			throw new FileLoadException("File name is incorrect!", model.Name);

		var result = await service.AddFileAsync(model);
		return new BaseResponse<string>(result);
	}

	/// <summary>
	///     Получить файлы публикации
	/// </summary>
	/// <param name="publicationId"></param>
	/// <param name="isReviewer"></param>
	/// <returns></returns>
	[HttpGet]
	[Route($"{nameof(Get)}")]
	[ProducesResponseType(200, Type = typeof(BaseResponse<IReadOnlyCollection<PublicationFileModel>>))]
	[ProducesResponseType(400, Type = typeof(BaseResponse))]
	public async Task<BaseResponse<IReadOnlyCollection<PublicationFileModel>>> Get([FromServices] IFileService service,
		[FromQuery] long publicationId, [FromQuery] bool isReviewer)
	{
		var files = await service.GetPublicationFilesAsync(publicationId, isReviewer);
		return new BaseResponse<IReadOnlyCollection<PublicationFileModel>>(files);
	}
}