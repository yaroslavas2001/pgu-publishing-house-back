using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PublishingHouse.Data.Models;

namespace PublishingHouse.Data.Configurations;

public class AuthorConfigurations :
	IEntityTypeConfiguration<Author>
{
	public void Configure(EntityTypeBuilder<Author> builder)
	{
		builder.HasKey(x => x.Id);
		builder.HasOne(x => x.Department)
			.WithMany(x => x.Authors)
			.HasForeignKey(x => x.DepartmentId)
			.IsRequired(false);
	}
}