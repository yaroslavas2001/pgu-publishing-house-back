using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using File = PublishingHouse.Data.Models.File;

namespace PublishingHouse.Data.Configurations;

public class FileConfigurations : IEntityTypeConfiguration<File>
{
	public void Configure(EntityTypeBuilder<File> builder)
	{
		builder.HasKey(x => x.Id);
		builder
			.HasOne(x => x.Publication)
			.WithMany(x => x.Files)
			.HasForeignKey(x => x.PublicationId);

		builder
			.HasOne(x => x.Review)
			.WithMany(x => x.Files)
			.HasForeignKey(x => x.ReviewId)
			.IsRequired(false);
	}
}