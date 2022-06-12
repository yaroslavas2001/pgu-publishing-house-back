using System;
using Microsoft.EntityFrameworkCore;
using PublishingHouse.Data;

namespace PublishingHouse.Tests.Common;

public class DbFixture : IDisposable
{
	public DbFixture()
	{
		Context = new DataContext(
			new DbContextOptionsBuilder<DataContext>()
				.UseNpgsql("Host=localhost;Port=5432;Database=Yarik.test;Username=postgres;Password=test").Options);
	}

	public DataContext Context { get; set; }

	public void Dispose()
	{
		Context.Users.RemoveRange(Context.Users);
		Context.Authors.RemoveRange(Context.Authors);
		Context.SaveChanges();
		Context.Dispose();
	}
}