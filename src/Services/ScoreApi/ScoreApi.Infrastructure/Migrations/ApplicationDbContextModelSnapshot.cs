﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ScoreApi.Infrastructure.Persistence.AppDb;

#nullable disable

namespace ScoreApi.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.19")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ScoreApi.Domain.Aggregates.ScoreAggregate.Score", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasAlternateKey("UserId");

                    b.ToTable("Scores");
                });

            modelBuilder.Entity("ScoreApi.Domain.Aggregates.ScoreAggregate.ScoreHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("OccurrenceId")
                        .HasColumnType("integer");

                    b.Property<int>("ScoreId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OccurrenceId");

                    b.HasIndex("ScoreId");

                    b.ToTable("ScoreHistory");
                });

            modelBuilder.Entity("ScoreApi.Domain.Entities.Occurrence", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasAlternateKey("Description");

                    b.ToTable("Occurrences");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Conta paga antes do vencimento",
                            Value = 20
                        },
                        new
                        {
                            Id = 2,
                            Description = "Conta paga depois do vencimento",
                            Value = -20
                        },
                        new
                        {
                            Id = 3,
                            Description = "Finalizou o pagamento do empréstimo",
                            Value = 60
                        },
                        new
                        {
                            Id = 4,
                            Description = "Atrasou a parcela do empréstimo",
                            Value = -40
                        },
                        new
                        {
                            Id = 5,
                            Description = "Contratou um empréstimo",
                            Value = -50
                        },
                        new
                        {
                            Id = 6,
                            Description = "Cadastrou valor da renda mensal",
                            Value = 30
                        },
                        new
                        {
                            Id = 7,
                            Description = "Usuário foi criado",
                            Value = 500
                        });
                });

            modelBuilder.Entity("ScoreApi.Domain.Aggregates.ScoreAggregate.ScoreHistory", b =>
                {
                    b.HasOne("ScoreApi.Domain.Entities.Occurrence", "Occurrence")
                        .WithMany()
                        .HasForeignKey("OccurrenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ScoreApi.Domain.Aggregates.ScoreAggregate.Score", null)
                        .WithMany("ScoreHistories")
                        .HasForeignKey("ScoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Occurrence");
                });

            modelBuilder.Entity("ScoreApi.Domain.Aggregates.ScoreAggregate.Score", b =>
                {
                    b.Navigation("ScoreHistories");
                });
#pragma warning restore 612, 618
        }
    }
}
