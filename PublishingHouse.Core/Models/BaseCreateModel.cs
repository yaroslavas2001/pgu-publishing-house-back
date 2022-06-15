namespace PublishingHouse.Models;

/// <summary>
///		Базовая модель добавления сущностей
/// </summary>
public class BaseCreateModel
{
	/// <summary>
	///		Имя добавляемой сущности
	/// </summary>
	public string Name { get; set; } = string.Empty;
}