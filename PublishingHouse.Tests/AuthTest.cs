using System.Linq;
using System.Threading.Tasks;
using AutoBogus;
using Bogus;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using PublishingHouse.Data;
using PublishingHouse.Data.Models;
using PublishingHouse.Interfaces.Model.Auth;
using PublishingHouse.Services;
using PublishingHouse.StorageEnums;
using PublishingHouse.Tests.Common;
using Xunit;

namespace PublishingHouse.Tests;

public class AuthTest : IClassFixture<DbFixture>
{
	private readonly DataContext _context;
	private readonly AuthService _sut;

	public AuthTest(DbFixture fixture)
	{
		_context = fixture.Context;
		_sut = new AuthService(fixture.Context);
	}

	[Fact(DisplayName = "Регистрация")]
	public async Task ShouldBeUserCreated()
	{
		var request = RegisterRequestFaker();

		var res = await _sut.Register(request);

		res.Should().BeEquivalentTo(new
		{
			IsSuccess = true
		});
		var userExist = _context.Users.FirstOrDefault(x => x.Email == request.Email);
		userExist.Should().BeEquivalentTo(new
		{
			request.Email,
			request.FatherName,
			request.FirstName,
			request.LastName,
			Status = EnumUserStatus.New,
			Role = EnumUserRole.User
		});
		Assert.True(await _context.MailToken.AnyAsync(x => x.UserId == userExist.Id));
	}

	private static RegisterRequest RegisterRequestFaker()
	{
		var password = Instances.Faker.Random.String2(8);
		return new AutoFaker<RegisterRequest>()
			.RuleFor(x => x.Password, x => password)
			.RuleFor(x => x.ConfirmPassword, x => password);
	}

	[Fact(DisplayName = "Активация")]
	public async Task ShouldBeLogin()
	{
		var request = RegisterRequestFaker();
		await _sut.Register(request);
		var userExist = _context.Users.Include(x => x.Tokens).FirstOrDefault(x => x.Email == request.Email);

		var activate = await _sut.ActivateAccount(new ActivateAccountRequest
			{Key = userExist.Tokens.FirstOrDefault()!.Key});

		var activateUser = _context.Users.Include(x => x.Tokens).FirstOrDefault(x => x.Email == request.Email);

		activate.Should().BeEquivalentTo(new
		{
			IsSuccess = true
		});
		activateUser!.Status.Should().Be(EnumUserStatus.Verified);
	}

	[Fact(DisplayName = "добавить автора")]
	public async Task AddAuthor()
	{
		var add = new Author();
		await _context.AddAsync(add);
		await _context.SaveChangesAsync();
	}
}

public static class Instances
{
	public static Faker Faker = new();
}