namespace PublishingHouse.Interfaces.Model;

/// <summary>
///		Модель пагинации запросов
/// </summary>
public class PaginationRequest
{
	/// <summary>
	///		Пропустить
	/// </summary>
	public int? Skip { get; set; }

	/// <summary>
	///		Вытащить
	/// </summary>
	public int? Take { get; set; }
}