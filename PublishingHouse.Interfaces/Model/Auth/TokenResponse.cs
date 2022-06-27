namespace PublishingHouse.Interfaces.Model.Auth;

public class TokenResponse
{
	public long UserId { get; set; }

	public string FirstName { get; set; } = string.Empty;

	public string LastName { get; set; } = string.Empty;

	public string SureName { get; set; } = string.Empty;

	public string Token { get; set; } = string.Empty;

}