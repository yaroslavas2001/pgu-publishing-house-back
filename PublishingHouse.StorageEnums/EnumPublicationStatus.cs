namespace PublishingHouse.StorageEnums;

/// <summary>
///		Статус публикации
/// </summary>
public enum EnumPublicationStatus
{
	/// <summary>
	/// Черновик
	/// </summary>
	Draft = 0,

	/// <summary>
	/// Опубликовано
	/// </summary>
	Published = 1,

	/// <summary>
	/// На проверке
	/// </summary>
	Check = 2,

	/// <summary>
	/// На исправление
	/// </summary>
	Rework = 3
}