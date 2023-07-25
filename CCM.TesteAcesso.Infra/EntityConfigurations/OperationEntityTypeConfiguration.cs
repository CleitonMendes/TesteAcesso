using CCM.TesteAcesso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CCM.TesteAcesso.Infra.EntityConfigurations
{
    public class OperationEntityTypeConfiguration : IEntityTypeConfiguration<Operation>
    {
        public void Configure(EntityTypeBuilder<Operation> builder)
        {
            builder.ConfigBaseEntity(Context.DefaultSchema);

            CustomerRoot(builder);
        }

        private static void CustomerRoot(EntityTypeBuilder<Operation> builder)
        {

            builder
                .Property(p => p.AccountDestination)
                .HasMaxLength(50)
                .IsRequired();

            builder
            .Property(p => p.AccountDestination)
            .HasMaxLength(50)
            .IsRequired();

            builder
                .Property(p => p.Value)
                .IsRequired();
            builder
                .Property(p => p.OperationType)
                .IsRequired();

            builder
                .Property(p => p.Executed)
                .IsRequired();
            builder
                .Property(p => p.DateExecuted);

            builder
                .HasOne(p => p.Transfer)
                .WithMany(x => x.Operations)
                .HasForeignKey(x => x.TransferId);
        }
    }
}
