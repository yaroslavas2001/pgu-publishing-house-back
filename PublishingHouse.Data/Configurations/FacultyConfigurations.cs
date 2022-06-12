using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PublishingHouse.Data.Models;

namespace PublishingHouse.Data.Configurations;

public class FacultyConfigurations :
	IEntityTypeConfiguration<Faculty>
{
	public void Configure(EntityTypeBuilder<Faculty> builder)
	{
		builder.HasKey(x => x.Id);
	}
}