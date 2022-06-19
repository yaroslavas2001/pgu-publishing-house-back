namespace PublishingHouse.StorageEnums;

/// <summary>
///		Статус публикации
/// </summary>
public enum EnumPublicationStatus
{
	/// <summary>
	/// Новый материал
	/// </summary>
	Draft = 0,

	/// <summary>
	/// Возвращено на доработку
	/// </summary>
	Returned = 1,

	/// <summary>
	/// На проверке у рецензента
	/// </summary>
	Check = 2,

	/// <summary>
	/// На проверке у издательства
	/// </summary>
	ReworkPublishing = 3,

	/// <summary>
	/// На печать
	/// </summary>
	ToPrint = 4,

	/// <summary>
	/// Не рекомендовано к печати
	/// </summary>
	NotRecommendedForPrinting = 5,

	/// <summary>
	/// На архивацию
	/// </summary>
	ForArchiving = 6,
}