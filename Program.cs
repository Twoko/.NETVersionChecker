using System;
using System.IO;
using System.Collections.Generic;
using Mono.Cecil;
using System.Linq;

namespace DotNetVersionChecker
{
    class Program
    {
        class AssemblyInfo
        {
            public string Path { get; set; }
            public string FileName { get; set; }
            public string TargetFramework { get; set; }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Enter the directory path to scan:");
            var folderPath = Console.ReadLine();

            Console.WriteLine("Enter the output file path (including filename and .csv or .txt extension):");
            var outputPath = Console.ReadLine();

            var assemblies = new List<AssemblyInfo>();
            foreach (var filePath in Directory.EnumerateFiles(folderPath, "*.*", SearchOption.AllDirectories))
            {
                if (filePath.EndsWith(".dll") || filePath.EndsWith(".exe"))
                {
                    var version = GetTargetFrameworkVersion(filePath);
                    assemblies.Add(new AssemblyInfo
                    {
                        Path = filePath,
                        FileName = System.IO.Path.GetFileName(filePath),
                        TargetFramework = version
                    });
                }
            }

            // Sorting by Target Framework Version in descending order
            var sortedAssemblies = assemblies.OrderByDescending(a => a.TargetFramework).ToList();

            WriteResultsToFile(sortedAssemblies, outputPath);
        }

        static string GetTargetFrameworkVersion(string assemblyPath)
        {
            try
            {
                using (var assembly = AssemblyDefinition.ReadAssembly(assemblyPath))
                {
                    foreach (var attr in assembly.CustomAttributes)
                    {
                        if (attr.AttributeType.FullName == "System.Runtime.Versioning.TargetFrameworkAttribute")
                        {
                            return attr.ConstructorArguments[0].Value.ToString();
                        }
                    }
                    return "Unknown or Not Specified";
                }
            }
            catch (Exception ex)
            {
                return $"Error reading framework version: {ex.Message}";
            }
        }

        static void WriteResultsToFile(List<AssemblyInfo> assemblies, string outputPath)
{
    if (string.IsNullOrEmpty(outputPath))
    {
        return;
    }

    // Adding a header line for the titles
    var lines = new List<string> { "Path, FileName, Target Framework" };
    lines.AddRange(assemblies.Select(a => $"{a.Path}, {a.FileName}, {a.TargetFramework}"));

    try
    {
        if (outputPath.EndsWith(".csv") || outputPath.EndsWith(".txt"))
        {
            File.WriteAllLines(outputPath, lines);
        }
        else
        {
            Console.WriteLine("Unsupported file format. Only .csv and .txt are supported.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Failed to write to file: {ex.Message}");
    }
}
    }

}
