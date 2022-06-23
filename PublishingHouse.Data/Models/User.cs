using PublishingHouse.StorageEnums;

namespace PublishingHouse.Data.Models;

public class User
{
	public long Id { get; set; }

	public string FirstName { get; set; } = string.Empty;

	public string LastName { get; set; } = string.Empty;

	public string SureName { get; set; } = string.Empty;

	public string Email { get; set; } = string.Empty;

	public string PasswordHash { get; set; } = string.Empty;

	public string PasswordKey { get; set; } = string.Empty;

	public EnumUserRole Role { get; set; }

	public EnumUserStatus Status { get; set; } = EnumUserStatus.New;

	public List<Publication> Publications { get; set; } = null!;

	public List<MailToken> Tokens { get; set; } = null!;
}