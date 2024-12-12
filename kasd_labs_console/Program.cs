using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using kasd_labs_console;



namespace kasd_labs_console
{


    class Program
    {
        static void Main()
        {
            var dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
            string inputFilePath = Path.Combine(dir, "input.txt");
            string outputFilePath = Path.Combine(dir, "results.txt");

            string pattern = @"(?<type>[a-zA-Z_][a-zA-Z0-9_]*)\s+(?<name>[a-zA-Z_][a-zA-Z0-9_]*)\s*=\s*(?<value>\d+)\s*;";
            MyHashMap<string, Variable> variabelsMap = new MyHashMap<string, Variable>();
            var errors = new List<string>();


            string variablesFile = File.ReadAllText(inputFilePath);
            variablesFile = variablesFile.Replace("\r", "").Replace("\n", " ");

            MatchCollection matches = Regex.Matches(variablesFile, pattern);

            if (matches.Count > 0) 
            {
                foreach (Match match in matches)
                {
                    string typeStr = match.Groups["type"].Value;
                    string name = match.Groups["name"].Value;
                    string value = match.Groups["value"].Value;
                        

                    if (!Enum.TryParse(typeStr, true, out Type type))
                    {
                        errors.Add($"Некорректный тип: {typeStr} для переменной {name}.");
                        continue;
                    }
                    if (variabelsMap.ContainsKey(name))
                    {
                        errors.Add($"Переопределение переменной: {name}. Значение {value} игнорируется. " +
                                    $"Оставляем первую определённую переменную");
                        continue;
                    }

                    variabelsMap.Put(name, new Variable(type, value));
                }

                using (StreamWriter writer = new StreamWriter(outputFilePath))
                {
                    foreach (var entry in variabelsMap.EntrySet())
                    {
                        writer.WriteLine($"{entry.Value.Type} => {entry.Key}({entry.Value.Value})");
                    }

                    writer.WriteLine("\nОшибки:");
                    foreach (string error in errors)
                    {
                        writer.WriteLine(error);
                    }
                }

                Console.WriteLine($"Результаты сохранены в {outputFilePath}");
            }
            else
            {
                Console.WriteLine("Определения переменных не найдены.");
            }

        }
    }
}
        
