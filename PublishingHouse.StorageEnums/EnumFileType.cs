using System.ComponentModel;

namespace PublishingHouse.StorageEnums;

/// <summary>
/// Тип файла
/// </summary>
public enum EnumFileType
{
	/// <summary>
	/// Выписка с кафедры
	/// </summary>
	Extract = 0,

	/// <summary>
	/// Общее
	/// </summary>
	Abstract = 1,

	/// <summary>
	/// Анти-плагиат
	/// </summary>
	AntiPlagiarism = 2,

	/// <summary>
	/// Статья
	/// </summary>
	Article = 3
}