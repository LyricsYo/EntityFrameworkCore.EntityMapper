using EFCoreWithPostgresqlDemo.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreWithPostgresqlDemo.Data.Mapping
{
    public interface IEntityMapper : IEntityRegister
    {
        void Configure(EntityTypeBuilder builder);
    }


    public interface IEntityMapper<TEntity> : IEntityRegister
        where TEntity : class
    {
        void Configure(EntityTypeBuilder<TEntity> builder);
    }
}
