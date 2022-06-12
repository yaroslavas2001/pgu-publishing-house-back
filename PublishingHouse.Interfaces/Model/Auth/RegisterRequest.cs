﻿namespace PublishingHouse.Interfaces.Model.Auth;

public class RegisterRequest
{
	public string Email { get; set; }
	public string Password { get; set; }
	public string ConfirmPassword { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string FatherName { get; set; }
}