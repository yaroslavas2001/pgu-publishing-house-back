using PublishingHouse.StorageEnums;

namespace PublishingHouse.Interfaces.Model.Files;

/// <summary>
///     Модель файла для загрузки
/// </summary>
public class AddFileModel
{
	/// <summary>
	///     Путь к файлу, для хранения
	/// </summary>
	public string? Path { get; set; }

	/// <summary>
	///     Содержимое файла в Base64
	/// </summary>
	public string FileBase64 { get; set; }

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
}