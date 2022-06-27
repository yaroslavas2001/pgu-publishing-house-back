using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PublishingHouse.Data;
using PublishingHouse.Data.Models;
using PublishingHouse.External.Mail;
using PublishingHouse.External.Mail.Request;
using PublishingHouse.Interfaces;
using PublishingHouse.Interfaces.Enums;
using PublishingHouse.Interfaces.Model;
using PublishingHouse.Interfaces.Model.Auth;
using PublishingHouse.Services.Infrastructure;
using PublishingHouse.StorageEnums;

namespace PublishingHouse.Services;

public class AuthService : IAuthService
{
	private readonly DataContext _db;
	private readonly MailService _mailService = new();

	public AuthService(DataContext db)
	{
		_db = db;
	}

	public async Task<BaseResponse<LoginResponse>> Login(LoginRequest request)
	{
		if (string.IsNullOrWhiteSpace(request.Password))
			throw new PublicationHouseException(EnumErrorCode.PasswordIsAreRequired);

		if (string.IsNullOrWhiteSpace(request.Email))
			throw new PublicationHouseException(EnumErrorCode.EmailAreRequired);

		var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == request.Email.ToLower());
		if (user == null || user.Status is EnumUserStatus.New or EnumUserStatus.Blocked)
			throw new PublicationHouseException(EnumErrorCode.AccessDenied);

		if (user.PasswordHash != PasswordHashService.GetHashPassword(request.Password, user.PasswordKey))
			throw new PublicationHouseException("Password are incorrect", EnumErrorCode.AccessDenied);

		var response = new LoginResponse
		{
			Token = (await GenerateToken(user.Id)).Token,
			Id = user.Id,
			SureName = user.SureName,
			FirstName = user.FirstName,
			Role = user.Role
		};

		return new BaseResponse<LoginResponse>(response);
	}

	public async Task<BaseResponse> Register(RegisterRequest request)
	{
		if (await _db.Users.AnyAsync(x => x.Email == request.Email))
			throw new PublicationHouseException(EnumErrorCode.MailIsAlreadyInUse);

		if (string.IsNullOrWhiteSpace(request.Email))
			throw new PublicationHouseException(EnumErrorCode.EmailAreRequired);

		if (string.IsNullOrWhiteSpace(request.Password))
			throw new PublicationHouseException(EnumErrorCode.PasswordIsAreRequired);

		if (string.IsNullOrWhiteSpace(request.ConfirmPassword))
			throw new PublicationHouseException(EnumErrorCode.PasswordIsAreRequired);

		if (request.Password != request.ConfirmPassword)
			throw new PublicationHouseException(EnumErrorCode.PasswordsDoNotMatch);

		var guidEmail = Guid.NewGuid();
		var password = PasswordHashService.GenHashPassword(request.Password);

		var user = new User
		{
			Email = request.Email,
			SureName = request.SureName,
			FirstName = request.FirstName,
			LastName = request.LastName,
			Status = EnumUserStatus.New,
			Role = EnumUserRole.User,
			PasswordKey = password.Key,
			PasswordHash = password.Hash
		};

		var token = new MailToken
		{
			DateExpire = DateTime.UtcNow.AddDays(5),
			Key = guidEmail,
			User = user
		};

		await _mailService.RegisterSuccess(new SendRegisterMail
		{
			Email = request.Email,
			Token = guidEmail.ToString("D")
		});

		await _db.MailToken.AddAsync(token);
		await _db.SaveChangesAsync();

		return new BaseResponse();
	}

	public async Task<BaseResponse<TokenResponse>> ActivateAccount(ActivateAccountRequest request)
	{
		var dateExpire = DateTime.UtcNow;
		var user = await _db.Users.FirstOrDefaultAsync(x =>
			x.Tokens.Any(z => z.Key == request.Key && z.DateExpire > dateExpire));

		if (user is null)
			throw new PublicationHouseException(EnumErrorCode.TokenIsNotFound);

		user.Status = EnumUserStatus.Verified;
		await _db.SaveChangesAsync();

		return new BaseResponse<TokenResponse>(await GenerateToken(user.Id));
	}

	private async Task<TokenResponse> GenerateToken(long id)
	{
		(User user, ClaimsIdentity claim) identity = await GetIdentity(id);
		if (identity.user is null)
			throw new PublicationHouseException("User is not found!", EnumErrorCode.EntityIsNotFound);

		var now = DateTime.UtcNow;
		var jwt = new JwtSecurityToken(
			AuthOptions.ISSUER,
			AuthOptions.AUDIENCE,
			notBefore: now,
			claims: identity.claim.Claims,
			expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
			signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
				SecurityAlgorithms.HmacSha256));

		var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

		return new TokenResponse
		{
			UserId = identity.user.Id,
			FirstName = identity.user.FirstName,
			LastName = identity.user.LastName,
			SureName = identity.user.SureName,
			Token = $"Bearer {encodedJwt}"
		};
	}

	private async Task<(User, ClaimsIdentity)> GetIdentity(long id)
	{
		var person = await _db.Users.FindAsync(id);
		if (person is null)
			throw new PublicationHouseException("User is not found!", EnumErrorCode.EntityIsNotFound);

		if (person.Status == EnumUserStatus.Blocked)
			throw new PublicationHouseException(EnumErrorCode.UserIsBlocked);

		var claims = new List<Claim> { new(ClaimTypes.Name, person.Id.ToString()) };
		var claimsIdentity =
			new ClaimsIdentity(claims, "TokenResponse", ClaimsIdentity.DefaultNameClaimType,
				ClaimsIdentity.DefaultRoleClaimType);
		return (person, claimsIdentity);
	}
}