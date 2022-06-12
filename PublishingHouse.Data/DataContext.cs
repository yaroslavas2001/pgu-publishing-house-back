using Microsoft.EntityFrameworkCore;
using PublishingHouse.Data.Models;
using File = PublishingHouse.Data.Models.File;

namespace PublishingHouse.Data;

public sealed class DataContext : DbContext
{
	public DataContext(DbContextOptions<DataContext> options) : base(options)
	{
		Database.Migrate();
	}

	public DbSet<Faculty> Faculties { get; set; }

	public DbSet<Department> Departments { get; set; }

	public DbSet<Author> Authors { get; set; }

	public DbSet<Publication> Publications { get; set; }

	public DbSet<PublicationAuthors> PublicationsAuthors { get; set; }

	public DbSet<File> Files { get; set; }

	public DbSet<Reviewer> Reviewers { get; set; }

	public DbSet<User> Users { get; set; }

	public DbSet<MailToken> MailToken { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		base.OnConfiguring(optionsBuilder);
	}

	protected override void OnModelCreating(ModelBuilder mb)
	{
		base.OnModelCreating(mb);
		mb.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
	}
}