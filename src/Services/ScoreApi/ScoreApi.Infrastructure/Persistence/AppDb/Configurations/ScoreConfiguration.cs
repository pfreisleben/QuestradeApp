using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoreApi.Domain.Aggregates.ScoreAggregate;

namespace ScoreApi.Infrastructure.Persistence.AppDb.Configurations;

public class ScoreConfiguration : IEntityTypeConfiguration<Score>
{
    public void Configure(EntityTypeBuilder<Score> entity)
    {
        entity.HasKey(x => x.Id);
        entity.HasAlternateKey(x => x.UserId);

        entity.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        entity.HasMany(x => x.ScoreHistories)
            .WithOne()
            .HasForeignKey(x => x.ScoreId);



    }
}