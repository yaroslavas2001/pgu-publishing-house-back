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
				.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PublishingHouse;Integrated Security=True;Connect Timeout=300;").Options);
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