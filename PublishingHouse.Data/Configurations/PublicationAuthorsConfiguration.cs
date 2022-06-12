using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PublishingHouse.Data.Models;

namespace PublishingHouse.Data.Configurations;

public class PublicationAuthorsConfiguration : IEntityTypeConfiguration<PublicationAuthors>
{
	public void Configure(EntityTypeBuilder<PublicationAuthors> builder)
	{
		builder.HasKey(x => new {x.AuthorId, x.PublicationId});
		builder
			.HasOne(x => x.Author)
			.WithMany(x => x.Publications)
			.HasForeignKey(x => x.PublicationId);
		builder
			.HasOne(x => x.Publication)
			.WithMany(x => x.Authors)
			.HasForeignKey(x => x.AuthorId);
	}
}