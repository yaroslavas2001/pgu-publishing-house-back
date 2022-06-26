using System.ComponentModel;

namespace PublishingHouse.Interfaces.Enums;

public enum EnumErrorCode
{
	Unknown = 0,

	[Description("Mail is already used!")]
	MailIsAlreadyInUse = 1,

	[Description("String is not in valid mail address!")]
	EmailIsNotValid = 2,

	[Description("The requested entry does not exist!")]
	EntityIsNotFound = 3,

	[Description("Entity with relations cant be deleted!")]
	EntityWithRelationsCantBeDeleted = 4,

	[Description("Email is are required!")]
	EmailAreRequired = 5,

	[Description("Password is are required!")]
	PasswordIsAreRequired = 6,

	[Description("Passwords do not match!")]
	PasswordsDoNotMatch = 7,

	[Description("User is blocked!")]
	UserIsBlocked = 8,

	[Description("Argument is incorrect")]
	ArgumentIsIncorrect = 9,

	[Description("Entity is already exists")]
	EntityIsAlreadyExists = 10,

	[Description("Access denied!")]
	AccessDenied = 11,

	[Description("TriggerIsNotFound is not found")]
	TriggerIsNotFound = 12,

	[Description("Trigger is not supported!")]
	TriggerIsNotSupported = 13,

	[Description("Token is not found!")]
	TokenIsNotFound = 14,
}