using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreWithPostgresqlDemo.Data.Mapping
{
    public interface IEntityRegister
    {
        void Register(ModelBuilder modelBuilder);
    }
}
