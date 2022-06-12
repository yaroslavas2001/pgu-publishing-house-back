using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PublishingHouse.Data.Models;

namespace PublishingHouse.Data.Configurations;

public class ReviewerConfig : IEntityTypeConfiguration<Reviewer>
{
	public void Configure(EntityTypeBuilder<Reviewer> builder)
	{
		builder.HasKey(x => x.Id);
	}
}