namespace PublishingHouse.Interfaces.Exstensions.Pagination;

/// <summary>
///		Модель страницы
/// </summary>
public class Page
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