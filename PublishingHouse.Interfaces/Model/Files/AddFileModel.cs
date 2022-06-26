using System.ComponentModel.DataAnnotations;
using PublishingHouse.StorageEnums;
using static System.String;

namespace PublishingHouse.Interfaces.Model.Files;

/// <summary>
///     Модель файла для загрузки
/// </summary>
public class AddFileModel
{
	/// <summary>
	///     Содержимое файла в Base64
	/// </summary>
	public string FileBase64 { get; set; } = Empty;

	/// <summary>
	///     Доступен ли файл для ревьюеров
	/// </summary>
	public bool IsVisibleForReviewers { get; set; }

	/// <summary>
	///     Тип файла в публикации
	/// </summary>
	public EnumFileType FileType { get; set; }

	/// <summary>
	///     Идентификатор публикации
	/// </summary>
	public long PublicationId { get; set; }

	/// <summary>
	/// Имя файла
	/// </summary>
	[Required]
	public string Name { get; set; }
}