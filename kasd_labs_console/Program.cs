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
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
            string inputFile = Path.Combine(dir, "input.txt");

            string tagPattern = @"</?[a-zA-Z][a-zA-Z0-9]*>";

            MyHashMap<string, int> myHashMap = new MyHashMap<string, int>();

            string[] lines = File.ReadAllLines(inputFile);

            Console.WriteLine("Извлечённые теги:");
            foreach (var line in lines)
            {
                string trimmedLine = line.Replace(" ", "").Replace("/", "").ToLower();
                var matches = Regex.Matches(trimmedLine, tagPattern);

                foreach (Match match in matches)
                {
                    string tag = match.Value;
                    Console.WriteLine(tag);
                    if (myHashMap.ContainsKey(tag))
                    {
                        myHashMap.Put(tag, myHashMap.Get(tag)+1);
                    }
                    else
                    {
                        myHashMap.Put(tag, 1);
                    }
                }
            }

            
            var hashedSet = myHashMap.EntrySet();
            
            foreach (var pair in hashedSet)
            {
                Console.WriteLine($"Тег: {pair.Key} Найден {pair.Value} раз.");
            }
            
        }
    }
}
        
