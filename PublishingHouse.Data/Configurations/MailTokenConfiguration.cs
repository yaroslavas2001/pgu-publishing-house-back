using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PublishingHouse.Data.Models;

namespace PublishingHouse.Data.Configurations;

public class MailTokenConfiguration : IEntityTypeConfiguration<MailToken>
{
	public void Configure(EntityTypeBuilder<MailToken> builder)
	{
		builder.HasKey(x => x.Id);
		builder.HasOne(x => x.User)
			.WithMany(x => x.Tokens)
			.HasForeignKey(x => x.UserId);
	}
}