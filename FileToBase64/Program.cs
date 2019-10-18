using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileToBase64
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Getting files...");
            var files = GetFilesToConvert();
            Console.WriteLine($"Found {files.Length} files");
            
            Console.WriteLine($"Converting files...");
            var base64Files = ConvertFiles(files);
            Console.WriteLine($"All files converted");
            
            Console.WriteLine($"Saving files...");
            SaveFiles(base64Files);
            Console.WriteLine($"Files saved");
            
            Console.WriteLine("DONE");
        }

        private static string[] GetFilesToConvert()
        {
            return Directory.GetFiles($"{Environment.CurrentDirectory}/Files/Input/");
        }

        private static string GetFilesOutputFolder()
        {
            return $"{Environment.CurrentDirectory}/Files/Output/";
        }

        private static Dictionary<string, string> ConvertFiles(string[] files)
        {
            var base64Files = new Dictionary<string, string>();
            
            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                Console.WriteLine($"Converting file {fileName}...");
                var bytes = File.ReadAllBytes(file);
                var base64String = Convert.ToBase64String(bytes);
                base64Files.Add(fileName, base64String);
            }

            return base64Files;
        }

        private static void SaveFiles(Dictionary<string, string> base64Files)
        {
            foreach (var file in base64Files.Keys)
            {
                var fileName = Path.GetFileNameWithoutExtension(file);
                var fileContent = base64Files[file];
                var filePath = GetFilesOutputFolder() + fileName + ".txt";
                
                Console.WriteLine($"Writing file {fileName}...");
                
                if(File.Exists(filePath))
                    File.Delete(filePath);
                
                File.AppendAllText(filePath, fileContent);
            }
        }
    }
}