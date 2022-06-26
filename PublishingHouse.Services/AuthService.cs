using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PublishingHouse.Data;
using PublishingHouse.Data.Models;
using PublishingHouse.External.Mail;
using PublishingHouse.External.Mail.Request;
using PublishingHouse.Interfaces;
using PublishingHouse.Interfaces.Model;
using PublishingHouse.Interfaces.Model.Auth;
using PublishingHouse.Services.Infrastruct;
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
		if (string.IsNullOrWhiteSpace(request.Password)
		    || string.IsNullOrWhiteSpace(request.Email))
			throw new Exception("Заполни нужные поля");

		var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == request.Email.ToLower());
		if (user == null || user.Status is EnumUserStatus.New or EnumUserStatus.Blocked)
			throw new Exception("Ты кто такой, я таких не знаю");

		if (user.PasswordHash != PasswordHashService.GetHashPassword(request.Password, user.PasswordKey))
			throw new Exception("Пароль должен быть правильный");

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
			throw new Exception("Email занят");

		if (string.IsNullOrWhiteSpace(request.Email))
			throw new Exception("Email обязателен");

		if (string.IsNullOrWhiteSpace(request.Password))
			throw new Exception("Пароль обязателен");

		if (string.IsNullOrWhiteSpace(request.ConfirmPassword))
			throw new Exception("Повтор пароль обязателен");

		if (request.Password != request.ConfirmPassword)
			throw new Exception("Пароли не совпали(");

		var guidEmail = Guid.NewGuid();
		var password = PasswordHashService.GenHashPassword(request.Password);

		var user = new User
		{
			Email = request.Email,
			SureName = request.SureName ?? string.Empty,
			FirstName = request.FirstName ?? string.Empty,
			LastName = request.LastName ?? string.Empty,
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
		if (user == null)
			throw new Exception("Токен не найден");
		user.Status = EnumUserStatus.Verified;
		await _db.SaveChangesAsync();
		return new BaseResponse<TokenResponse>(await GenerateToken(user.Id));
	}

	private async Task<TokenResponse> GenerateToken(long id)
	{
		(User user, ClaimsIdentity claim) identity;
		identity = await GetIdentity(id);
		if (identity.user is null)
			throw new Exception("User Not Found");
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
			Token = encodedJwt
		};
	}

	private async Task<(User, ClaimsIdentity)> GetIdentity(long id)
	{
		var person = await _db.Users.FindAsync(id);
		if (person is null)
			throw new Exception("Пользователь не найден");

		if (person.Status == EnumUserStatus.Blocked)
			throw new Exception("Пользователь заблокирован");

		var claims = new List<Claim> {new(ClaimTypes.Name, person.Id.ToString())};
		var claimsIdentity =
			new ClaimsIdentity(claims, "TokenResponse", ClaimsIdentity.DefaultNameClaimType,
				ClaimsIdentity.DefaultRoleClaimType);
		return (person, claimsIdentity);
	}
}