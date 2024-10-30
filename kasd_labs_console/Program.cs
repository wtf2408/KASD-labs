using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace kasd_labs_console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("D:\\KUBGU\\kasd-labs\\kasd_labs_console\\input.html");
            foreach (var line in lines)
            {
                var words = line.Split(' ');
                foreach (var word in words)
                {
                    string ip = string.Empty;
                    var ips = word.Select(c => c)
                                  .Where(c =>
                                  {
                                      return int.TryParse(c.ToString(), out int parsed) || c == '.';
                                  });
                    
                    foreach (var digit in ips)
                    {
                        ip += digit;
                        //Console.Write(digit);
                    }
                    if (!string.IsNullOrEmpty(ip))
                    {
                        var numbers = ip.Split('.');
                        if (numbers.All(n => !string.IsNullOrEmpty(n)) && numbers.Length == 4)
                            Console.WriteLine($"{ip} ");
                    }
                }
            }
        }
    }
}
