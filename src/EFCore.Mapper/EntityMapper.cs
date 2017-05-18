using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreWithPostgresqlDemo.Data.Mapping
{
    public abstract class EntityMapper<TEntity> : IEntityMapper<TEntity>
       where TEntity : class
    {

        public abstract void Configure(EntityTypeBuilder<TEntity> builder);

        public void Register(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TEntity>(Configure);
        }
    }


    public abstract class EntityMapper : IEntityMapper
    {
        private Type _entityType = null;
        private string _typeName = null;

        public EntityMapper(Type entityType)
        {
            _entityType = entityType;
        }

        public EntityMapper(string typeName)
        {
            _typeName = typeName;
        }

        public abstract void Configure(EntityTypeBuilder builder);

        public void Register(ModelBuilder modelBuilder)
        {
            if (_entityType != null)
                modelBuilder.Entity(_entityType, Configure);
            else if (_typeName != null)
                modelBuilder.Entity(_typeName, Configure);
        }
    }
}
