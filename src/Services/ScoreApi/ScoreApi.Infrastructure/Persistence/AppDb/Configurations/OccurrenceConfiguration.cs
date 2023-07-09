using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoreApi.Domain.Entities;

namespace ScoreApi.Infrastructure.Persistence.AppDb.Configurations;

public class OccurrenceConfiguration : IEntityTypeConfiguration<Occurrence>
{
    public void Configure(EntityTypeBuilder<Occurrence> entity)
    {
        entity.HasKey(x => x.Id);

        entity.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        entity.HasAlternateKey(x => x.Description);

        entity.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(100);


        entity.HasData(
            new Occurrence(1, 20, "Conta paga antes do vencimento"),
            new Occurrence(2, -20, "Conta paga depois do vencimento"),
            new Occurrence(3, 60, "Finalizou o pagamento do empréstimo"),
            new Occurrence(4, -40, "Atrasou a parcela do empréstimo"),
            new Occurrence(5, -50, "Contratou um empréstimo"),
            new Occurrence(6, 30, "Cadastrou valor da renda mensal")
            );
    }
}