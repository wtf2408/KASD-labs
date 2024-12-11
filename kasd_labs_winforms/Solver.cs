using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace kasd_labs_winforms
{
    internal class Solver<T>
    {
        T[][] arrays;
        T[][] sortedArrays;
        bool executed = false;


        public void GenerateArrays(int size, string gen_group)
        {
            arrays = new T[size][];
            int s = 10;
            for (int i = 0; i < size; i++)
            {
                switch (gen_group)
                {
                    case "sort_mass":
                        arrays[i] = Generation.GenSortMass<T>(s);
                        break;
                    case "unsort_mass":
                        arrays[i] = Generation.GenDescSortMass<T>(s);
                        break;
                    case "random_mass":
                        arrays[i] = Generation.GenRandomMass<T>(s);
                        break;
                    case "submassives_mass":
                        arrays[i] = Generation.GenSubmassivesMass<T>(s);
                        break;
                    case "swap_mass":
                        arrays[i] = Generation.GenSwapMass<T>(s);
                        break;
                    case "replace_mass":
                        arrays[i] = Generation.GenReplaceMass<T>(s);
                        break;
                    case "repeat_mass":
                        arrays[i] = Generation.GenRepeatMass<T>(s);
                        break;
                    default:
                        break;
                }
                s *= 10;
            }
        }
        
        public List<(int, double)> SortingTimes(Action<T[], Comparer<T>> fn)
        {
            List<(int, double)> list = new List<(int, double)>();
            Stopwatch sw;


            sortedArrays = new T[arrays.GetLength(0)][]; 
            for (int i = 0; i < sortedArrays.Length; i++)
            {
                double middle = 0;
                sortedArrays[i] = new T[arrays[i].Length];
                for (int count = 0; count < 20; count++)
                {
                    Array.Copy(arrays[i], sortedArrays[i], arrays[i].Length);
                    sw = new Stopwatch();
                    sw.Start();

                    fn(arrays[i], ComparatorGen.GetComparer<T>());

                    sw.Stop();
                    middle += sw.ElapsedMilliseconds;
                }
                middle /= 20;
                list.Add((i, middle));
            }
            return list;
        }

        public async void SaveResultsToFile()
        {
            StreamWriter gen_file = new StreamWriter("../../generatedArrays.txt");
            for (int i = 0; i < arrays.Length; i++)
            {
                for (int j = 0; j < arrays[i].Length; j++)
                {
                    await gen_file.WriteAsync($"{arrays[i][j]} ");
                }
                await gen_file.WriteLineAsync();
            }

            StreamWriter sort_file = new StreamWriter("../../sortedArrays.txt");
            for (int i = 0; i < sortedArrays.Length; i++)
            {
                for (int j = 0; j < sortedArrays[i].Length; j++)
                {
                    await sort_file.WriteAsync($"{sortedArrays[i][j]} ");
                }
                await sort_file.WriteLineAsync();
            }
        }
    }
}
