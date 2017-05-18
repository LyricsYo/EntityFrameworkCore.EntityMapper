using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace EFCoreWithPostgresqlDemo.Data.Mapping
{
    public static class EntityMapperExtensions
    {
        static List<IEntityRegister> _cacheEntityRegisters = null;

        public static void RegisterEntityMapper(this ModelBuilder modelBuilder, IEntityRegister configuration)
        {
            configuration.Register(modelBuilder);
        }

        public static void AutoRegisterMappings(this ModelBuilder modelBuilder, IEnumerable<Assembly> assemblies = null, bool useCache = true)
        {            
            assemblies = assemblies ?? LoadAssemblies();
            var baseType = typeof(IEntityRegister);
            if (!useCache || _cacheEntityRegisters == null)
                _cacheEntityRegisters = assemblies.SelectMany(a => a.DefinedTypes
                    .Where(typeInfo => baseType.IsAssignableFrom(typeInfo.AsType()) && typeInfo.IsClass && !typeInfo.IsAbstract)
                    .Select(t => Activator.CreateInstance(t.AsType(), null) as IEntityRegister))
                    .ToList();
            _cacheEntityRegisters.ForEach(config => modelBuilder.RegisterEntityMapper(config));
        }

        private static IEnumerable<Assembly> LoadAssemblies()
        {
            var AssemblyFilePathes = Directory.GetFiles(AppContext.BaseDirectory, "*.dll", SearchOption.AllDirectories);
            var assemblyNames = AssemblyFilePathes.Select(path => AssemblyLoadContext.GetAssemblyName(path));
            var assemblies = assemblyNames.Select(name => AssemblyLoadContext.Default.LoadFromAssemblyName(name));
            return assemblies;
        }
    }
}
