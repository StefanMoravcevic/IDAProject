// See https://aka.ms/new-console-template for more information

using System.Reflection;
using System.Runtime.CompilerServices;
using ArchitectureGenerator;
using Humanizer;


if (args.Length == 0)
{
    Console.WriteLine("Please specify the entity name.");
    return;
}

bool forceOverwrite = args.Contains("-f");
bool mocking = args.Contains("-m");

var ent = EntityManager.GetEntity(args[0]);

if (ent.EntityType == null && !mocking)
{
    Console.WriteLine($"The entity {ent.SingularName} does not exist.");
    return;
}

var fileList =  FileManager.GetFileList(ent);
bool fileExists = fileList.Any(x => File.Exists(x.FullPath));

// Check if file exists and handle force overwrite
if (fileExists && !forceOverwrite)
{
    Console.WriteLine($"The architecture for {ent.Name} already exists. Use -f to force overwrite.");
}
else
{
    // Process the files and generate the code
    foreach (var file in fileList)
    {
        FileManager.ProcessAndWriteFile(file, ent);
    }
}


