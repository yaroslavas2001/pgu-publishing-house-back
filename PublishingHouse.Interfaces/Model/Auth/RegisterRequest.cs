using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace PublishingHouse.Interfaces.Model.Auth;

public class RegisterRequest
{
	[Required]
	public string Email { get; set; } = string.Empty;

	[Required]
	public string Password { get; set; } = string.Empty;

	[Required]
	public string ConfirmPassword { get; set; } = string.Empty;

	public string FirstName { get; set; } = string.Empty;

	public string LastName { get; set; } = string.Empty;

	public string SureName { get; set; } = string.Empty;
}