using PublishingHouse.StorageEnums;

namespace PublishingHouse.Interfaces.Model.Auth;

public class LoginResponse
{
	public string Token { get; set; } = string.Empty;
	public string FirstName { get; set; } = string.Empty;
	public string SureName { get; set; } = string.Empty;
	public EnumUserRole Role { get; set; }
	public long Id { get; set; }
}