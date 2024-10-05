using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

//Из файла input.txt считывать строки (без пробелов). Из каждой строки извлечь теги
//(Пример: < html >, </ H1 >, < PrIvet >) и поместить их в динамический массив. Тег начинается
//символом <, заканчивается символом >, после < может иметь символ /. Остальные
//символы — цифры и буквы, первый символ – обязательно буква. Удалить из списка
//повторяющие теги (с точностью до наличия/отсутствия символа /, и регистра букв, т. е.
//<html> и </HtMl> – одинаковые теги).

namespace kasd_labs_console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var list = new MyArrayList<string>(5);
            foreach (var row in File.ReadAllLines("D:/KUBGU/kasd-labs/kasd_labs_console/input.html"))
            {
                var tag = string.Empty; bool appending = false;
                foreach (var symbol in row)
                {
                    if (symbol == '<') { appending = true; tag = "<"; }
                    else if (symbol == '>' && appending) 
                    { 
                        appending = false;
                        bool same = false;
                        tag += ">";
                        foreach (var _tag in list)
                        {
                            if (_tag != null && isSameTags((string)_tag, tag)) { same = true; }
                        }
                        if (!same) list.Add(tag); 
                    }
                    else {
                        if (appending) {
                            if (tag.Length == 1 && (from n in new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' }
                                                    where n == symbol
                                                    select n).ToArray().Length == 1) { appending = false; continue; }
                            tag += symbol;
                        }
                    }
                } 
            }
            foreach (var item in list ) { Console.WriteLine(item); }

        }
        static bool isSameTags(string tag1, string tag2)
        {
            tag1 = tag1.ToLower().Replace("<", "").Replace(">", "").Replace("/", "");
            tag2 = tag2.ToLower().Replace("<", "").Replace(">", "").Replace("/", "");
            return tag1.Equals(tag2);
        }
    }
}
