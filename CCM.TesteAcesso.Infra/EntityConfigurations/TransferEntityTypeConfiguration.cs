using CCM.TesteAcesso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CCM.TesteAcesso.Infra.EntityConfigurations
{
    public class TransferEntityTypeConfiguration : IEntityTypeConfiguration<Transfer>
    {
        public void Configure(EntityTypeBuilder<Transfer> builder)
        {
            builder.ConfigBaseEntity(Context.DefaultSchema);

            builder
                .Property(p => p.AccountDestination)
                .HasMaxLength(50)
                .HasComment("Account destination")
                .IsRequired();
            builder
                .Property(p => p.AccountOrigin)
                .HasMaxLength(50)
                .HasComment("Account origin")
                .IsRequired();

            builder
                .Property(p => p.Message)
                .HasMaxLength(300);

            builder
                .Property(p => p.Status)
                .IsRequired();

            builder
                .Property(p => p.TransactionUid)
                .IsRequired();

            builder.HasIndex(p => p.TransactionUid)
                .IsUnique()
                .HasDatabaseName("idx_transaction_transaction_uid");

            builder
                .Property(p => p.Value)
                .IsRequired();
            builder
                .HasMany(p => p.Operations)
                .WithOne(x => x.Transfer)
                .HasForeignKey(x => x.TransferId);
        }
    }
}