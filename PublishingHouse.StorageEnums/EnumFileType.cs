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
	[Description("Выписка с кафедры")]
	Extract = 0,

	/// <summary>
	/// Общее
	/// </summary>
	[Description("Общее")]
	Abstract = 1,

	/// <summary>
	/// Анти-плагиат
	/// </summary>
	[Description("Анти-плагиат")]
	AntiPlagiarism = 2,

	/// <summary>
	/// Статья
	/// </summary>
	[Description("Статья")]
	Article = 3
}