﻿// <auto-generated />
using System;
using CCM.TesteAcesso.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CCM.TesteAcesso.Infra.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20230725183354_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.HasSequence("seq_operation_id", "fund-transfer")
                .IncrementsBy(10);

            modelBuilder.HasSequence("seq_transfer_id", "fund-transfer")
                .IncrementsBy(10);

            modelBuilder.Entity("CCM.TesteAcesso.Domain.Entities.Operation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasComment("Table unique identity");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<long>("Id"), "seq_operation_id", "fund-transfer");

                    b.Property<string>("AccountDestination")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("account_destination");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_date")
                        .HasComment("Record inserted timestamp");

                    b.Property<DateTime?>("DateExecuted")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("date_executed");

                    b.Property<bool>("Executed")
                        .HasColumnType("boolean")
                        .HasColumnName("executed");

                    b.Property<byte>("OperationType")
                        .HasColumnType("smallint")
                        .HasColumnName("operation_type");

                    b.Property<long>("TransferId")
                        .HasColumnType("bigint")
                        .HasColumnName("transfer_id");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated_date")
                        .HasComment("Recorde updated timestamp ");

                    b.Property<decimal>("Value")
                        .HasColumnType("numeric")
                        .HasColumnName("value");

                    b.HasKey("Id")
                        .HasName("pk_operation");

                    b.HasIndex("TransferId")
                        .HasDatabaseName("ix_operation_transfer_id");

                    b.ToTable("operation", "fund-transfer");
                });

            modelBuilder.Entity("CCM.TesteAcesso.Domain.Entities.Transfer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasComment("Table unique identity");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<long>("Id"), "seq_transfer_id", "fund-transfer");

                    b.Property<string>("AccountDestination")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("account_destination")
                        .HasComment("Account destination");

                    b.Property<string>("AccountOrigin")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("account_origin")
                        .HasComment("Account origin");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_date")
                        .HasComment("Record inserted timestamp");

                    b.Property<string>("Message")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)")
                        .HasColumnName("message");

                    b.Property<byte>("Status")
                        .HasColumnType("smallint")
                        .HasColumnName("status");

                    b.Property<Guid>("TransactionUid")
                        .HasColumnType("uuid")
                        .HasColumnName("transaction_uid");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated_date")
                        .HasComment("Recorde updated timestamp ");

                    b.Property<decimal>("Value")
                        .HasColumnType("numeric")
                        .HasColumnName("value");

                    b.HasKey("Id")
                        .HasName("pk_transfer");

                    b.HasIndex("TransactionUid")
                        .IsUnique()
                        .HasDatabaseName("idx_transaction_transaction_uid");

                    b.ToTable("transfer", "fund-transfer");
                });

            modelBuilder.Entity("CCM.TesteAcesso.Domain.Entities.Operation", b =>
                {
                    b.HasOne("CCM.TesteAcesso.Domain.Entities.Transfer", "Transfer")
                        .WithMany("Operations")
                        .HasForeignKey("TransferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_operation_transfer_transfer_id");

                    b.Navigation("Transfer");
                });

            modelBuilder.Entity("CCM.TesteAcesso.Domain.Entities.Transfer", b =>
                {
                    b.Navigation("Operations");
                });
#pragma warning restore 612, 618
        }
    }
}
