using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PublishingHouse.Data.Models;

namespace PublishingHouse.Data.Configurations;

public class DepartmentConfigurations : IEntityTypeConfiguration<Department>
{
	public void Configure(EntityTypeBuilder<Department> entityTypeBuilder)
	{
		entityTypeBuilder.HasKey(x => x.Id);
		entityTypeBuilder
			.HasOne(x => x.Faculty)
			.WithMany(x => x.Departments)
			.HasForeignKey(x => x.FacultyId);
	}
}