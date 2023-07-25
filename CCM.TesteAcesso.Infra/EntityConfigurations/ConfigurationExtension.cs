using CCM.TesteAcesso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiraeDigital.FixedIncome.Infrastructure.Extensions;

namespace CCM.TesteAcesso.Infra.EntityConfigurations
{
    public static class ConfigurationExtension
    {
        public static void ConfigBaseEntity<TEntity>(this EntityTypeBuilder<TEntity> builder, string schema) where TEntity : Entity
        {
            var tableName = typeof(TEntity).Name.AsNpgsqlConvention();

            builder.ToTable(tableName, schema.ToLower());
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd().UseHiLo($"seq_{tableName}_id", schema.ToLower()).HasComment("Table unique identity").IsRequired();
            builder.Property(p => p.UpdatedDate).HasComment("Recorde updated timestamp ");
            builder.Property(p => p.CreatedDate).HasComment("Record inserted timestamp").IsRequired();
        }

        public static string GetColName(this PropertyEntry propertyEntry)
        {
            var storeObjectId = StoreObjectIdentifier.Create(propertyEntry.Metadata.DeclaringEntityType, StoreObjectType.Table);
            return propertyEntry.Metadata.GetColumnName(storeObjectId.GetValueOrDefault());
        }
    }
}
