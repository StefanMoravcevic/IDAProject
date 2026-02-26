using System.Reflection;
using Humanizer;

public class EntityManager
{
    public static Entity? GetEntity(string entityName)
    {
        Assembly assembly = Assembly.Load("IDAProject.Web.Db");
        // Assuming your entities follow a naming convention and are in a specific namespace
        string fullEntityName = $"IDAProject.Web.Db.MainDatabase.{entityName}";
        Type entityType = assembly.GetType(fullEntityName);
        return new Entity()
        {
            Name = entityName.Pluralize(),
            SingularName = entityName,
            Namespace = "IDAProject.Web",
            EntityType = entityType
        };
    }

    // method for getting the properties of an entity based on certain rules
    public static PropertyInfo[] GetEntityBasicProperties(Entity entity)
    {
        var propertiesToExclude = new HashSet<string> { "IsDeleted", "DeletedDate", "DeletedBy" };

        if (entity == null)
        {
            throw new InvalidOperationException($"Entity type '{entity.SingularName}' not found.");
        }

        return entity.EntityType.GetProperties()
            .Where(prop => !propertiesToExclude.Contains(prop.Name) &&
                           !(prop.GetGetMethod()?.IsVirtual == true && !prop.GetGetMethod().IsFinal) &&
                           !(prop.GetSetMethod()?.IsVirtual == true && !prop.GetSetMethod().IsFinal))
            .ToArray();
    }
}