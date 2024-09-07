using Jared.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection;
using System.Text;

namespace Jared.Api.Integration.Tests;

public static class SeedData
{
    public static async Task Seed(this IDataContext dataContext)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var fakerTypes = assembly.GetTypes().Where(t =>
            t.BaseType is not null &&
            t.BaseType.IsGenericType &&
            t.BaseType.GetGenericTypeDefinition() == typeof(BaseIntegrationFaker<>))
            .OrderBy(x =>
            {
                var priority = x.GetProperty("PRIORITY", BindingFlags.NonPublic | BindingFlags.Instance);
                var instance = Activator.CreateInstance(x);
                return (int)priority!.GetValue(instance)!;
            });

        foreach (var fakerType in fakerTypes)
        {
            StringBuilder insertCommand = new();

            var fakerInstance = Activator.CreateInstance(fakerType);
            var methods = fakerType
                .GetMethods(BindingFlags.Static | BindingFlags.Public)
                .Where(m => m.ReturnType.GetInterfaces().Contains(typeof(IEntity)));

            var entityType = fakerType.BaseType!.GetGenericArguments()[0];
            var entity = dataContext.Model.FindEntityType(entityType)
                ?? throw new("Entity not found in database");

            var properties = entity.GetProperties();

            insertCommand.AppendLine($"SET IDENTITY_INSERT [{entityType.Name}] ON;");
            foreach (var method in methods)
            {
                var seedData = method!.Invoke(fakerInstance, null);
                insertCommand.AppendLine(GenerateInsertCommand(properties, entityType.Name!, seedData!));
            }
            insertCommand.AppendLine($"SET IDENTITY_INSERT [{entityType.Name}] OFF;");

            await dataContext.Database.ExecuteSqlRawAsync(insertCommand.ToString());
            await dataContext.SaveChangesAsync(default);
        }
    }

    private static string GenerateInsertCommand(IEnumerable<IProperty> properties, string tableName, object data)
    {
        var columns = string.Join(", ", properties.Select(p => p.Name));
        var values = string.Join(", ", properties.Select(p =>
        {
            var propertyInfo = data.GetType().GetProperty(p.Name);
            var value = propertyInfo?.GetValue(data);

            if (value is null)
            {
                return "NULL";
            }

            if (DateTime.TryParse(value!.ToString(), out var date))
            {
                value = date.ToString("yyyy-MM-dd hh:mm:ss");
            }
            else if (value.GetType().IsEnum)
            {
                value = (int)value;
            }

            return $"'{value}'";
        }));

        return $"INSERT INTO [{tableName}] ({columns}) VALUES ({values}); ";
    }
}
