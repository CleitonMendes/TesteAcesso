using CCM.TesteAcesso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MiraeDigital.FixedIncome.Infrastructure.Extensions;

namespace CCM.TesteAcesso.Infra
{
    public class Context : DbContext
    {
        public const string DefaultSchema = "fund-transfer";

        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<Operation> Operations { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);


            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.SetSchema(DefaultSchema.ToLower());

                StoreObjectIdentifier.Create(entityType, StoreObjectType.Table);

                entityType.GetProperties()
                    .ToList()
                    .ForEach(p => p.SetColumnName(p.GetColumnName(StoreObjectIdentifier.Create(entityType, StoreObjectType.Table).Value).AsNpgsqlConvention()));

                var pk = entityType.FindPrimaryKey();

                if (pk is not null)
                    pk.SetName(pk.GetName().ToLower());

                entityType.GetForeignKeys()
                    .ToList()
                    .ForEach(fk => fk.SetConstraintName(fk.GetConstraintName().ToLower()));


                entityType.GetIndexes()
                    .ToList()
                    .ForEach(ix => ix.SetDatabaseName(ix.GetDatabaseName().ToLower()));
            }

        }

    }
}
