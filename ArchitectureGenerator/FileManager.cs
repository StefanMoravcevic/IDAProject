using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureGenerator
{

    public class FileManager
    {
        public static string projectDirectory
        {
            get
            {
                return Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
            }
        }

        public static void ProcessAndWriteFile(CodeFile file, Entity ent)
        {
            var templatePath = Path.Combine(projectDirectory, "ArchitectureGenerator", "ObjectTemplates", file.TemplateName);
            var template = File.ReadAllText(templatePath);
            var entityProperties = EntityManager.GetEntityBasicProperties(ent);
            var basicProperties = FormatPropertiesForClass(entityProperties);
            var columnDefinitions = FormatColumnDefinitionsForClass(entityProperties);

            var code = template.Replace("<<EntityName>>", ent.Name)
                .Replace("<<EntityNameSingular>>", ent.SingularName)
                .Replace("<<BasicProperties>>", basicProperties)
                .Replace("<<ColumnDefinitions>>", columnDefinitions);

            string directoryPath = Path.GetDirectoryName(file.FullPath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            File.WriteAllText(file.FullPath, code);
        }

        public static List<CodeFile> GetFileList(Entity entity)
        {
            var fileList = new List<CodeFile>()
            {
        new(fileName: $"Save{entity.SingularName}RequestModel.cs",
            path: Path.Combine(projectDirectory, entity.Namespace + ".Models", "Dto", $"{entity.Name}"),
            templateName: "SaveEntityRequestModelTemplate.txt"),
        new(fileName: $"Search{entity.Name}Params.cs",
            path: Path.Combine(projectDirectory, entity.Namespace + ".Models", "RequestModels", $"{entity.Name}"),
            templateName: "SearchEntityParamsTemplate.txt"),
        new(fileName: $"{entity.SingularName}Dto.cs",
            path: Path.Combine(projectDirectory, entity.Namespace + ".Models", "Dto", $"{entity.Name}"),
            templateName: "EntityDtoTemplate.txt"),
        new(fileName: $"I{entity.Name}Repository.cs",
            path: Path.Combine(projectDirectory, entity.Namespace + ".Api.Models", "Interfaces", "Repositories"),
            templateName: "ApiRepositoryInterfaceTemplate.txt"),
        new(fileName: $"I{entity.Name}Manager.cs",
            path: Path.Combine(projectDirectory, entity.Namespace + ".Api.Models", "Interfaces", "Managers"),
            templateName: "ApiManagerInterfaceTemplate.txt"),
        new(fileName: $"{entity.Name}Repository.cs", path: Path.Combine(projectDirectory, entity.Namespace + ".Api.Repositories"),
            templateName: "ApiRepositoryTemplate.txt"),
        new(fileName: $"{entity.Name}Manager.cs", path: Path.Combine(projectDirectory, entity.Namespace + ".Api.Managers"),
            templateName: "ApiManagerTemplate.txt"),
        new(fileName: $"{entity.Name}Controller.cs", path: Path.Combine(projectDirectory, entity.Namespace + ".Api", "Controllers"),
            templateName: "ApiControllerTemplate.txt"),

        //patch in admin project
        new(fileName: $"{entity.Name}ViewModel.cs",
            path: Path.Combine(projectDirectory, "IDAProject.Web.Admin", "Models", "ViewModels", $"{entity.Name}"),
            templateName: "EntityViewModel.txt"),
        new(fileName: $"{entity.SingularName}ViewModel.cs",
            path: Path.Combine(projectDirectory, "IDAProject.Web.Admin", "Models", "ViewModels", $"{entity.Name}"),
            templateName: "EntitySingularViewModel.txt"),
        new(fileName: $"I{entity.Name}Manager.cs",
            path: Path.Combine(projectDirectory, "IDAProject.Web.Admin.Models", "Interfaces", "Managers"),
            templateName: "AdminManagerInterfaceTemplate.txt"),
        new(fileName: $"{entity.Name}Manager.cs", path: Path.Combine(projectDirectory, "IDAProject.Web.Admin.Managers"),
            templateName: "AdminManagerTemplate.txt"),
        new(fileName: $"{entity.Name}Controller.cs", path: Path.Combine(projectDirectory, "IDAProject.Web.Admin", "Controllers"),
            templateName: "AdminControllerTemplate.txt"),
            };
            return fileList;
        }

        public static string FormatPropertiesForClass(PropertyInfo[] properties)
        {
            return string.Join(Environment.NewLine, properties.Select(prop =>
            {
                var type = prop.PropertyType;
                string typeName;

                // Check if the type is nullable and format accordingly
                if (IsNullableType(type))
                {
                    var underlyingType = Nullable.GetUnderlyingType(type);
                    typeName = underlyingType.Name + "?";
                }
                else
                {
                    typeName = type.Name;
                }

                return $"public {typeName} {prop.Name} {{ get; set; }}";
            }));
        }

        private static bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static string FormatColumnDefinitionsForClass(PropertyInfo[] properties)
        {
            return string.Join(Environment.NewLine, properties.Select(prop =>
                $@"new( ""{prop.Name}"", ""{prop.Name}""), "));
        }
    }
}
