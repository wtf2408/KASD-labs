using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kasd_labs_console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Complex number = new Complex(0, 0);

            while (true)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("A - Ввод комплексного числа");
                Console.WriteLine("S - Сложение");
                Console.WriteLine("D - Вычитание");
                Console.WriteLine("M - Умножение");
                Console.WriteLine("E - Деление");
                Console.WriteLine("R - Модуль");
                Console.WriteLine("G - Аргумент");
                Console.WriteLine("Q - Выход");

                Console.Write("Введите команду: ");
                char command = Console.ReadKey().KeyChar;
                Console.WriteLine();

                switch (command)
                {
                    case 'A':
                        Console.Write("Введите вещественную часть: ");
                        double real = double.Parse(Console.ReadLine());
                        Console.Write("Введите мнимую часть: ");
                        double imaginary = double.Parse(Console.ReadLine());
                        number = new Complex(real, imaginary);
                        Console.WriteLine($"Введено число: {number}");
                        break;

                    case 'S':
                        Console.Write("Введите вещественную часть второго числа: ");
                        double real2 = double.Parse(Console.ReadLine());
                        Console.Write("Введите мнимую часть второго числа: ");
                        double imaginary2 = double.Parse(Console.ReadLine());
                        number += new Complex(real2, imaginary2);
                        Console.WriteLine($"Результат сложения: {number}");
                        break;

                    case 'D':
                        Console.Write("Введите вещественную часть второго числа: ");
                        real2 = double.Parse(Console.ReadLine());
                        Console.Write("Введите мнимую часть второго числа: ");
                        imaginary2 = double.Parse(Console.ReadLine());
                        number -= new Complex(real2, imaginary2);
                        Console.WriteLine($"Результат вычитания: {number}");
                        break;

                    case 'M':
                        Console.Write("Введите вещественную часть второго числа: ");
                        real2 = double.Parse(Console.ReadLine());
                        Console.Write("Введите мнимую часть второго числа: ");
                        imaginary2 = double.Parse(Console.ReadLine());
                        number *= new Complex(real2, imaginary2);
                        Console.WriteLine($"Результат умножения: {number}");
                        break;

                    case 'E':
                        Console.Write("Введите вещественную часть второго числа: ");
                        real2 = double.Parse(Console.ReadLine());
                        Console.Write("Введите мнимую часть второго числа: ");
                        imaginary2 = double.Parse(Console.ReadLine());
                        number /= new Complex(real2, imaginary2);
                        Console.WriteLine($"Результат деления: {number}");
                        break;

                    case 'R':
                        Console.WriteLine($"Модуль числа: {number.Module()}");
                        break;

                    case 'G':
                        Console.WriteLine($"Аргумент числа: {number.Argument()}");
                        break;

                    case 'Q':
                    case 'q':
                        Console.WriteLine("Выход из программы.");
                        return;

                    default:
                        Console.WriteLine("Неизвестная команда.");
                        break;
                }
            }
        }
    }
}
