using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static System.Math;

namespace kasd_labs_winforms
{
    class Generation
    {

        private static T GetRandomElement<T>()
        {
            Random random = new Random(((int)DateTime.Now.Ticks));
            if (typeof(T) == typeof(int))
            {
                return (T)(object)random.Next(1000);
            }
            if (typeof(T) == typeof(char))
            {
                char c = (char)random.Next(255);
                return (T)(object)c;
            }
            if (typeof(T) == typeof(double))
            {
                return (T)(object)(random.NextDouble() % 1000);
            }
            else throw new Exception("This type is not supported");
        }
        public static T[] GenSortMass<T>(int size)
        {
            T[] mass = new T[size];
            for (int i = 0; i < size; i++)
            {
                mass[i] = GetRandomElement<T>();
            }

            Array.Sort(mass, ComparatorGen.GetComparer<T>());
            return mass;
        }
        public static T[] GenRandomMass<T>(int size)
        {
            Random random = new Random(((int)DateTime.Now.Ticks));
            T[] mass = new T[size];
            for (int i = 0; i < size; i++)
            {
                mass[i] = GetRandomElement<T>();
            }

            return mass;
        }
        public static T[] GenDescSortMass<T>(int size)
        {
            T[] sortMass = GenSortMass<T>(size);
            T[] mass = new T[size];
            for (int i = 0; i < size; i++)
            {
                mass[i] = sortMass[size - i - 1];
            }

            return mass;
        }
        public static T[] GenSubmassivesMass<T>(int size)
        {
            Random random = new Random(((int)DateTime.Now.Ticks));
            T[] mass = new T[size];
            int index = 0;
            int rest_size = size;

            while (rest_size != 0)
            {
                int submass_size = rest_size >= 500
                    ? random.Next(1, rest_size / 100)
                    : random.Next(1, rest_size / 10);
                submass_size *= 10;
                T[] submass = GenSortMass<T>(submass_size);
                for (int i = 0; i < submass_size; i++) { mass[index++] = submass[i]; }

                rest_size -= submass_size;
            }

            return mass;
        }
        public static T[] GenSwapMass<T>(int size)
        {
            Random random = new Random(((int)DateTime.Now.Ticks));
            T[] mass = GenSortMass<T>(size);
            int swap_size = random.Next(size / 10);

            for (int i = 0; i < swap_size; i++)
            {
                int ind1 = random.Next(0, size - 1);
                int ind2 = random.Next(0, size - 1);

                Sorting.Swap(ref mass[ind1], ref mass[ind2]);
            }

            return mass;
        }
        public static T[] GenReplaceMass<T>(int size)
        {
            Random random = new Random(((int)DateTime.Now.Ticks));
            T[] mass = GenSortMass<T>(size);
            int swap_size = random.Next(size);
            for (int i = 0; i < swap_size; i++)
            {
                int ind = random.Next(0, size - 1);
                T value = GetRandomElement<T>();

                mass[ind] = value;
            }
            return mass;
        }

        public static T[] GenRepeatMass<T>(int size)
        {
            Random random = new Random(((int)DateTime.Now.Ticks));
            T[] mass = GenSortMass<T>(size);

            int[] mass_rep = { size / 10, size / 4, size / 2, 3 * (size / 4), 9 * (size / 10) };
            int rep_ind = random.Next(0, mass_rep.Length - 1);

            int ind = random.Next(0, size - mass_rep[rep_ind] - 1);
            var elem = mass[ind];
            for (int i = 0; i < mass_rep[rep_ind]; i++)
            {
                mass[ind++] = elem;
            }
            return mass;
        }
    }
}
